using BLL.Entities;
using BLL.IRepositories;
using DAL.Entities;
using HairShopWebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HairShopWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplyController : ControllerBase
    {
        private readonly IGenericRepository<Supply> _repository;
        private readonly IBaseRepository _baseRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly ShopContext _context;
        private readonly ILogger<SupplyController> _logger;

        public SupplyController(IGenericRepository<Supply> repository, IBaseRepository baseRepository, IRepository<Product> productRepository, ILogger<SupplyController> logger, ShopContext context)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet("/supply-get-list")]
        public IEnumerable<SupplyModel> GetList()
        {
            return _baseRepository.GetSupplyListWithSupplier().Select(b => new SupplyModel
            {
                supplyID = b.SupplyID,
                invoice = b.Invoice,
                data = b.Data,
                supplierID = b.SupplierID,
                supplierName = b.Supplier?.SupplierName
            });
        }

        [HttpGet("/supply-get-by-id/{id}")]
        public ActionResult<SupplyModel> Get(int id)
        {
            var itemDb = _repository.GetByIdOrNULL(id);

            if (itemDb == null)
            {
                return NotFound();
            }

            var itemNew = new SupplyModel
            {
                supplyID = itemDb.SupplyID,
                invoice = itemDb.Invoice,
                data = itemDb.Data,
                supplierID = itemDb.SupplierID
            };

            return itemNew;
        }

        [HttpPost("/supply-post")]
        public JsonResult Create(SupplyModel model)
        {
            var dt = model.data;
            dt = dt.AddSeconds(-dt.Second);

            var productList = _productRepository.GetDataList();

            var item = new Supply
            {
                Invoice = model.invoice,
                Data = dt,
                SupplierID = model.supplierID,
                UserID = model.userID
            };
            try
            {
                _repository.Created(item);

                foreach (var productModel in model.productsModel)
                {
                    foreach (var product in productList)
                    {
                        if (productModel.productID == product.ProductID)
                        {
                            product.CountStock = product.CountStock + productModel.countStock;
                            product.UnitPrice = productModel.unitPrice;
                        }
                    }
                    item.SupplyForProducts.Add(new SupplyForProduct { ProductID = productModel.productID });
                }
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new JsonResult("Create was not successful");
            }

            return new JsonResult($"Успешно создана запись под id = { item.SupplyID }");
        }

        [HttpPut("/supply-put-by-id/{id}")]
        public JsonResult Update(int id, SupplyModel model)
        {
            if (id != model.supplyID)
            {
                return new JsonResult("Id incorrect");
            }
            var item = new Supply
            {
                SupplyID = model.supplyID,
                Invoice = model.invoice,
                Data = model.data,
                SupplierID = model.supplierID,
                UserID = model.userID
            };

            try
            {
                _repository.Update(item);
            }
            catch (DbUpdateConcurrencyException)
            {
                return new JsonResult("Update was not successful");
            }

            return new JsonResult($"Update successful");
        }

        [HttpDelete("/supply-delete-by-id/{id}")]
        public JsonResult Delete(int id)
        {

            if (_repository.Delete(id))
            {
                return new JsonResult("Delete successful");
            }
            else
            {
                return new JsonResult("Delete was not successful");
            }
        }
    }
}

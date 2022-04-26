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
    public class CheckController : ControllerBase
    {
        private readonly IRepository<Check> _repository;
        private readonly IRepository<Product> _productRepository;
        private readonly ShopContext _context;
        private readonly ILogger<CheckController> _logger;

        public CheckController(IRepository<Check> repository, IRepository<Product> productRepository, ILogger<CheckController> logger, ShopContext context)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet("/check-get-list")]
        public IEnumerable<CheckModel> GetList()
        {
            return _repository.GetDataList().Select(b => new CheckModel
            {
                checkID = b.CheckID,
                date = b.Date,
                totalPrice = b.TotalPrice,
                userID = b.UserID
            });
        }

        [HttpGet("/check-get-by-id/{id}")]
        public ActionResult<CheckModel> Get(int id)
        {
            var itemDb = _repository.GetData(id);

            if (itemDb == null)
            {
                return NotFound();
            }

            var itemNew = new CheckModel
            {
                checkID = itemDb.CheckID,
                date = itemDb.Date,
                totalPrice = itemDb.TotalPrice,
                userID = itemDb.UserID
            };

            return itemNew;
        }

        [HttpPost("/check-post")]
        public JsonResult Create(CheckModel model)
        {
            var item = new Check
            {
                Date = DateTime.Now,
                TotalPrice = model.totalPrice,
                UserID = model.userID
            };

            var productList = _productRepository.GetDataList();

            try
            {
                _repository.Create(item);
                _repository.Save();

                foreach (var productModel in model.productsModel)
                {
                    foreach (var product in productList)
                    {
                        if (productModel.productID == product.ProductID)
                        {
                            product.CountStock = product.CountStock - productModel.countStock;
                            product.UnitPrice = productModel.unitPrice;
                        }
                    }
                    item.CheckForProducts.Add(new CheckForProduct { ProductID = productModel.productID });
                }
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new JsonResult("Create was not successful");
            }

            return new JsonResult($"Create successful id = { item.CheckID }");
        }

        [HttpPut("/check-put-by-id/{id}")]
        public JsonResult Update(int id, CheckModel model)
        {
            if (id != model.checkID)
            {
                return new JsonResult("Id incorrect");
            }
            var item = new Check
            {
                CheckID = model.checkID,
                Date = model.date,
                TotalPrice = model.totalPrice
                //user
            };

            try
            {
                _repository.Update(item);
                _repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new JsonResult("Update was not successful");
            }

            return new JsonResult($"Update successful");
        }

        [HttpDelete("/check-delete-by-id/{id}")]
        public JsonResult Delete(int id)
        {

            if (_repository.Delete(id))
            {
                _repository.Save();
                return new JsonResult("Delete successful");
            }
            else
            {
                return new JsonResult("Delete was not successful");
            }
        }
    }
}

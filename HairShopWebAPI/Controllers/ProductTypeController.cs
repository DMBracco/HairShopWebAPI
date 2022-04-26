using BLL.Entities;
using BLL.IRepositories;
using HairShopWebAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HairShopWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductTypeController : ControllerBase
    {
        private readonly IGenericRepository<ProductType> _repository;
        private readonly ILogger<ProductTypeController> _logger;

        public ProductTypeController(IGenericRepository<ProductType> repository, ILogger<ProductTypeController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("/productType-get-list")]
        public IEnumerable<ProductTypeModel> GetList()
        {
            return _repository.GetList().Select(b => new ProductTypeModel
            {
                productTypeID = b.ProductTypeID,
                productTypeName = b.ProductTypeName
            });
        }

        [HttpGet("/productType-get-by-id/{id}")]
        public ActionResult<ProductTypeModel> Get(int id)
        {
            var todoItem = _repository.GetByIdOrNULL(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            var item = new ProductTypeModel
            {
                productTypeID = todoItem.ProductTypeID,
                productTypeName = todoItem.ProductTypeName
            };

            return item;
        }

        [HttpPost("/productType-post")]
        public JsonResult Create(ProductTypeModel model)
        {
            var item = new ProductType { ProductTypeName = model.productTypeName };
            try
            {
                _repository.Created(item);
            }
            catch (DbUpdateConcurrencyException)
            {
                return new JsonResult("Create was not successful");
            }

            return new JsonResult($"Create successful id = { item.ProductTypeID }");
        }

        [HttpPut("/productType-put-by-id/{id}")]
        public JsonResult Update(int id, ProductTypeModel model)
        {
            if (id != model.productTypeID)
            {
                return new JsonResult("Id incorrect");
            }
            var item = new ProductType { ProductTypeID = model.productTypeID, ProductTypeName = model.productTypeName };

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

        [HttpDelete("/productType-delete-by-id/{id}")]
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

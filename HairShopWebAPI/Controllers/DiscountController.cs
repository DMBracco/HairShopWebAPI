using BLL.Entities;
using BLL.IRepositories;
using HairShopWebAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HairShopWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IGenericRepository<Discount> _repository;
        private readonly ILogger<DiscountController> _logger;

        public DiscountController(IGenericRepository<Discount> repository, ILogger<DiscountController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("/discount-get-list")]
        public IEnumerable<DiscountModel> GetList()
        {
            return _repository.GetList().Select(b => new DiscountModel
            {
                discountID = b.DiscountID,
                discountName = b.DiscountName,
                discountAmount = b.DiscountAmount,
                dateStart = b.DateStart,
                dateEnd = b.DateEnd,
                productID = b.ProductID
            });
        }

        [HttpGet("/discount-get-by-id/{id}")]
        public ActionResult<DiscountModel> Get(int id)
        {
            var itemDb = _repository.GetByIdOrNULL(id);

            if (itemDb == null)
            {
                return NotFound();
            }

            var itemNew = new DiscountModel
            {
                discountID = itemDb.DiscountID,
                discountName = itemDb.DiscountName,
                discountAmount = itemDb.DiscountAmount,
                dateStart = itemDb.DateStart,
                dateEnd = itemDb.DateEnd,
                productID = itemDb.ProductID
            };

            return itemNew;
        }

        [HttpPost("/discount-post")]
        public JsonResult Create(DiscountModel model)
        {
            var item = new Discount {
                DiscountName = model.discountName,
                DiscountAmount = model.discountAmount,
                DateStart = model.dateStart,
                DateEnd = model.dateEnd,
                ProductID = model.productID
            };
            try
            {
                _repository.Created(item);
            }
            catch (DbUpdateConcurrencyException)
            {
                return new JsonResult("Create was not successful");
            }

            return new JsonResult($"Create successful id = { item.DiscountID }");
        }

        [HttpPut("/discount-put-by-id/{id}")]
        public JsonResult Update(int id, DiscountModel model)
        {
            if (id != model.discountID)
            {
                return new JsonResult("Id incorrect");
            }
            var item = new Discount {
                DiscountID = model.discountID,
                DiscountName = model.discountName,
                DiscountAmount = model.discountAmount,
                DateStart = model.dateStart,
                DateEnd = model.dateEnd,
                ProductID = model.productID
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

        [HttpDelete("/discount-delete-by-id/{id}")]
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

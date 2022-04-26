using BLL.Entities;
using BLL.IRepositories;
using HairShopWebAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HairShopWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HairTypeController : ControllerBase
    {
        private readonly IGenericRepository<HairType> _repository;
        private readonly ILogger<HairTypeController> _logger;

        public HairTypeController(IGenericRepository<HairType> repository, ILogger<HairTypeController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("/hairType-get-list")]
        public IEnumerable<HairTypeModel> GetList()
        {
            return _repository.GetList().Select(b => new HairTypeModel
            {
                hairTypeID = b.HairTypeID,
                hairTypeName = b.HairTypeName
            });
        }

        [HttpGet("/hairType-get-by-id/{id}")]
        public ActionResult<HairTypeModel> Get(int id)
        {
            var todoItem = _repository.GetByIdOrNULL(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            var item = new HairTypeModel
            {
                hairTypeID = todoItem.HairTypeID,
                hairTypeName = todoItem.HairTypeName
            };

            return item;
        }

        [HttpPost("/hairType-post")]
        public JsonResult Create(HairTypeModel model)
        {
            var item = new HairType { HairTypeName = model.hairTypeName };
            try
            {
                _repository.Created(item);
            }
            catch (DbUpdateConcurrencyException)
            {
                return new JsonResult("Create was not successful");
            }

            return new JsonResult($"Create successful id = { item.HairTypeID }");
        }

        [HttpPut("/hairType-put-by-id/{id}")]
        public JsonResult Update(int id, HairTypeModel model)
        {
            if (id != model.hairTypeID)
            {
                return new JsonResult("Id incorrect");
            }
            var item = new HairType { HairTypeID = model.hairTypeID, HairTypeName = model.hairTypeName };

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

        [HttpDelete("/hairType-delete-by-id/{id}")]
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

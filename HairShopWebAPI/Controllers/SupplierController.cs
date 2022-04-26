using BLL.Entities;
using BLL.IRepositories;
using HairShopWebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HairShopWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IGenericRepository<Supplier> _repository;
        private readonly ILogger<SupplierController> _logger;

        public SupplierController(IGenericRepository<Supplier> repository, ILogger<SupplierController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("/supplier-get-list")]
        public IEnumerable<SupplierModel> GetList()
        {
            return _repository.GetList().Select(b => new SupplierModel
            {
                supplierID = b.SupplierID,
                supplierName = b.SupplierName,
                phonenumber = b.Phonenumber
            });
        }

        [HttpGet("/supplier-get-by-id/{id}")]
        public ActionResult<SupplierModel> Get(int id)
        {
            var itemDb = _repository.GetByIdOrNULL(id);

            if (itemDb == null)
            {
                return NotFound();
            }

            var itemNew = new SupplierModel
            {
                supplierID = itemDb.SupplierID,
                supplierName = itemDb.SupplierName,
                phonenumber = itemDb.Phonenumber
            };

            return itemNew;
        }

        [HttpPost("/supplier-post")]
        public JsonResult Create(SupplierModel model)
        {
            var item = new Supplier
            {
                SupplierName = model.supplierName,
                Phonenumber = model.phonenumber
            };
            try
            {
                _repository.Created(item);
            }
            catch (DbUpdateConcurrencyException)
            {
                return new JsonResult("Create was not successful");
            }

            return new JsonResult($"Create successful id = { item.SupplierID }");
        }

        [HttpPut("/supplier-put-by-id/{id}")]
        public JsonResult Update(int id, SupplierModel model)
        {
            if (id != model.supplierID)
            {
                return new JsonResult("Id incorrect");
            }
            var item = new Supplier
            {
                SupplierID = model.supplierID,
                SupplierName = model.supplierName,
                Phonenumber = model.phonenumber
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

        [HttpDelete("/supplier-delete-by-id/{id}")]
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

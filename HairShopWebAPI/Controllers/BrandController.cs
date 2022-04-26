using BLL.Entities;
using BLL.IRepositories;
using HairShopWebAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HairShopWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IRepository<Brand> _brandRepository;
        private readonly ILogger<BrandController> _logger;

        public BrandController(IRepository<Brand> brandRepository, ILogger<BrandController> logger)
        {
            _brandRepository = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("/brand-get-list")]
        public IEnumerable<BrandModel> GetList()
        {
            return _brandRepository.GetDataList().Select(b => new BrandModel
            {
                brandID = b.BrandID,
                brandName = b.BrandName
            });
        }

        [HttpGet("/brand-get-by-id/{id}")]
        public ActionResult<BrandModel> Get(int id)
        {
            var todoItem = _brandRepository.GetData(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return BrandModel(todoItem);
        }

        [HttpPost("/brand-post")]
        public JsonResult Create(BrandModel brandModel)
        {
            var brand = new Brand { BrandName = brandModel.brandName };
            _brandRepository.Create(brand);
            try
            {
                _brandRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new JsonResult("Create was not successful");
            }

            return new JsonResult($"Create successful id = { brand.BrandID }");
        }

        [HttpPut("/brand-put-by-id/{id}")]
        public JsonResult Update(int id, BrandModel brandModel)
        {
            if (id != brandModel.brandID)
            {
                return new JsonResult("Id incorrect");
            }
            var brand = new Brand { BrandID=brandModel.brandID, BrandName=brandModel.brandName };

            _brandRepository.Update(brand);

            try
            {
                _brandRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new JsonResult("Update was not successful");
            }

            return new JsonResult($"Update successful");
        }

        [HttpDelete("/brand-delete-by-id/{id}")]
        public JsonResult Delete(int id)
        {
            
            if(_brandRepository.Delete(id))
            {
                _brandRepository.Save();
                return new JsonResult("Delete successful");
            }
            else
            {
                return new JsonResult("Delete was not successful");
            }
        }

        //private bool TodoItemExists(long id)
        //{
        //    return _context.TodoItems.Any(e => e.Id == id);
        //}

        private static BrandModel BrandModel(Brand b) =>
            new()
            {
                brandID = b.BrandID,
                brandName = b.BrandName
            };
    }
}

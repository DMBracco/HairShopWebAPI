using BLL.Entities;
using BLL.IRepositories;
using HairShopWebAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HairShopWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IRepository<Product> _repository;
        private readonly IRepository<Brand> _brandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        private readonly IGenericRepository<HairType> _hairTypeRepository;
        private readonly IBaseRepository _baseRepository;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IRepository<Product> repository, 
            IRepository<Brand> brandRepository, 
            IGenericRepository<ProductType> productTypeRepository, 
            IGenericRepository<HairType> hairTypeRepository, 
            ILogger<ProductController> logger, 
            IBaseRepository baseRepository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _brandRepository = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
            _productTypeRepository = productTypeRepository ?? throw new ArgumentNullException(nameof(productTypeRepository));
            _hairTypeRepository = hairTypeRepository ?? throw new ArgumentNullException(nameof(hairTypeRepository));
            _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("/product-get-list")]
        public IEnumerable<ProductModel> GetList()
        {
            return _repository.GetDataList().Select(b => new ProductModel
            {
                productID = b.ProductID,
                productName = b.ProductName,
                unitPrice = b.UnitPrice,
                countStock = b.CountStock,
                volume = b.Volume,
                brandID = b.BrandID,
                brandName = b.Brand?.BrandName,
                productTypeID = b.ProductTypeID,
                productTypeName = b.ProductType?.ProductTypeName,
                hairTypeID = b.HairTypeID,
                hairTypeName = b.HairType?.HairTypeName,
                //discountID = b.Discount.DiscountID,
                //discountAmount = b.Discount.DiscountAmount
            });
        }

        [HttpGet("/product-get-list-of-supply")]
        public IEnumerable<ProductModel> GetListOfSupply()
        {
            var products = _repository.GetDataList().ToList();
            var productModels = new List<ProductModel>();
            foreach (var product in products)
            {
                if (0 < product.SupplyForProducts.Count)
                {
                    var productModel = new ProductModel {
                        productID = product.ProductID,
                        productName = product.ProductName,
                        unitPrice = product.UnitPrice,
                        countStock = product.CountStock,
                        volume = product.Volume,
                        brandID = product.BrandID,
                        brandName = product.Brand?.BrandName,
                        productTypeID = product.ProductTypeID,
                        productTypeName = product.ProductType?.ProductTypeName,
                        hairTypeID = product.HairTypeID,
                        hairTypeName = product.HairType?.HairTypeName,
                        //discountID = product.Discount.DiscountID,
                        //discountAmount = product.Discount.DiscountAmount
                    };
                    productModels.Add(productModel);
                }
            }
            return (productModels);
        }

        [HttpGet("/product-get-brands-productTypes-hairTypes")]
        public UnionModel GetBrandsProductTypesHairTypes()
        {
            var union = new UnionModel();

            union.Brands = _brandRepository.GetDataList().Select(b => new BrandModel
            {
                brandID = b.BrandID,
                brandName = b.BrandName
            });

            union.ProductTypes = _productTypeRepository.GetList().Select(b => new ProductTypeModel
            {
                productTypeID = b.ProductTypeID,
                productTypeName = b.ProductTypeName
            });

            union.HairTypes = _hairTypeRepository.GetList().Select(b => new HairTypeModel
            {
                hairTypeID = b.HairTypeID,
                hairTypeName = b.HairTypeName
            });

            return union;
        }

        [HttpGet("/product-get-by-id/{id}")]
        public ActionResult<ProductModel> Get(int id)
        {
            var itemDb = _repository.GetData(id);

            if (itemDb == null)
            {
                return NotFound();
            }

            var itemNew = new ProductModel
            {
                productID = itemDb.ProductID,
                productName = itemDb.ProductName,
                unitPrice = itemDb.UnitPrice,
                countStock = itemDb.CountStock,
                volume = itemDb.Volume,
                brandID = itemDb.BrandID,
                brandName = itemDb.Brand?.BrandName,
                productTypeID = itemDb.ProductTypeID,
                productTypeName = itemDb.ProductType?.ProductTypeName,
                hairTypeID = itemDb.HairTypeID,
                hairTypeName = itemDb.HairType?.HairTypeName
            };

            return itemNew;
        }

        [HttpPost("/product-post")]
        public JsonResult Create(ProductModel model)
        {
            var itemNew = new Product {
                ProductName = model.productName,
                UnitPrice = model.unitPrice,
                CountStock = model.countStock,
                Volume = model.volume,
                BrandID = model.brandID,
                ProductTypeID = model.productTypeID,
                HairTypeID = model.hairTypeID
            };
            _repository.Create(itemNew);
            try
            {
                _repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new JsonResult("Create was not successful");
            }

            return new JsonResult($"Create successful id = { itemNew.ProductID }");
        }

        [HttpPut("/product-put-by-id/{id}")]
        public JsonResult Update(int id, ProductModel model)
        {
            if (id != model.productID)
            {
                return new JsonResult("Id incorrect");
            }
            var itemNew = new Product
            {
                ProductID = model.productID,
                ProductName = model.productName,
                UnitPrice = model.unitPrice,
                CountStock = model.countStock,
                Volume = model.volume,
                BrandID = model.brandID,
                ProductTypeID = model.productTypeID,
                HairTypeID = model.hairTypeID
            };

            _repository.Update(itemNew);

            try
            {
                _repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new JsonResult("Update was not successful");
            }

            return new JsonResult($"Update successful");
        }

        [HttpDelete("/product-delete-by-id/{id}")]
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

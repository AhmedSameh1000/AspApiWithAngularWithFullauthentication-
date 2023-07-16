using JWTApi.Data;
using JWTApi.Models;
using JWTApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWTApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _host;

        public ProductController(ApplicationDbContext applicationDbContext, IWebHostEnvironment host)
        {
            _applicationDbContext = applicationDbContext;
            _host = host;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var allproduct = _applicationDbContext.Products.Include(c => c.Category).Include(b => b.Brand).Select(p => new
            {
                id = p.Id,
                name = p.Name,
                price = p.Price,
                quantity = p.Quantity,
                category = p.Category.Name,
                brand = p.Brand.Name,
                image = p.Iamge,
                categoryId = p.CategoryId,
                brandId = p.BrandId
            }).ToList();

            return Ok(allproduct);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromForm] ProductVM productVM)
        {
            string RootPath = _host.WebRootPath;
            var ImageUrl = "";
            if (productVM.File != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var productsFolderPath = Path.Combine(RootPath, @"images/products");
                var extension = Path.GetExtension(productVM.File.FileName);

                using (var fileStreams = new FileStream(Path.Combine(productsFolderPath,
                    fileName + extension), FileMode.Create))
                {
                    productVM.File.CopyTo(fileStreams);
                }
                ImageUrl = @$"{Request.Scheme}://{Request.Host}/images/products/" + fileName + extension;
            }
            var Product = new Product
            {
                Name = productVM.Name,
                Price = productVM.Price,
                Quantity = productVM.Quantity,
                Iamge = ImageUrl,
                CategoryId = productVM.CategoryId,
                BrandId = productVM.BrandId
            };
            _applicationDbContext.Products.Add(Product);
            _applicationDbContext.SaveChanges();
            return Ok(Product);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_applicationDbContext.Products.Find(id));
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update([FromRoute] int id, [FromForm] ProductVM productVM)
        {
            var Product = _applicationDbContext.Products.Find(id);
            if (Product is null)
                return NotFound(Product);
            var Image = Product.Iamge;
            string RootPath = _host.WebRootPath.Replace("\\\\", "\\");
            if (productVM.File != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var productsFolderPath = Path.Combine(RootPath, @"images/products");
                var extension = Path.GetExtension(productVM.File.FileName);

                if (productVM.File != null)
                {
                    var imageNameToDelete = System.IO.Path.GetFileNameWithoutExtension(Product.Iamge);
                    var EXT = Path.GetExtension(Image);
                    var oldImagePath = $@"{RootPath}\images\products\{imageNameToDelete}{EXT}";

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var fileStreams = new FileStream(Path.Combine(productsFolderPath,
                    fileName + extension), FileMode.Create))
                {
                    productVM.File.CopyTo(fileStreams);
                }
                Image = $"{Request.Scheme}:/{Request.Host}/images/products/" + fileName + extension;
            }
            Product.Name = productVM.Name;
            Product.Price = productVM.Price;
            Product.Quantity = productVM.Quantity;
            Product.CategoryId = productVM.CategoryId;
            Product.BrandId = productVM.BrandId;
            Product.Iamge = Image;

            _applicationDbContext.SaveChanges();
            return Ok(Product);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var product = _applicationDbContext.Products.Find(id);
            if (product is null)
            {
                return NotFound();
            }
            string RootPath = _host.WebRootPath.Replace("\\\\", "\\");
            var imageNameToDelete = System.IO.Path.GetFileNameWithoutExtension(product.Iamge);
            var EXT = Path.GetExtension(product.Iamge);
            var oldImagePath = $@"{RootPath}\images\products\{imageNameToDelete}{EXT}";
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _applicationDbContext.Products.Remove(product);

            _applicationDbContext.SaveChanges();
            return Ok(product);
        }
    }
}
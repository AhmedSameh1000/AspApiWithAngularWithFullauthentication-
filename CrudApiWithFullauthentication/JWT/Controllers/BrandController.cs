using JWTApi.Data;
using JWTApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JWTApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class BrandController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BrandController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_applicationDbContext.Brands.ToList());
        }

        [HttpPost("Create")]
        public IActionResult Create(ViewModel viewModel)
        {
            var Brand = new Brand
            {
                Name = viewModel.Name
            };
            _applicationDbContext.Brands.Add(Brand);
            _applicationDbContext.SaveChanges();
            return Ok(Brand);
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update([FromRoute] int id, ViewModel viewModel)
        {
            var Brand = _applicationDbContext.Brands.Find(id);
            if (Brand is null)
                return NotFound(Brand);

            Brand.Name = viewModel.Name;
            _applicationDbContext.SaveChanges();
            return Ok(Brand);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var Brand = _applicationDbContext.Brands.Find(id);
            if (Brand is null)
                return NotFound(Brand);

            _applicationDbContext.Brands.Remove(Brand);
            _applicationDbContext.SaveChanges();
            return Ok(Brand);
        }
    }
}
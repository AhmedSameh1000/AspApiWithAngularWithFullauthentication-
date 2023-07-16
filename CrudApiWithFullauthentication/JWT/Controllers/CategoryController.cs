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
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CategoryController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_applicationDbContext.Categories.ToList());
        }

        [HttpPost("Create")]
        public IActionResult Create(ViewModel viewModel)
        {
            var Category = new Category()
            {
                Name = viewModel.Name
            };
            _applicationDbContext.Categories.Add(Category);
            _applicationDbContext.SaveChanges();
            return Ok(Category);
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update([FromRoute] int id, ViewModel viewModel)
        {
            var Category = _applicationDbContext.Categories.Find(id);
            if (Category is null)
                return NotFound(Category);

            Category.Name = viewModel.Name;
            _applicationDbContext.SaveChanges();
            return Ok(Category);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Update([FromRoute] int id)
        {
            var Category = _applicationDbContext.Categories.Find(id);
            if (Category is null)
                return NotFound(Category);

            _applicationDbContext.Categories.Remove(Category);
            _applicationDbContext.SaveChanges();
            return Ok(Category);
        }
    }
}
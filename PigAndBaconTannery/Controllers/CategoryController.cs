using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PigAndBaconTannery.Repositories;

namespace PigAndBaconTannery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepo = categoryRepository;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_categoryRepo.GetAll());
        }
    }
}

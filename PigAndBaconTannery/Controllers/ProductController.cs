using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PigAndBaconTannery.Models;
using PigAndBaconTannery.Repositories;

namespace PigAndBaconTannery.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        //api/product/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_productRepository.GetAll());
        }

        [HttpPost]
        public void Add(Product product)
        {
            _productRepository.Add(product);
        }
    }
}

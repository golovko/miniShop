
using System;
using Api.Data;
using Api.Models;
using Api.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Api.Logging;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("Api/Products")]
    [ApiController]
    public class ApiProductController : ControllerBase
    {
        private readonly ILogging _logger;
        private DbConnect _dbConnect;

        public ApiProductController(ILogging logger, DbConnect dbConnect)
        {
            _logger = logger;
            _dbConnect = dbConnect;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ProductDto>> GetProducts()
        {
            var products = from p in _dbConnect.Products
                           select new ProductDto()
                           {
                               Id = p.Id,
                               Category = p.Category,
                               Name = p.Name,
                               Manufacturer = p.Manufacturer,
                               ShortDescription = p.ShortDescription,
                               Description = p.Description,
                               Price = p.Price,
                               Currency = p.Currency,
                               ItemsLeft = p.ItemsLeft,
                               MainImage = p.MainImage
                           };

            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetProductById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProductDto> GetProduct(int id)
        {

            var p = _dbConnect.Products.AsNoTracking().FirstOrDefault(p => p.Id == id);

            if (id <= 0)
            {
                _logger.Log("Wrong ID", LogType.Error);
                return BadRequest("Id must be greater than 0!");
            }
            if (p == null)
                return NotFound($"There no product with requested ID {id}");

            ProductDto model = new ProductDto()
            {
                Id = p.Id,
                Category = p.Category,
                Name = p.Name,
                Manufacturer = p.Manufacturer,
                ShortDescription = p.ShortDescription,
                Description = p.Description,
                Price = p.Price,
                Currency = p.Currency,
                ItemsLeft = p.ItemsLeft,
                MainImage = p.MainImage
            };
            _logger.Log("Success", LogType.Message);
            return Ok(model);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ProductDto> AddProduct([FromBody] ProductDto p)
        {
            if (p == null)
                return BadRequest(p);

            if (p.Id > 0)
                return StatusCode(StatusCodes.Status500InternalServerError);

            Product model = new Product()
            {
                Category = p.Category,
                Name = p.Name,
                Manufacturer = p.Manufacturer,
                ShortDescription = p.ShortDescription,
                Description = p.Description,
                Price = p.Price,
                Currency = p.Currency,
                ItemsLeft = p.ItemsLeft,
                MainImage = p.MainImage,
                CreatedDateTime = DateTime.Now
            };
            _dbConnect.Products.Add(model);
            _dbConnect.SaveChanges();

            var res = _dbConnect.People.OrderBy(p => p.Id).Last();
            ProductDto modelDto = new ProductDto()
            {
                Id = p.Id,
                Category = p.Category,
                Name = p.Name,
                Manufacturer = p.Manufacturer,
                ShortDescription = p.ShortDescription,
                Description = p.Description,
                Price = p.Price,
                Currency = p.Currency,
                ItemsLeft = p.ItemsLeft,
                MainImage = p.MainImage
            };
            return CreatedAtRoute("GetProductById", new { id = model.Id }, modelDto);

        }

        [HttpDelete("{id:int}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteProduct(int id)
        {
            var list = _dbConnect.Products;
            var res = list.FirstOrDefault(p => p.Id == id);

            if (id <= 0)
                return BadRequest("Id must be greater than 0!");
            if (res == null)
                return NotFound($"There no person with requested ID {id}");

            _dbConnect.Products.Remove(res);
            _dbConnect.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateProduct(int id, [FromBody] Product p)
        {

            if (id <= 0 || id != p.Id || p == null)
                return BadRequest();

            Product model = new Product()
            {
                Category = p.Category,
                Name = p.Name,
                Manufacturer = p.Manufacturer,
                ShortDescription = p.ShortDescription,
                Description = p.Description,
                Price = p.Price,
                Currency = p.Currency,
                ItemsLeft = p.ItemsLeft,
                MainImage = p.MainImage,
                CreatedDateTime = DateTime.Now
            };

            _dbConnect.Update(model);
            _dbConnect.SaveChanges();
            return NoContent();

        }
    }
}


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
    [Route("Api/Orders")]
    [ApiController]
    public class ApiOrderController : ControllerBase
    {
        private readonly ILogging _logger;
        private DbConnect _dbConnect;

        public ApiOrderController(ILogging logger, DbConnect dbConnect)
        {
            _logger = logger;
            _dbConnect = dbConnect;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<OrderDto>> GetOrders()
        {
            var orders = from p in _dbConnect.Orders.Include(b => b.Buyer).Include(pr => pr.Products)
                         select new OrderDto()
                         {
                             Id = p.Id,
                             BuyerId = p.Buyer.Id,
                             CreatedDateTime = p.CreatedDateTime,
                             OrderStatus = p.OrderStatus,
                             Payed = p.Payed,
                             OrderSum = p.OrderSum

                         };


            return Ok(orders);
        }

        [HttpGet("{id:int}", Name = "GetOrderById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<OrderDto> GetOrder(int id)
        {

            var p = _dbConnect.Orders.Include(b => b.Buyer).Include(pr => pr.Products).AsNoTracking().FirstOrDefault(p => p.Id == id);

            if (id <= 0)
            {
                _logger.Log("Wrong ID", LogType.Error);
                return BadRequest("Id must be greater than 0!");
            }
            if (p == null)
                return NotFound($"There no order with requested ID {id}");

            OrderDto model = new OrderDto()
            {
                Id = p.Id,
                BuyerId = p.Buyer.Id,
                OrderSum = p.OrderSum,
                OrderStatus = p.OrderStatus,
                Payed = p.Payed,
                CreatedDateTime = p.CreatedDateTime,
                ProductsId = new List<int>()
            };

            var products = p.Products.ToList();
            for (int i = 0; i < products.Count; i++)
            {
                model.ProductsId.Add(products[i].Id);
            }

            _logger.Log("Success", LogType.Message);
            return Ok(model);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OrderDto> AddOrder([FromBody] OrderDto p)
        {
            if (p == null)
                return BadRequest(p);

            if (p.Id > 0)
                return StatusCode(StatusCodes.Status500InternalServerError);


            Order model = new Order()
            {
                //Id = p.Id,
                OrderSum = p.OrderSum,
                OrderStatus = p.OrderStatus,
                Payed = p.Payed,
                CreatedDateTime = DateTime.Now,

            };
            //нахожу покупателя по id
            model.Buyer = _dbConnect.People.AsNoTracking().FirstOrDefault(u => u.Id == p.BuyerId);

            //нахожу товары из заказа по id
            List<Product> temp = new List<Product>();
            foreach (var item in p.ProductsId)
            {
                temp.Add(_dbConnect.Products.AsNoTracking().FirstOrDefault(p => p.Id == item));  
            }
            model.Products = (ICollection<Product>)temp;

            _dbConnect.Orders.Add(model);
            _dbConnect.SaveChanges();

            var res = _dbConnect.Orders.OrderBy(p => p.Id).Last();
            OrderDto modelDto = new OrderDto()
            {
                Id = p.Id,
                OrderSum = p.OrderSum,
                OrderStatus = p.OrderStatus,
                Payed = p.Payed,
                CreatedDateTime = p.CreatedDateTime

            };
            return CreatedAtRoute("GetOrderById", new { id = model.Id }, modelDto);

        }

        /*

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
      */
    }
}


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
        public ActionResult<OrderDto> AddOrder([FromBody] OrderDto orderDto)
        {
            if (orderDto == null)
                return BadRequest(orderDto);

            if (orderDto.Id > 0)
                return StatusCode(StatusCodes.Status500InternalServerError);


            Order model = new Order()
            {
                //Id = p.Id,
                //OrderSum = orderDto.OrderSum,
                OrderStatus = orderDto.OrderStatus,
                Payed = orderDto.Payed,
                CreatedDateTime = DateTime.Now,

            };
            _dbConnect.Orders.Add(model);

            //нахожу покупателя по id
            var buyer = _dbConnect.People.AsNoTracking().Include(o => o.Orders).FirstOrDefault(u => u.Id == orderDto.BuyerId);
            model.Buyer = buyer;

            _dbConnect.SaveChanges();

            //нахожу товары из заказа по id
            List<Product> productsInOrder = new List<Product>();
            foreach (var item in orderDto.ProductsId)
            {
                Product product = _dbConnect.Products.AsNoTracking().FirstOrDefault(p => p.Id == item);
                model.OrderSum += product.Price;
                productsInOrder.Add(product);

            }
            model.Products = productsInOrder;
            _dbConnect.SaveChanges();


            //geting result from the db to check that the data was saved properly
            var res = _dbConnect.Orders.AsNoTracking().Include(p => p.Buyer).Include(g => g.Products).OrderBy(p => p.Id).Last();
            OrderDto modelDto = new OrderDto()
            {
                Id = res.Id,
                OrderSum = res.OrderSum,
                OrderStatus = res.OrderStatus,
                Payed = res.Payed,
                CreatedDateTime = res.CreatedDateTime,
                BuyerId = res.Buyer.Id
            };
            var productsId = new List<int>();
            foreach (var item in res.Products)
            {
                productsId.Add(item.Id);
            }
            modelDto.ProductsId = productsId;
            return CreatedAtRoute("GetOrderById", new { id = res.Id }, modelDto);

        }
    }
}


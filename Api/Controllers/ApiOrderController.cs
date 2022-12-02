using System;
using Api.Data;
using Api.Models;
using Api.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Api.Logging;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Newtonsoft.Json;

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
        public ActionResult<IEnumerable<Order>> GetOrders()
        {
          
            var ordersFull = from p in _dbConnect.Orders
                             select new Order()
                         {
                             Id = p.Id,
                             CreatedDateTime = p.CreatedDateTime,
                             OrderStatus = p.OrderStatus,
                             Payed = p.Payed,
                             OrderSum = p.OrderSum,
                             Buyer = p.Buyer,
                              Products = p.Products,
                         };

            return Ok(ordersFull);
        }

        [HttpGet("{id:int}", Name = "GetOrderById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Order> GetOrder(int id)
        {
         
            var p = _dbConnect.Orders.AsNoTracking().Include(u=>u.Buyer).Include(g=>g.Products).FirstOrDefault(p => p.Id == id);

            Order order = new()
            {
                 Id = p.Id, Buyer = p.Buyer, CreatedDateTime = p.CreatedDateTime, OrderStatus = p.OrderStatus,
                  OrderSum = p.OrderSum, Payed = p.Payed , Products = p.Products,  UpdatedDateTime = p.UpdatedDateTime 

            };
            if (id <= 0)
            {
                _logger.Log("Wrong ID", LogType.Error);
                return BadRequest("Id must be greater than 0!");
            }
            if (p == null)
                return NotFound($"There no order with requested ID {id}");


            _logger.Log("Success", LogType.Message);
            return Ok(order);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OrderDto> AddOrder([FromBody] OrderDto order)
        {
            if (order == null)
                return BadRequest(order);

            if (order.Id > 0)
                return StatusCode(StatusCodes.Status500InternalServerError);


            Order model = new Order()
            {
                //Id = p.Id,
                //OrderSum = orderDto.OrderSum,
                OrderStatus = order.OrderStatus,
                Payed = order.Payed,
                CreatedDateTime = DateTime.Now,

            };
            _dbConnect.Orders.Add(model);

            //нахожу покупателя по id
            var buyer = _dbConnect.People.AsNoTracking().Include(o => o.Orders).FirstOrDefault(u => u.Id == order.BuyerId);
            model.Buyer = buyer;

            _dbConnect.SaveChanges();

            //нахожу товары из заказа по id
            List<Product> productsInOrder = new List<Product>();
            foreach (var item in order.ProductsId)
            {
                Product product = _dbConnect.Products.AsNoTracking().FirstOrDefault(p => p.Id == item);
                model.OrderSum += product.Price;
                productsInOrder.Add(product);

            }
            model.Products = productsInOrder;
            _dbConnect.SaveChanges();


            //geting result from the db to check that the data was saved properly
            var res = _dbConnect.Orders.AsNoTracking().Include(p => p.Buyer).Include(g => g.Products).OrderBy(p => p.Id).Last();
            OrderDto orderDto = new()
            {
                Id = res.Id, BuyerId = res.Buyer.Id, CreatedDateTime = res.CreatedDateTime
                , OrderStatus = res.OrderStatus, OrderSum = res.OrderSum, Payed = res.Payed,
                ProductsId = new List<int>(),
            };

            List<int> productsIds= new ();
            foreach (var item in res.Products)
            {
                productsIds.Add(item.Id);
            }
            orderDto.ProductsId = productsIds;
            return CreatedAtRoute("GetOrderById", new { id = res.Id }, orderDto);

        }
    }
}


using System;
namespace Api.Models.Dto
{
	public class OrderDto
	{
        public int Id { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Payed Payed { get; set; }
        public int BuyerId { get; set; }
        public List<int> ProductsId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public double OrderSum { get; set; }


	}

}


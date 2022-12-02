using System;
namespace App.Model
{
	public class Order
	{
        public int Id { get; set; }
        public double OrderSum { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Payed Payed { get; set; }
        public Person? Buyer { get; set; }
        public ICollection<Product>? Products { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }

    }

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


    public enum OrderStatus
    {
        Created,
        Approved,
        Canceled,
        Collected,
        OnTransit,
        Delivered,
        Returned
    }

    public enum Payed
    {
        Payed,
        Unpayed,
        Moneyback
    }
}


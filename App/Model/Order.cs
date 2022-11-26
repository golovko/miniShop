using System;
namespace App.Model
{
	public class Order
	{
        public int Id { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Payed Payed { get; set; }
        public int BuyerId { get; set; }
        public List<int> ProductsId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public double OrderSum { get; set; }
        public Order()
		{
		}
	}
    public class OrderFull : Order
    {
        public Person UserInfo { get; set; }
        public List<Product> productsInfo { get; set; }
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


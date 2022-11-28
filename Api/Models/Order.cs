using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
	public class Order
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double OrderSum { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Payed Payed { get; set; }
        public Person? Buyer { get; set; }
        public ICollection<Product>? Products { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }

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


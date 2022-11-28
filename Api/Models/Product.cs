using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
	public class Product
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Manufacturer { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? MainImage { get; set; }
        public double Price { get; set; }
        public string? Currency { get; set; }
        public int ItemsLeft { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}


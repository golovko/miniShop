using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models.Dto
{
    public class PersonDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string Ocupation { get; set; }
        public string Image { get; set; }
    }  
}


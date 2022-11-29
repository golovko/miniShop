using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Dto
{
	public class AuthDto
	{
        
        public int Id { get; set; }
		public string? Login { get; set; }
		public string? Pass { get; set; }
		public PersonDto? personDto { get; set; }
    }
}


using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Model;

namespace App
{

	public class Auth
	{

		public int Id { get; set; }
		public string? Login { get; set; }
		public string? Pass { get; set; }
		public Person? Person { get; set; }

	}
}


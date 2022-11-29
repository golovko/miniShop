using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Model
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string Ocupation { get; set; }
        public string Image { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public Auth Auth { get; set; }
    }
    public class PersonDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string website { get; set; }
        public string ocupation { get; set; }
        public string image { get; set; }
        public Auth Auth { get; set; }
    }
}


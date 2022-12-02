using System;
using Api.Data;
using Api.Models;
using Api.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Api.Logging;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("Api/People")]
    [ApiController]
    public class ApiPersonController : ControllerBase
    {
        private readonly ILogging _logger;
        private DbConnect _dbConnect;

        public ApiPersonController(ILogging logger, DbConnect dbConnect)
        {
            _logger = logger;
            _dbConnect = dbConnect;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PersonDto>> GetPeople()
        {
            //var people = _dbConnect.People;
            var people = from p in _dbConnect.People
                         select new PersonDto()
                         {
                             Id = p.Id,
                             Name = p.Name,
                             Address = p.Address,
                             Email = p.Email,
                             Image = p.Image,
                             Ocupation = p.Ocupation,
                             Website = p.Website
                         };

            return Ok(people);
        }
      

        [HttpGet("{id:int}",Name ="GetPersonById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PersonDto> GetPerson(int id)
        {
            
            var person = _dbConnect.People.AsNoTracking().FirstOrDefault(p => p.Id == id);
            
            if (id <= 0)
            {
                _logger.Log("Wrong ID", LogType.Error);
                return BadRequest("Id must be greater than 0!");
            }
            if (person == null)
                return NotFound($"There no person with requested ID {id}");

            PersonDto model = new PersonDto()
            {
                Id = person.Id,
                Name = person.Name,
                Address = person.Address,
                Email = person.Email,
                Image = person.Image,
                Ocupation = person.Ocupation,
                Website = person.Website
            };
            _logger.Log("Success", LogType.Message);
            return Ok(model);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PersonDto> AddPerson([FromBody]PersonDto person)
        {
            if (person == null)
                return BadRequest(person);

            if(person.Id > 0)
                return StatusCode(StatusCodes.Status500InternalServerError);

            Person model = new Person()
                         {
                             Name = person.Name,
                             Address = person.Address,
                             CreatedDateTime = DateTime.Now,
                             Email = person.Email,
                             Image = person.Image,
                             Ocupation = person.Ocupation,
                             Website = person.Website,
                              Auth = person.Auth
                         };

            _dbConnect.People.Add(model);
            _dbConnect.SaveChanges();
            var res = _dbConnect.People.OrderBy(p=> p.Id).Last();
            PersonDto modelDto = new PersonDto()
            {
                Id = res.Id,
                Name = res.Name,
                Address = res.Address,
                Email = res.Email,
                Image = res.Image,
                Ocupation = res.Ocupation,
                Website = res.Website
            };
            return CreatedAtRoute("GetPersonById", new { id = model.Id }, modelDto);

        }

        [HttpDelete("{id:int}", Name = "DeletePerson")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeletePerson(int id)
        {
            var list = _dbConnect.People;
            var res = list.FirstOrDefault(p => p.Id == id);

            if (id <= 0)
                return BadRequest("Id must be greater than 0!");
            if (res == null)
                return NotFound($"There no person with requested ID {id}");

            _dbConnect.People.Remove(res);
            _dbConnect.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id:int}",Name = "UpdatePerson")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePerson(int id, [FromBody]PersonDto person)
        {
            

            if (id <= 0 || id != person.Id || person == null)
                return BadRequest();

            Person model = new Person()
            {
                Id = person.Id,
                Name = person.Name,
                Address = person.Address,
                Email = person.Email,
                Image = person.Image,
                Ocupation = person.Ocupation,
                Website = person.Website,
                UpdatedDateTime = DateTime.Now
                
            };

            _dbConnect.Update(model);
            _dbConnect.SaveChanges();
            return NoContent();

        }

        //[HttpPatch("{id:int}", Name = "UpdatePartialPerson")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult UpdatePartialPerson(int id, JsonPatchDocument<PersonDto> patchDocument)
        //{
        //    var list = PersonData.People;
        //    var res = list.FirstOrDefault(p => p.Id == id);

        //    if (id <= 0 || res == null)
        //        return BadRequest();
        //    patchDocument.ApplyTo(res, ModelState);
        //    if(!ModelState.IsValid)
        //        return BadRequest();

        //    return NoContent();

        //}

    }
}


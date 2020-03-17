using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cwiczenie3ABDP.DAL;
using Cwiczenie3ABDP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Cwiczenie3ABDP.Controllers
{
    [Route("api/students")]
    [ApiController]
    
    public class StudentsController : ControllerBase
    {



        private readonly IDbService _dbService;

        public StudentsController(IDbService dbService)
        {
            _dbService =dbService;
        }
       
        [HttpGet]
        public IActionResult GetStudent()
        {
            return Ok(_dbService.GetStudents());
        }
        [HttpPost]
        public IActionResult CreateStudent(Student Student)
        {
            Student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(Student);
        }
        [HttpPut]
        public IActionResult PutStudent(string id)
        {
            return Ok($"Aktualizacja dokończona {id}");
        }
        [HttpDelete]
        public IActionResult DeleteStudent(string id)
        {
            return Ok($"Usuwanie dokończone {id}");
        }
        
    }
}
    
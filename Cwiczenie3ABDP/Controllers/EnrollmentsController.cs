using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cwiczenie3ABDP.DAL;
using Cwiczenie3ABDP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenie3ABDP.Controllers
{
    
    
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {

        private readonly IDbService _dbService;

        public EnrollmentsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [Route("api/enrollments")]
        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            try
            {
                return Created("",_dbService.EnrollStudent(student));
            }
            catch(Exception e)
            {
                Console.WriteLine("Cos poszlo nie tak!!!");
                Console.WriteLine(e.StackTrace);
                return StatusCode(400);
            }
            return Ok(student);
        }
        [Route("api/enrollments/promotions")]
        [HttpPost]
        public IActionResult PromoteStudent(Promotion promotion)
        {
            //try
            //{
                return Created("", _dbService.PromoteStudent(promotion));
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("Cos poszlo nie tak!!!");
            //    Console.WriteLine(e.StackTrace);
            //    return StatusCode(400);
            //}
            return Ok(promotion);
        }
    }
}
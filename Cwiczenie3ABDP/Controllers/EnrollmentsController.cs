using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cwiczenie3ABDP.DAL;
using Cwiczenie3ABDP.DTO;
using Cwiczenie3ABDP.Models;
using Microsoft.AspNetCore.Authorization;
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
        public IActionResult CreateStudent(EnrollStudentReq req)
        {
            try
            {
                return Created("",_dbService.EnrollStudent(req));
            }
            catch(Exception e)
            {
                Console.WriteLine("Cos poszlo nie tak!!!");
                Console.WriteLine(e.StackTrace);
                return StatusCode(400);
            }
          
        }
        [Route("api/enrollments/promotions")]
        [HttpPost]
        public IActionResult PromoteStudent(PromoteStudentReq promotion)
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
           
        }
    }
}
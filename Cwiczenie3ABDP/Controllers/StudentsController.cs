using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            _dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetStudent()
        {
            List<Object> list = new List<object>();
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18734;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                com.CommandText = "select a.FirstName, a.LastName, a.BirthDate, b.Semester, c.Name" +
                    "               from Student a inner join Enrollment b on a.IdEnrollment = b.IdEnrollment" +
                    "                              inner join Studies c on b.IdStudy = c.IdStudy";

                client.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {

                    var st = new
                    {
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        BirthDate = DateTime.Parse(dr["BirthDate"].ToString()),
                        Semester = dr["Semester"].ToString(),
                        Name = dr["Name"].ToString()
                    };
                    list.Add(st);

                    //...
                }

            }
            return Ok(list);

        }
        [HttpGet("{id}")]
        public IActionResult GetStudent(string Id)
        {
            List<Object> list = new List<object>();
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18734;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                com.CommandText = "select a.FirstName, a.LastName, a.BirthDate, b.Semester, c.Name" +
                    "               from Student a inner join Enrollment b on a.IdEnrollment = b.IdEnrollment" +
                    "                              inner join Studies c on b.IdStudy = c.IdStudy where" +
                    "                              a.IndexNumber = @id";
                com.Parameters.AddWithValue("id", Id);

                client.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {

                    var st = new
                    {
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        BirthDate = DateTime.Parse(dr["BirthDate"].ToString()),
                        Semester = dr["Semester"].ToString(),
                        Name = dr["Name"].ToString()
                    };
                    list.Add(st);

                  
                }

            }
            return Ok(list);

        }

        private void Open()
        {
            throw new NotImplementedException();
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
    
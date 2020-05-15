using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Cwiczenie3ABDP.DAL;
using Cwiczenie3ABDP.DTO;
using Cwiczenie3ABDP.Models;
using Cwiczenie3ABDP.Models_Zadanie10;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;


namespace Cwiczenie3ABDP.Controllers
{
    [Route("api/students")]
    [ApiController]

    public class StudentsController : ControllerBase
    {


        public IConfiguration Configuration { get; set; }

        private readonly IDbService _dbService;

        public StudentsController(IDbService dbService, IConfiguration configuration)
        {
            _dbService = dbService;
            Configuration = configuration;
        }
        [HttpGet]
        public IActionResult GetStudent(Models_Zadanie10.Student student)
        {
            if (!_dbService.CheckIfStudentsExists())
            {
                return Forbid();
            }
            return Ok(_dbService.GetStudents_zad10());
        }
        [HttpPut("{id}")]
        public IActionResult ChangeStudent(Models_Zadanie10.Student student, string id)
        {
            if (!_dbService.checkIfStudentExist(id))
            {
                return Forbid();
            }
            _dbService.modifyStudent(student, id);
            return Ok("Aktualizacja dokonana");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(string id)
        {
            if (!_dbService.checkIfStudentExist(id))
            {
                return Forbid();
            }
            _dbService.removeStudent(id);
            return Ok("Aktualizacja dokonana");
        }

        //[HttpGet]
        //[Authorize(Roles = "admin")]
        //public IActionResult GetStudent()
        //{
        //    List<Object> list = new List<object>();
        //    using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18734;Integrated Security=True"))
        //    using (var com = new SqlCommand())
        //    {
        //        com.Connection = client;
        //        com.CommandText = "select a.FirstName, a.LastName, a.BirthDate, b.Semester, c.Name" +
        //            "               from Student a inner join Enrollment b on a.IdEnrollment = b.IdEnrollment" +
        //            "                              inner join Studies c on b.IdStudy = c.IdStudy";

        //        client.Open();
        //        var dr = com.ExecuteReader();
        //        while (dr.Read())
        //        {

        //            var st = new
        //            {
        //                FirstName = dr["FirstName"].ToString(),
        //                LastName = dr["LastName"].ToString(),
        //                BirthDate = DateTime.Parse(dr["BirthDate"].ToString()),
        //                Semester = dr["Semester"].ToString(),
        //                Name = dr["Name"].ToString()
        //            };
        //            list.Add(st);

        //            //...
        //        }

        //    }
        //    return Ok(list);

        //}
        //[HttpGet("{id}")]
        //public IActionResult GetStudent(string Id)
        //{
        //    List<Object> list = new List<object>();
        //    using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18734;Integrated Security=True"))
        //    using (var com = new SqlCommand())
        //    {
        //        com.Connection = client;
        //        com.CommandText = "select a.FirstName, a.LastName, a.BirthDate, b.Semester, c.Name" +
        //            "               from Student a inner join Enrollment b on a.IdEnrollment = b.IdEnrollment" +
        //            "                              inner join Studies c on b.IdStudy = c.IdStudy where" +
        //            "                              a.IndexNumber = @id";
        //        com.Parameters.AddWithValue("id", Id);

        //        client.Open();
        //        var dr = com.ExecuteReader();
        //        while (dr.Read())
        //        {

        //            var st = new
        //            {
        //                FirstName = dr["FirstName"].ToString(),
        //                LastName = dr["LastName"].ToString(),
        //                BirthDate = DateTime.Parse(dr["BirthDate"].ToString()),
        //                Semester = dr["Semester"].ToString(),
        //                Name = dr["Name"].ToString()
        //            };
        //            list.Add(st);


        //        }

        //    }
        //    return Ok(list);

        //}

        //private void Open()
        //{
        //    throw new NotImplementedException();
        //}

        //[HttpPost]
        //public IActionResult CreateStudent(Models.Student Student)
        //{
        //    Student.IndexNumber = $"s{new Random().Next(1, 20000)}";
        //    return Ok(Student);
        //}
        //[HttpPost]
        //[Route("login")]
        //public IActionResult Login(LoginRequestDTO request)
        //{

        //    if (!_dbService.CheckCredentials(request))
        //    {
        //        return Unauthorized();
        //    }
        //    Console.WriteLine(_dbService.CreateSalt());
        //    return Ok(NowyToken(request.Login));
        //}
        //[HttpPost("refreshToken")]
        //public IActionResult RefreshToken(string refreshToken)
        //{
        //    string login = _dbService.CheckRefTok(refreshToken);
        //    if (login == "")
        //    {
        //        return Unauthorized();
        //   }

        //   return Ok(NowyToken(login));
        //}
        //public object NowyToken(string login)
        //{
        //    var claims = new[] {

        //                new Claim(ClaimTypes.NameIdentifier,"1"),
        //                new Claim(ClaimTypes.Name,"Jan"),
        //                new Claim(ClaimTypes.Role, "admin"),
        //                new Claim(ClaimTypes.Role, "student"),
        //                new Claim(ClaimTypes.Role, "employee")
        //                };
        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //            issuer: "Gakko",
        //            audience: "student",
        //            claims: claims,
        //            expires: DateTime.Now.AddMinutes(10),
        //            signingCredentials: creds
        //            );

        //    var newToken = Guid.NewGuid();
        //    _dbService.AddRefreshToken(newToken, login);

        //    return Ok(new
        //    {
        //        token = new JwtSecurityTokenHandler().WriteToken(token),
        //        refreshToken = newToken
        //    }
        //  ) ;
        //}

        //[HttpPut]
        //public IActionResult PutStudent(string id)
        //{
        //    return Ok($"Aktualizacja dokończona {id}");
        //}
        //[HttpDelete]
        //public IActionResult DeleteStudent(string id)
        //{
        //    return Ok($"Usuwanie dokończone {id}");
        //}
        
    }
}
    
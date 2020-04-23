using Cwiczenie3ABDP.DTO;
using Cwiczenie3ABDP.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cwiczenie3ABDP.DAL
{
    public class DBServiceMS : IDbService
    {


        public Enrollment EnrollStudent(Student student)
        {
            using (var polaczenie = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18734;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                polaczenie.Open();
                SqlTransaction transaction = polaczenie.BeginTransaction();
                try
                {

                    com.Transaction = transaction;
                    com.Connection = polaczenie;
                    com.CommandText = $"select count(1) from Studies where name=@name";

                    com.Parameters.AddWithValue("name", student.Studies);



                    int id = 0;

                    var dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        int v = (int)dr.GetValue(0);
                        id = v;
                    }
                    if (id == -1) throw new Exception("Student nie istnieje");

                    dr.Close();


                    com.CommandText = $"select idEnrollment from Enrollment where idStudy = @id and Semester = 1";
                    com.Parameters.AddWithValue("id", id);

                    dr = com.ExecuteReader();


                    int idstudy = -1;


                    while (dr.Read())
                    {
                        idstudy = (int)dr.GetValue(0);

                    }
                    int maxid = 1;

                    if (idstudy == -1)
                    {
                        dr.Close();
                        com.CommandText = $"select max(idEnrollment)+1 from Enrollment";



                        dr = com.ExecuteReader();

                        while (dr.Read())
                        {
                            maxid = (int)dr.GetValue(0);

                        }

                        DateTime date = DateTime.Now;
                        dr.Close();
                        com.CommandText = $"insert into Enrollment values (@idEnrollment, 1, @idStudy, @StartDate)";
                        com.Parameters.AddWithValue("idEnrollment", maxid);
                        com.Parameters.AddWithValue("idStudy", idstudy);
                        com.Parameters.AddWithValue("StartDate", date);
                        com.ExecuteNonQuery();

                    }

                    //dodajemy nowego studenta
                    dr.Close();
                    com.CommandText = $"insert into Student values (@id2, @firstname2, @lastname2, @birthdate2, @enrollment2)";
                    com.Parameters.AddWithValue("id2", student.IndexNumber.ToString());
                    com.Parameters.AddWithValue("firstname2", student.FirstName);
                    com.Parameters.AddWithValue("lastname2", student.LastName);
                    com.Parameters.AddWithValue("birthdate2", student.BirthDate);
                    com.Parameters.AddWithValue("enrollment2", maxid);
                    com.ExecuteNonQuery();

                    transaction.Commit();


                    return new Enrollment
                    {
                        IdEnrollment = maxid,
                        Semester = 1,
                        IdStudy = idstudy,
                        StartDate = DateTime.Now


                    };
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    transaction.Rollback();
                }
                throw new Exception("Cos sie stalo sie");
            }



        }
        public IEnumerable<Student> GetStudents()
        {
            return null;
        }

        public Enrollment PromoteStudent(Promotion promotion)
        {
            using (var polaczenie = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18734;Integrated Security=True"))
            using (var com = new SqlCommand())
            {

                polaczenie.Open();
                com.Connection = polaczenie;
                com.CommandText = $"select count(1) from Enrollment a inner join Studies b on a.idstudy = b.idstudy where b.name=@studies10 and a.semester = @semester10";
                com.Parameters.AddWithValue("studies10", promotion.Studies);
                com.Parameters.AddWithValue("semester10", promotion.Semester);
                
                var dr = com.ExecuteReader();
                int v = -1;
                while (dr.Read())
                {
                    v = (int)dr.GetValue(0);
                }
                if (v == -1) throw new Exception("Student nie istnieje");


                dr.Close();
                com.CommandText = "Exec procedura1 @studies2,@semester2";
                com.Parameters.AddWithValue("studies2", promotion.Studies);
                com.Parameters.AddWithValue("semester2", promotion.Semester);
                com.ExecuteNonQuery();

                dr.Close();
                com.CommandText = $"select max(a.IdEnrollment)+1, a.idstudy from Enrollment a inner join Studies b on a.idstudy = b.idstudy where b.name=@studies3 and a.semester = @semester3 group by a.idstudy";
                com.Parameters.AddWithValue("studies3", promotion.Studies);
                com.Parameters.AddWithValue("semester3", promotion.Semester);
                dr = com.ExecuteReader();
                int idstudy = -1;
                while (dr.Read())
                {
                    v = (int)dr.GetValue(0);
                    idstudy = (int)dr.GetValue(1);
                }
                if (v == -1) throw new Exception("Student nie istnieje");

                return new Enrollment
                {
                    IdEnrollment = v,
                    Semester = 1,
                    IdStudy = idstudy,
                    StartDate = DateTime.Now
                };

            }





        }
        public bool CheckIndex(string index) {
            using (var polaczenie = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18734;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                polaczenie.Open();
                com.Connection = polaczenie;
                com.CommandText = $"select count(1) from Student where IndexNumber = @index";
                com.Parameters.AddWithValue("index", index);

                int id = -1;
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    id =  (int)dr.GetValue(0);
                }
                if (id == -1) return false;


            }
            return true;
        }
        public bool CheckCredentials(LoginRequestDTO request)
        {
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18734;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                client.Open();
                com.Connection = client;
                com.CommandText = "select salt from student where indexnumber = @id2";
                com.Parameters.AddWithValue("id2", request.Login);
                var dr = com.ExecuteReader();
                string haslosalt = "";
                dr.Read();
                haslosalt = dr["Salt"].ToString();
                dr.Close();


                var hash = CreateHash(request.Haslo, haslosalt);

                com.CommandText = "select password from Student where indexnumber=@id2";
      
                dr = com.ExecuteReader();
                dr.Read();
                string haslo = (string)dr.GetValue(0);
                dr.Close();

                if (!haslo.Equals(hash))
                    return false;
                return true;
            }
        }


        public string CheckRefTok(string refTok)
        {
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18734;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                client.Open();
                com.Connection = client;
                com.CommandText = "select indexNumber from student where refreshToken = @reftok";
                com.Parameters.AddWithValue("reftok", refTok);
                var dr = com.ExecuteReader();

                dr.Read();
                string login = "";
                if(dr.HasRows)
                {
                    login = dr["indexNumber"].ToString();

                }

                dr.Close();
                return login;
            }
        }

        public void AddRefreshToken(Guid refreshToken, string login)
        {
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18734;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                client.Open();
                com.Connection = client;
                com.CommandText = "update student set RefreshToken = @token where IndexNumber = @login";
                com.Parameters.AddWithValue("token", refreshToken);
                com.Parameters.AddWithValue("login", login);
                var dr = com.ExecuteNonQuery();
            }
        }

        public string CreateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }
        public string CreateHash(string password, string salt)
        {
            var valueBytes = KeyDerivation.Pbkdf2(
                                    password: password,
                                    salt: Encoding.UTF8.GetBytes(salt),
                                    prf: KeyDerivationPrf.HMACSHA512,
                                    iterationCount: 10000,
                                    numBytesRequested: 256 / 8);

            return Convert.ToBase64String(valueBytes);
        }
        
    }
}

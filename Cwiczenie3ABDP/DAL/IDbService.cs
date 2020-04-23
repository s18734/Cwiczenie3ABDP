using Cwiczenie3ABDP.DTO;
using Cwiczenie3ABDP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenie3ABDP.DAL
{
    public interface IDbService 
    {
        public IEnumerable<Student> GetStudents();
        public Enrollment EnrollStudent(Student student);
        public Enrollment PromoteStudent(Promotion promotion);
        public bool CheckIndex(string index);

        public bool CheckCredentials(LoginRequestDTO request);

        public string CreateSalt();
        public string CreateHash(string password, string salt);

        public void AddRefreshToken(Guid refreshToken, string login);
        public string CheckRefTok(string refTok);
    }
}

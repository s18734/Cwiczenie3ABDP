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
        
        public Models_Zadanie10.Enrollment EnrollStudent(EnrollStudentReq req);
        public Models_Zadanie10.Enrollment PromoteStudent(PromoteStudentReq promotion);
        public bool CheckIndex(string index);

        public bool CheckCredentials(LoginRequestDTO request);

        public string CreateSalt();
        public string CreateHash(string password, string salt);

        public void AddRefreshToken(Guid refreshToken, string login);
        public string CheckRefTok(string refTok);

        public bool CheckIfStudentsExists();

        public IEnumerable<Models_Zadanie10.Student> GetStudents_zad10();
        public bool checkIfStudentExist(string id);
        public void modifyStudent(Models_Zadanie10.Student student, String id);
        public void removeStudent(String id);
    }
}

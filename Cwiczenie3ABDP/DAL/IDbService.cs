﻿using Cwiczenie3ABDP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenie3ABDP.DAL
{
    public interface IDbService 
    {
        public IEnumerable<Student> GetStudents();
    }
}
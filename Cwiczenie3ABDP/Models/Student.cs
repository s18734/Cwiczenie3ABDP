﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenie3ABDP.Models
{
    public class Student
    {
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]

        public string Studies { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string IndexNumber { get; set; }

    }
}

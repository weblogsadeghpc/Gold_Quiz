﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold_Quiz.DataModel.Entities
{
    public class Courses
    {
        [Key]
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string UserID { get; set; }
        [ForeignKey("UserID")] // kelid khareji misazim bar asas users paiini 
        public virtual ApplicationUsers Users { get; set; }
    }
}

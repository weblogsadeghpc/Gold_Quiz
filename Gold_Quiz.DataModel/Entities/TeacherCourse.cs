using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold_Quiz.DataModel.Entities
{
    public class TeacherCourse
    {
        // har teacher che dorosi ro dare
        [Key]
        public int TeacherCourseID { get; set; }
        public string TeacherID { get; set; }
        public int CourseID { get; set; } // id dars 
        public string TeacherAdminID { get; set; } //admin in dars kie
        public int CenterID { get; set; }// in teacher marboot be kodom markaz mishe 

        [ForeignKey("TeacherAdminID")]
        public virtual ApplicationUsers Users_Admin { get; set; }

        [ForeignKey("TeacherID")]
        public virtual ApplicationUsers Users_Teacher { get; set; } // Key khode teacher hast

        [ForeignKey("CenterID")]
        public virtual Centers Centers { get; set; }

        [ForeignKey("CourseID")]
        public virtual Courses Courses { get; set; }

    }
}

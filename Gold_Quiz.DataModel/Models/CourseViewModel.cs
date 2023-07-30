using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold_Quiz.DataModel.Models
{
    public class CourseViewModel
    {
        public int CourseID { get; set; }
        [Display(Name = "نام درس")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "نام درس وارد نشده است .")]
        public string CourseName { get; set; }
        public string UserId { get; set; }
    }
}

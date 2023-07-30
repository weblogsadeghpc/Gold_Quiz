using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Gold_Quiz.DataModel.Entities
{
    public class Centers
    {
        [Key]
        public int CenterID { get; set; }
        public string CenterName { get; set; }
        //maghadir company type 1 = مدرسه
        //2 = دانشگاه 
        //3 = موسسه آموزشی
        //4 = سازمان یا شرکت
        public byte CenterType { get; set; }
        // be dalile estefade ziad va inke nemikhaim zaid pichide behshe az byte estefaed mikonim 
        //1 = admin
        //2 = Teacher
        //3 = Student
        //4 = SuperAdmin

        //Admin Markaz
        public String CenterAdminID { get; set; }
        //kelid khareji
        [ForeignKey("CenterAdminID")]
        // in gharare list moshtarian ya marakezi ke dar system sabte nam kardand 
        
        public virtual ApplicationUsers Users { get; set; }

    }
}

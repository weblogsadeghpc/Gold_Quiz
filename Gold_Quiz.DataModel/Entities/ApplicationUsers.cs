using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold_Quiz.DataModel.Entities
{
    public class ApplicationUsers : IdentityUser
    {
        //afzoodan filed ha 
        public string FirstName { get; set; }
        public string Family { get; set; }
        public string CustomerName { get; set; }
        //maghadir company type 1 = مدرسه
        //2 = دانشگاه 
        //3 = موسسه آموزشی
        //4 = سازمان یا شرکت
        public byte CustomerType { get; set; }
        // be dalile estefade ziad va inke nemikhaim zaid pichide behshe az byte estefaed mikonim 
        //1 = admin
        //2 = Teacher
        //3 = Student
        //4 = SuperAdmin
        public byte UserType { get; set; }
        // control bar roye account ha
        //true = Active Account
        //false = deactive Account 
        public bool IsActive { get; set; }


    }
}

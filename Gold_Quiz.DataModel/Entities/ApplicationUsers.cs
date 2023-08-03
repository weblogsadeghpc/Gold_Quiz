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
        public byte UserType { get; set; }
        //1 Admin
        //2 Teacher
        //3 Student
        //4 Super admin

        // control bar roye account ha
        //true = Active Account
        //false = deactive Account 
        public byte IsActive { get; set; }


    }
}

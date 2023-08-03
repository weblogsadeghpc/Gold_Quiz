using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold_Quiz.DataModel.Entities
{
    public class CenterUsers
    {
        [Key]
        public int CenterUsersID { get; set; }
        public string CenterAdminID { get; set; } // student or teacher id 
        public string CenterUserID { get; set; }
        public int CenterID { get; set; }
        public byte UserType { get; set; }
        //1 Admin
        //2 Teacher
        //3 Student
        //4 Super admin

        [ForeignKey("CenterAdminID")]
        public virtual ApplicationUsers Users_Admin { get; set; }

        [ForeignKey("CenterUserID")]
        public virtual ApplicationUsers Users_ST { get; set; } // student - teacher

        [ForeignKey("CenterID")]
        public virtual Centers Centers { get; set; }

    }
}

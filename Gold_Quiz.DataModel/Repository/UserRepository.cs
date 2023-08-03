using Gold_Quiz.DataModel.Models;
using Gold_Quiz.DataModel.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold_Quiz.DataModel.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;

        }

        public void UpdateUserInfo(TeacherViewModel model)
        {
            var result = (from u in _context.Users where u.Id == model.UserID select u);
            // user ro bedast miavarim 
            var currentUser = result.FirstOrDefault();
            if (result.Count() > 0)
            {
                // user peyda shod
                currentUser.FirstName = model.FirstName;
                currentUser.Family = model.Family;
               
                _context.Users.Attach(currentUser); // c
                _context.Entry(currentUser).State = Microsoft.EntityFrameworkCore.EntityState.Modified; // virayesh
                _context.SaveChanges();
            }
            
        }
    }
}

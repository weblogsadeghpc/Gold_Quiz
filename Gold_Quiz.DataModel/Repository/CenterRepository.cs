using Gold_Quiz.DataModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold_Quiz.DataModel.Repository
{
    public class CenterRepository : ICenterRepository
    {
        private readonly ApplicationDbContext _context;
        public CenterRepository(ApplicationDbContext context)
        {
            _context = context;
        } // mostaghiman be database dastresi darim chon inja mostaghiman ba database kar nemikonim 

        public int GetCenterID(string UserID) // mikhaim ID har user ke be in function dadae shod moshakhas kone ke dar kodom center ast 
        {
            var model = _context.Centers_Tbl.Where(c => c.CenterAdminID == UserID).Single(); // har admin ke behesh bedim moshakhas mikone dar kodom markaz ast
            return model.CenterID; // khroji center id 

        }
    }
}

using Gold_Quiz.DataModel.Entities;
using Gold_Quiz.DataModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold_Quiz.DataModel.Repository
{
    public class UnitOfWork : IDisposable , IUnitOfWork //IDisposable harvaght kar in class tamam shod az bein bere  
    {
        private readonly ApplicationDbContext _context;
        // harja estefade mostaghim az database konim == Repository 

        public UnitOfWork(ApplicationDbContext context)
        {
            //tazrighe vabastegi 
            _context = context;
        }
        // dare mige har table ke shoma darid yek property hast

        private GenericCRUD<Courses> _courses;

        public GenericCRUD<Courses> coursesUW
        {
            // فقط خواندن دروس 
            get
            {
                if (_courses == null)
                {
                    _courses = new GenericCRUD<Courses>(_context); // ersale _context
                }
                return _courses;
                //va daryaft courses be sorate CRUD
            }

        }
        // gharare in etelaati ro az database bekhone

        public void Save()
        {
            // method save baraye save kardan etelaat 
            _context.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
            // vazifeye biroon kardan az hafeze in class ro darad
        }
    }
}

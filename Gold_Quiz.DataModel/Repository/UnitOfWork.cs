using Gold_Quiz.DataModel.Entities;
using Gold_Quiz.DataModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold_Quiz.DataModel.Repository
{
    public class UnitOfWork : IDisposable, IUnitOfWork //IDisposable harvaght kar in class tamam shod az bein bere  
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
        private GenericCRUD<Centers> _centers;
        private GenericCRUD<CenterUsers> _centerUsers;
        private GenericCRUD<TeacherCourse> _teacherCourse;

        //مراکز 
        public GenericCRUD<Centers> centersUW
        {
            // فقط خواندن دروس 
            get
            {
                if (_centers == null)
                {
                    _centers = new GenericCRUD<Centers>(_context); // ersale _context
                }
                return _centers;
                //va daryaft satr be sorate CRUD
            }

        }

        //کاربران یک مرکز
        public GenericCRUD<CenterUsers> centerUsersUW
        {
            // فقط خواندن دروس 
            get
            {
                if (_centerUsers == null)
                {
                    _centerUsers = new GenericCRUD<CenterUsers>(_context); // ersale _context
                }
                return _centerUsers;
                //va daryaft satr be sorate CRUD
            }

        }

        //دروس یک معلم - استاد ...
        public GenericCRUD<TeacherCourse> teacherCourseUW
        {
            // فقط خواندن دروس 
            get
            {
                if (_teacherCourse == null)
                {
                    _teacherCourse = new GenericCRUD<TeacherCourse>(_context); // ersale _context
                }
                return _teacherCourse;
                //va daryaft satr be sorate CRUD
            }

        }

        // دروس
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

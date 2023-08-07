using AutoMapper;
using Gold_Quiz.DataModel.Entities;
using Gold_Quiz.DataModel.Models;

namespace Gold_Quiz.AutoMapping
{
    public class Automap : Profile
    {
        public Automap()
        {
            CreateMap<ApplicationUsers, RegisterViewModel>().ReverseMap();
            // update course
            CreateMap<Courses, CourseViewModel>().ReverseMap();
            // Create Teacehr 
            CreateMap<ApplicationUsers, TeacherViewModel>().ReverseMap(); // yani in view az ki moshtagh shode
            CreateMap<ApplicationUsers, StudentViewModel>().ReverseMap();
            CreateMap<ApplicationUsers, StudentExcellViewModel>().ReverseMap();
        }
    }
}

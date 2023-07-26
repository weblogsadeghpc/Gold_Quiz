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
        }
    }
}

using Gold_Quiz.DataModel.Entities;
using Gold_Quiz.DataModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold_Quiz.DataModel.Services
{
    public interface IUnitOfWork // interface hast va ma faghat ba esme method kar mikonim // layer ke mitonim dar UI azesh estefade konim
    {
        // takib class va interface == sevice ==> service bayad dar sturtup dar method configure service rahandazi beshe 
        GenericCRUD<Courses> coursesUW { get; } // faghat khandani
        void Save();
        public void Dispose();
    }
}

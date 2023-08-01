using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold_Quiz.DataModel.Services
{
    public interface IEntityDataBaseTransaction : IDisposable
    {
        void commit();
        void RollBack();
        void Dispose();

    }
}

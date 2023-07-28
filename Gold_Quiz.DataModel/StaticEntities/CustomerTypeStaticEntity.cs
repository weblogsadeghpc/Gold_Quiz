using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold_Quiz.DataModel.StaticEntities
{
    public class CustomerTypeStaticEntity
    {

        public int ID { get; set; }
        public string Title { get; set; }
        public List<CustomerTypeStaticEntity> GetCustomerType()
        { // in function yek list az customer type bar migardanad
            var model = new List<CustomerTypeStaticEntity>
            {
                new CustomerTypeStaticEntity { ID = 0,Title= "لطفا نوع موسسه را انتخاب کنید "},
                new CustomerTypeStaticEntity { ID = 1,Title= "مدرسه"},
                new CustomerTypeStaticEntity { ID = 2,Title= "دانشگاه"},
                new CustomerTypeStaticEntity { ID = 3,Title= "موسسه ی آموزشی "},
                new CustomerTypeStaticEntity { ID = 4,Title= "شرکت و سازمان"}
            };
            return model;
        }
    }
}

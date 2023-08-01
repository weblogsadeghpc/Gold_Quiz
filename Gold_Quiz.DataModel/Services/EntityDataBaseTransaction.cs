using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold_Quiz.DataModel.Services
{
    public class EntityDataBaseTransaction : IEntityDataBaseTransaction
    {
        private readonly IDbContextTransaction _transaction;

        public EntityDataBaseTransaction(ApplicationDbContext context)
        {
            //Databse ro be onvane vorodi migire va be mahze daryaft vorodi az dbcontext vared transaction mikone 
            _transaction = context.Database.BeginTransaction(); // be mahze shoro database transaction oon shoro mishe 
        }

        public void commit()
        {
            //وقتی همه ی دستورات با موفقیت انجام شد 
            _transaction.Commit();
        }

        public void RollBack()
        {
            //وفتی دستورات ناقص انجام شد یا حدف میشود یا کلا تراکنش ثبت نمیشود چون ناقص بوده 
            _transaction.Rollback();
        }
        public void Dispose()
        {
            // برای از بین بردن دیتابیس در حافظه استفاده میشود 
            _transaction.Dispose();
        }
    }
}

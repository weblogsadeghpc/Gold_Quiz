using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Gold_Quiz.DataModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Gold_Quiz.DataModel
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUsers, ApplicationRoles, string>
    {
        // record hayi ke be in class vared  mishan az noe string hastand ke migim in kelid haro az noe string dar nazar begir 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            // dar entity freamwork dbcontext namayande database ast

        }

        //AspNetUsers
        //AspNetRoles

        public DbSet<Courses> Course_Tbl { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // be mahze bevojod amad model bia taghirati ra anjam bede 
            //esme aspnetusers ro taghir midim be name yek table 
            //taghire esme jadavel
            builder.Entity<ApplicationUsers>(entity =>
            {
                entity.ToTable(name: "Users_Tbl");
            });//jadval karbaran ma
            builder.Entity<ApplicationRoles>(entity =>
            {
                entity.ToTable(name: "Roles_Tbl");
            });//jadval Roles ma
        }

    }
}

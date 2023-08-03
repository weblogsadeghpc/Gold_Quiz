using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gold_Quiz.DataModel.Repository
{
    public class GenericCRUD<Entity> where Entity : class //alamat <> yani noe ro moshakhas kon 
    {
        private readonly ApplicationDbContext _context; // seda zadan database
        private DbSet<Entity> _table; // noesh ro moshakhas mikonim // noesh table ast
        public GenericCRUD(ApplicationDbContext context)
        {
            _context = context;
            _table = _context.Set<Entity>(); // in _table yek jadval databasi ast az noe Entity --> ke mogheii ke in tabe ro seda mizanim az Entity estefade mikonim 
        }

        public virtual void Create(Entity entity) // esme jadval ke az noe Entity ba esme entity
        {
            // vaghti mikhaim etelaati darbyek jadval vared konim niaaz darim esme oon jadval ro bedonim 
            _table.Add(entity); // dakhele jadval entitiy yel record ra sabt bokon 
        }

        public virtual void Update(Entity entity)
        {
            _table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        // Get daryaft be sorate list hamoon sleect 
        // get by id daryaft ba id ast 

        public virtual Entity GetById(object id) // mikhaim chizi ro bargardonim pas az noe Entity // az har noei mitone bashe id  pas object mizarim noesh ro 
        {
            return _table.Find(id); // dar table begard ba recordi ke id un ba in id barabar ast ro bekesh biroon 
        }

        public virtual void Delete(Entity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)  // agar entity vojod dashte
            {
                // agar entitiy amadeye delete bood 
                _table.Attach(entity); // agar vojod nadasht bia atach kon agar vojod dasht bia removesh kon 
            }
            _table.Remove(entity); // yek record ro remove mikone
        }

        public virtual void DeleteById(object id)
        {
            var entity = GetById(id); // aval peydash kon
            Delete(entity);
        }

        public virtual void DeleteByRange(IEnumerable<Entity> entities) => _table.RemoveRange(entities);

        public virtual IEnumerable<Entity> Get(Expression<Func<Entity, bool>> whereVariable = null, string joinString = "") // null ham mitone bashe , Join ham darim 
        {
            // halate shart ham toosh byad begzarim 
            IQueryable<Entity> query = _table; // yek list az entity mikhaim ke esmesh query ast 
            //jadvali ke be onvane vorodi gerefti ro beriz toye motaghayer query ke motaghayer query yek list ast be sorat IQurable 
            if (whereVariable != null) // shart
            {
                query = query.Where(whereVariable); // shasrt dar neveshte mishe 
            }
            if (joinString != "")
            {
                foreach (string item in joinString.Split(','))
                {
                    query = query.Include(item); // miad ba , joda mikone va bdesh mirize join mikone 
                }
            }
            return query.ToList();

        }
    }
}

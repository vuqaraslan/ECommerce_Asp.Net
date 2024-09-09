using ECommerce.Core.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.DataAccess.EntityFramework
{
    public class EFEntityFrameworkRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
                                    where TEntity : class, IEntity, new() where TContext : DbContext/*, new()*/
    {
        private readonly TContext _context;
        public EFEntityFrameworkRepositoryBase(TContext context)
        {
            this._context = context;
        }

        //Comment-lerin aciqlamasi EFProductDal-dir.
        public async Task Add(TEntity entity)
        {
            //using (var context = new TContext())
            //{
                var addedEntity = _context.Entry(entity);
                addedEntity.State = EntityState.Added;
                await _context.SaveChangesAsync();
            //}
        }

        public async Task Delete(TEntity entity)
        {
            //using (var context = new TContext())
            //{
                var deletedEntity = _context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            //}
        }

        public Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            //using (var context = new TContext())
            //{
                return _context.Set<TEntity>().SingleOrDefaultAsync(filter);
            //}
        }

        public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>>? filter = null)
        {
            //using (var context = new TContext())
            //{
                return filter == null ? await _context.Set<TEntity>().ToListAsync() :
                                        await _context.Set<TEntity>().Where(filter).ToListAsync();
            //}
        }

        public async Task Update(TEntity entity)
        {
            //using (var context = new TContext())
            //{
                var updatedEntity = _context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                await _context.SaveChangesAsync();
            //}
        }


    }
}

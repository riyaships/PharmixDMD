using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pharmix.Data.Entities.Context;
using Pharmix.Web.Entities;

namespace Pharmix.Web.Services.Repositories
{
    public class Repository : IRepository
    {
        private readonly PharmixEntityContext _context;

        public Repository(PharmixEntityContext context)
        {
            this._context = context;
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            return _context.Set<T>();
        }

        //TODO: I add this method to use against context model that didnt extend from BaseEntity
        //Name : Riyo Maulana
        public IEnumerable<T> Get<T>() where T : class
        {
            return _context.Set<T>();
        }

        public T GetById<T>(int id) where T : class
        {
            return _context.Set<T>().Find(id);
        }

        public T SaveNew<T>(T entity, string userId = null) where T : class
        {
            _context.Set<T>().Add(entity);
            if (!string.IsNullOrEmpty(userId))
                _context.SaveChanges(userId);
            else
                _context.SaveChanges();
            return entity;
        }
        public BusinessDetails AddNew(BusinessDetails entity)
        {
            var result = _context.BusinessDetails.Add(entity);
            _context.SaveChanges();
            return entity;
        }
        public EmailPasswordTokeneDetails AddNew(EmailPasswordTokeneDetails entity)
        {
            var result = _context.EmailPasswordTokeneDetails.Add(entity);
            _context.SaveChanges();
            return entity;
        }
        public void SaveExisting<T>(T entity, string userId=null) where T : class
        {
            _context.Entry(entity).State = EntityState.Modified;
            if (!string.IsNullOrEmpty(userId))
                _context.SaveChanges(userId);
            else
                _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public PharmixEntityContext GetContext()
        {
            return _context;
        }

        public T Detach<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public void Reload<T>(T entity) where T : class
        {
            _context.Entry(entity).Reload();
        }
       
    }
}

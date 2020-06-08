using System.Collections.Generic;
using Pharmix.Data.Entities.Context;
using Pharmix.Web.Entities;

namespace Pharmix.Web.Services.Repositories
{
    public interface IRepository
    {
        IEnumerable<T> GetAll<T>() where T : class;

        IEnumerable<T> Get<T>() where T : class;

        T GetById<T>(int id) where T : class ;

        T SaveNew<T>(T entity, string userId=null) where T : class;

        void SaveExisting<T>(T entity, string userId=null) where T : class;

        void SaveChanges();

        void Delete<T>(T entity) where T : class;

        PharmixEntityContext GetContext();

        T Detach<T>(T entity) where T : class;

        void Reload<T>(T entity) where T : class;

        //IEnumerable<T> GetAllWithRelated<T>(params string[] paths) where T : BaseEntity;

        //T GetByIdWithRelated<T>(int id, params string[] paths) where T : BaseEntity;

        //void SaveExisting<T>(ApplicationUser entity) where T : BaseEntity;

        //void ModifyExisting<T>(T entity) where T : BaseEntity;
        BusinessDetails AddNew(BusinessDetails entity);
        EmailPasswordTokeneDetails AddNew(EmailPasswordTokeneDetails entity);
        
    }
}

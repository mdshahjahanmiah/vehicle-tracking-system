using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VehicleTrackingSystem.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<T> Get()
        {
            return _unitOfWork.QueriesContext.Set<T>().AsEnumerable<T>();
        }
        public IEnumerable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _unitOfWork.QueriesContext.Set<T>().Where(predicate).AsEnumerable<T>();
        }
        public void Add(T entity)
        {
            _unitOfWork.CommandsContext.Set<T>().Add(entity);
        }
        public void Delete(T entity)
        {
            T existing = _unitOfWork.QueriesContext.Set<T>().Find(entity);
            if (existing != null) _unitOfWork.CommandsContext.Set<T>().Remove(existing);
        }
        public void Update(T entity)
        {
            _unitOfWork.CommandsContext.Entry(entity).State = EntityState.Modified;
            _unitOfWork.CommandsContext.Set<T>().Update(entity);
        }
    }
}

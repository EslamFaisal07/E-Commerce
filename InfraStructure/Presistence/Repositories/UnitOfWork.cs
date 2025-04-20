using DomainLayer.Contracts;
using DomainLayer.Models;
using Presistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {

        private readonly Dictionary<string, object> _repositories = new Dictionary<string, object>();


        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {

            var typeName = typeof(TEntity).Name;
            if (_repositories.ContainsKey(typeName))
            {
                // casting from object to IGenericRepository
                return (IGenericRepository<TEntity, TKey>) _repositories[typeName];
            }
            else
            {
                /// create a new instance of the repository
                var repo = new GenericRepository<TEntity, TKey>(_dbContext);
                //store in dictionary
                _repositories.Add(typeName, repo);
                //return repo
                return repo;

            }









        }









        public async Task<int> SaveChangesAsync()
        {
             return await  _dbContext.SaveChangesAsync();
        }
    }
}

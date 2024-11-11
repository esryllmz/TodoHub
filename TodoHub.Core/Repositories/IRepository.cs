using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TodoHub.Core.Entities;

namespace TodoHub.Core.Repositories;

public interface IRepository<TEntity, TId> where TEntity : Entity<TId>, new()
{
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(TId id);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity?> RemoveAsync(TEntity entity);
    Task<TEntity?> UpdateAsync(TEntity entity);

}

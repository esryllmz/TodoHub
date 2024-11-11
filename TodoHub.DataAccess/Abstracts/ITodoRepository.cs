using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoHub.Core.Repositories;
using TodoHub.Models.Entities;

namespace TodoHub.DataAccess.Abstracts;

public interface ITodoRepository:IRepository<Todo, Guid>
{
    Task<int> GetToDosCountByUserAsync(string userId);
    IQueryable<Todo> GetByUserId(string userId);
}

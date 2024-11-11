using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoHub.Core.Repositories;
using TodoHub.DataAccess.Abstracts;
using TodoHub.DataAccess.Contexts;
using TodoHub.Models.Entities;

namespace TodoHub.DataAccess.Concretes;

public class EfTodoRepository : EfRepositoryBase<BaseDbContext, Todo, Guid>, ITodoRepository
{
    public EfTodoRepository(BaseDbContext context) : base(context)
    {

    }
    public async Task<int> GetToDosCountByUserAsync(string userId)
    {
        return await _context.Todos.CountAsync(todo => todo.UserId == userId);
    }

   

    IQueryable<Todo> ITodoRepository.GetByUserId(string userId)
    {
        return _context.Todos.Where(todo => todo.UserId == userId);
    }
}


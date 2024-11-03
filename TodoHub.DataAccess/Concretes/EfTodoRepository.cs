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
}


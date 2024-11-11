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

public class EfCategoryRepository : EfRepositoryBase<BaseDbContext, Category, int>, ICategoryRepository
{
   
    public EfCategoryRepository(BaseDbContext context) : base(context)
    {
        
    }
    public async Task<Category?> GetByNameAsync(string name)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
    }

}

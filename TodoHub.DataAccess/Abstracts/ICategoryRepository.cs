using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoHub.Core.Repositories;
using TodoHub.Models.Entities;

namespace TodoHub.DataAccess.Abstracts;

public interface ICategoryRepository : IRepository<Category, int>
{

    Task<Category?> GetByNameAsync(string name);
}

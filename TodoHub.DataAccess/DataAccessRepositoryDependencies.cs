using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoHub.DataAccess.Abstracts;
using TodoHub.DataAccess.Concretes;
using TodoHub.DataAccess.Contexts;

namespace TodoHub.DataAccess
{
    public static class RepositoryDependencies
    {

        public static IServiceCollection AddRepositoryDepencdencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITodoRepository, EfTodoRepository>();
            services.AddDbContext<BaseDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlConnection")));
            return services;
        }
    }
}

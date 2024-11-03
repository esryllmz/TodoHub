using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TodoHub.Services.Abstracts;
using TodoHub.Services.CacheService;
using TodoHub.Services.Concretes;
using TodoHub.Services.Rules;


namespace TodoHub.Services
{
    public static class ServiceDependencies
    {
        public static IServiceCollection AddServiceDependenies(this IServiceCollection services)
        {
            services.AddScoped<ITodoService, TodoService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<TodoCacheService>();
            services.AddScoped<TodoBusinessRules>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.TodoFluentValidationAutoValidation();
            services.TodoValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = "localhost:6379";
                opt.InstanceName = "TodoHub";
            });


            return services;
        }
    }
}

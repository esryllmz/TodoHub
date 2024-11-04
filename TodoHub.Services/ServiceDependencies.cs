using FluentValidation;
using FluentValidation.AspNetCore;
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
using TodoHub.Services.Constants;
using TodoHub.Services.Profiles;
using TodoHub.Services.Rules;


namespace TodoHub.Services
{
    public static class ServiceDependencies
    {
        public static IServiceCollection AddServiceDependenies(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddScoped<ITodoService, TodoService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ICategoryService, CategoryService>();    
            services.AddScoped<TodoCacheService>();

            services.AddScoped<TodoBusinessRules>();
            services.AddScoped<CategoryBusinessRules>();
            services.AddScoped<UserBusinessRules>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            


            return services;
        }
    }
}

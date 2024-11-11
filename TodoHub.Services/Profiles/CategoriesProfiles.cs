using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoHub.Models.Dtos.Category.Requests;
using TodoHub.Models.Dtos.Category.Responses;
using TodoHub.Models.Dtos.Todo.Requests;
using TodoHub.Models.Dtos.Todo.Responses;
using TodoHub.Models.Entities;

namespace TodoHub.Services.Profiles
{
    public class CategoriesProfiles : Profile
    {
        public CategoriesProfiles()
        {
            CreateMap<CreateCategoryRequest, Category>();
            CreateMap<UpdateCategoryRequest, Category>();
            CreateMap<Category, CategoryResponseDto>();

        }
    }
}

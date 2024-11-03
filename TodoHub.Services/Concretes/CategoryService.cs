using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoHub.Core.Exceptions;
using TodoHub.Core.Responses;
using TodoHub.DataAccess.Abstracts;
using TodoHub.DataAccess.Concretes;
using TodoHub.Models.Dtos.Category.Requests;
using TodoHub.Models.Dtos.Category.Responses;
using TodoHub.Models.Entities;
using TodoHub.Services.Abstracts;
using TodoHub.Services.Rules;

namespace TodoHub.Services.Concretes
{
    public sealed class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly CategoryBusinessRules _businessRules;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, CategoryBusinessRules businessRules)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _businessRules = businessRules;
        }

       
        public Task<ReturnModel<CategoryResponseDto>> AddAsync(CreateCategoryRequest dto)
        {
          throw new NotImplementedException();
        }


        public ReturnModel<string> Delete(int id)
        {
            _businessRules.CategoryIsPresent(id);

            Category category = _categoryRepository.GetById(id);
            _categoryRepository.Delete(category);

            return new ReturnModel<string>
            {
                Data = $"Kategori Başlığı : {category.Name}",
                Message = "Kategori başarıyla silindi.",
                StatusCode = 204,
                Success = true
            };
        }

        public ReturnModel<List<CategoryResponseDto>> GetAll()
        {
            var categories = _categoryRepository.GetAll();
            List<CategoryResponseDto> responses = _mapper.Map<List<CategoryResponseDto>>(categories);

            return new ReturnModel<List<CategoryResponseDto>>
            {
                Data = responses,
                Message = "Tüm kategoriler listelendi.",
                StatusCode = 200,
                Success = true
            };
        }

        public ReturnModel<CategoryResponseDto> GetById(int id)
        {
            _businessRules.CategoryIsPresent(id);

            Category category = _categoryRepository.GetById(id);
            CategoryResponseDto response = _mapper.Map<CategoryResponseDto>(category);

            return new ReturnModel<CategoryResponseDto>
            {
                Data = response,
                Message = "Kategori getirildi.",
                StatusCode = 200,
                Success = true
            };
        }

        public ReturnModel<CategoryResponseDto> Update(UpdateCategoryRequest dto)
        {
            _businessRules.CategoryIsPresent(dto.Id);

            Category category = _categoryRepository.GetById(dto.Id);
            category.Name = dto.Name;
           

            _categoryRepository.Update(category);

            CategoryResponseDto response = _mapper.Map<CategoryResponseDto>(category);

            return new ReturnModel<CategoryResponseDto>
            {
                Data = response,
                Message = "Kategori başarıyla güncellendi.",
                StatusCode = 200,
                Success = true
            };
        }
    }

    }

using AutoMapper;
using Azure.Core;
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

       
        public async Task<ReturnModel<CategoryResponseDto>> AddAsync(CreateCategoryRequest dto)
        {
            await _businessRules.IsNameUnique(dto.Name);

            Category createdCategory = _mapper.Map<Category>(dto);
            await _categoryRepository.AddAsync(createdCategory);
            CategoryResponseDto response = _mapper.Map<CategoryResponseDto>(createdCategory);

            return new ReturnModel<CategoryResponseDto>()
            {
                Success = true,
                Message = "Kategori eklendi",
                Data = response,
                StatusCode = 200
            };
        }


        public async Task<ReturnModel<CategoryResponseDto>> DeleteAsync(int id)
        {
            await _businessRules.IsCategoryExistAsync(id);

            Category category = await _categoryRepository.GetByIdAsync(id);
            Category deletedCategory = await _categoryRepository.RemoveAsync(category);
            CategoryResponseDto response = _mapper.Map<CategoryResponseDto>(deletedCategory);


            return new ReturnModel<CategoryResponseDto>()
            {
                Data = response,
                Message = "Kategori başarıyla silindi.",
                StatusCode = 200,
                Success = true
            };
        }

        public async Task<ReturnModel<List<CategoryResponseDto>>> GetAllAsync()
        {
            List<Category> categories = await _categoryRepository.GetAllAsync();
            List<CategoryResponseDto> responseList = _mapper.Map<List<CategoryResponseDto>>(categories);

            return new ReturnModel<List<CategoryResponseDto>>()
            {
                Success = true,
                Message = "Kategori listesi başarılı bir şekilde getirildi.",
                Data = responseList,
                StatusCode = 200
            };
        }

        public async Task<ReturnModel<CategoryResponseDto?>> GetByIdAsync(int id)
        {
            _businessRules.CategoryIsPresent(id);

            Category? category = await _categoryRepository.GetByIdAsync(id);
            CategoryResponseDto? response = _mapper.Map<CategoryResponseDto>(category);

            return new ReturnModel<CategoryResponseDto?>()
            {
                Success = true,
                Message = $"{id} numaralı kategori başarılı bir şekilde getirildi.",
                Data = response,
                StatusCode = 200
            };
        }

        public async Task<ReturnModel<CategoryResponseDto>> UpdateAsync(UpdateCategoryRequest request)
        {

            await _businessRules.IsCategoryExistAsync(request.Id);

            Category existingCategory = await _categoryRepository.GetByIdAsync(request.Id);

            existingCategory.Id = existingCategory.Id;
            existingCategory.Name = request.Name;

            Category updatedCategory = await _categoryRepository.UpdateAsync(existingCategory);
            CategoryResponseDto dto = _mapper.Map<CategoryResponseDto>(updatedCategory);

            return new ReturnModel<CategoryResponseDto>()
            {
                Success = true,
                Message = "Kategori güncellendi.",
                Data = dto,
                StatusCode = 200
            };
        }
    }

    }

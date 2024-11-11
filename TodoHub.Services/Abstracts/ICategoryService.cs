using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoHub.Core.Responses;
using TodoHub.Models.Dtos.Category.Requests;
using TodoHub.Models.Dtos.Category.Responses;

namespace TodoHub.Services.Abstracts;


    public interface ICategoryService
    {
        Task<ReturnModel<CategoryResponseDto>> AddAsync(CreateCategoryRequest dto);
        Task<ReturnModel<List<CategoryResponseDto>>> GetAllAsync();
        Task<ReturnModel<CategoryResponseDto?>> GetByIdAsync(int id);
        Task<ReturnModel<CategoryResponseDto>> UpdateAsync(UpdateCategoryRequest request);
        Task<ReturnModel<CategoryResponseDto>> DeleteAsync(int id);
   
    }




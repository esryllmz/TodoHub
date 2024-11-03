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
        ReturnModel<List<CategoryResponseDto>> GetAll();
        ReturnModel<CategoryResponseDto> GetById(int id);
        ReturnModel<CategoryResponseDto> Update(UpdateCategoryRequest dto);
        ReturnModel<string> Delete(int id);
   
    }




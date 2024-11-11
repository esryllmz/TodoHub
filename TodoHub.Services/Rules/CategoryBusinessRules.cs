using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoHub.Core.Exceptions;
using TodoHub.DataAccess.Abstracts;
using TodoHub.Services.Constants;

namespace TodoHub.Services.Rules
{
    public class CategoryBusinessRules(ICategoryRepository _categoryRepository)
    {
        public async Task IsCategoryExistAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                throw new NotFoundException($"{id} numaralı kategori bulunamadı.");
            }
        }

        public async Task IsNameUnique(string name)
        {
            var category = await _categoryRepository.GetByNameAsync(name);

            if (category != null)
            {
                throw new BusinessException("Bu isim ile sistemimizde zaten bir kategori mevcut.");
            }
        }

        internal void CategoryIsPresent(int id)
        {
            throw new NotImplementedException();
        }
    }

}

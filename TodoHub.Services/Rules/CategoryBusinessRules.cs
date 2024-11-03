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
    public class CategoryBusinessRules
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryBusinessRules(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public virtual bool CategoryIsPresent(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category is null)
            {
                throw new NotFoundException(Messages.CategoryIsNotPresentMessage(id));
            }

            return true;
        }
    }

}

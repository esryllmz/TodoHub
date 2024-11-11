using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoHub.Models.Dtos.Category.Requests;

namespace TodoHub.Services.Validations.Categories
{
    public class CreateCategoryRequestDtoValidator : AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryRequestDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kategori Adı Boş olamaz.");

        }
    }
}

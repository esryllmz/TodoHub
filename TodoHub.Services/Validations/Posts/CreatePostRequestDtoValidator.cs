using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoHub.Models.Dtos.Todo.Requests;

namespace TodoHub.Services.Validations
{
    public class CreateTodoRequestDtoValidator : AbstractValidator<CreateTodoRequestDto>
    {
        public CreateTodoRequestDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Todo Başlığı boş olamaz.")
                .Length(2, 50).WithMessage("Todo Başlığı minimum 2, maksimum 50 karakter olmalıdır.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Todo Açıklaması boş olamaz.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Başlangıç tarihi boş olamaz.")
                .LessThan(x => x.EndDate).WithMessage("Başlangıç tarihi, bitiş tarihinden önce olmalıdır.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("Bitiş tarihi boş olamaz.");

            RuleFor(x => x.Priority)
                .IsInEnum().WithMessage("Geçerli bir öncelik değeri seçilmelidir.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Geçerli bir kategori ID'si belirtilmelidir.");
        }
    }
}

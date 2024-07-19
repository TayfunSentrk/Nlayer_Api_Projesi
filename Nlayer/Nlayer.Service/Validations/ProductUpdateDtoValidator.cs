using FluentValidation;
using Nlayer.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Service.Validations
{
    public class ProductUpdateDtoValidator:AbstractValidator<ProductUpdateDto>
    {
        /// <summary>
        /// ProductDtoValidator yapıcı metodu, doğrulama kurallarını tanımlar.
        /// </summary>
        public ProductUpdateDtoValidator()
        {
            // Name property'si null veya boş olamaz.
            RuleFor(p => p.Name)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required");

            // Price property'si 1 ile int.MaxValue arasında olmalıdır.
            RuleFor(p => p.Price)
                .InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater than 0");

            // Stock property'si 1 ile int.MaxValue arasında olmalıdır.
            RuleFor(p => p.Stock)
                .InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater than 0");

            // CategoryId property'si 1 ile int.MaxValue arasında olmalıdır.
            RuleFor(p => p.CategoryId)
                .InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater than 0");
        }
    }
}

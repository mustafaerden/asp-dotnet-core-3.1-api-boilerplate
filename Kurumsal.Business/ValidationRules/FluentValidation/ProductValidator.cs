using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            // Seklinde de yazilabilir;
            //RuleFor(p => p.Name).NotEmpty().Length(3, 50);

            
            RuleFor(p => p.Name).NotEmpty().WithMessage("Product name is required ellam.");
            RuleFor(p => p.Name).Length(3, 50);

            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(1);
            // CategoryId si su olan product in price i 10 ve 10 dan buyuk olmak zorunda;
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);

            // Ornegin product name in Z harfi ile baslamasini zorunlu yapmak istiyoruz(Assagida metot olusturup onu Must in icinde cagiriyoruz);
            //RuleFor(p => p.Name).Must(StartWithZ);

        }

        //private bool StartWithZ(string arg)
        //{
        //    return arg.StartsWith("Z");
        //}
    }
}

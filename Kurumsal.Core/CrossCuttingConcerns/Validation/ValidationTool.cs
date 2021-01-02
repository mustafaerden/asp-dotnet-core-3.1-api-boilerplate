using FluentValidation;

namespace Core.CrossCuttingConcerns.Validation
{
    // Bu Class ta merkezi bir validator yazmis oluyoruz. Bunlari Business taki Manager larda kullanicaz.
    // ValidationRules larimizi Business katmaninda FluentValidation Dosyasi altinda yazdik!
    public static class ValidationTool
    {
        public static void Validate(IValidator validator, object entity)
        {
            var result = validator.Validate(entity);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}

using FluentValidation;
using WorkWearGroup.API.Models;

namespace WorkWearGroup.API.Validation
{
    public class ItemValidator : AbstractValidator<Item>
    {
        public const string RegexForUrlSafeKey = @"^[a-zA-Z0-9\-._~]*$";

        public ItemValidator()
        {
            RuleFor(x => x.Key).Matches(RegexForUrlSafeKey);
            RuleFor(x => x.Key).MinimumLength(1);
            RuleFor(x => x.Key).MaximumLength(32);
            RuleFor(x => x.Value).NotEmpty();
            RuleFor(x => x.Value).MaximumLength(1024);
        }
    }
}

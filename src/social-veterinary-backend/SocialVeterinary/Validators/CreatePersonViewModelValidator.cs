using FluentValidation;

using SocialVeterinary.Api.ViewModels;

namespace SocialVeterinary.Api.Validators
{
    public class CreatePersonViewModelValidator : AbstractValidator<CreatePersonViewModel>
    {
        public CreatePersonViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();
        }
    }
}

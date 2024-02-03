using FluentValidation;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Validators
{
    public class AddRegionRequestValidator : AbstractValidator<AddRegionRequest>
    {
        public AddRegionRequestValidator()
        {
            RuleFor(a => a.Code).NotEmpty();
            RuleFor(a => a.Name).NotEmpty();
            RuleFor(a => a.Area).GreaterThan(0);
            RuleFor(a => a.Long).GreaterThan(0);
            RuleFor(a => a.Lat).GreaterThan(0);
            RuleFor(a => a.Population).GreaterThanOrEqualTo(0);
        }
    }

    public class UpdateRegionRequestValidator : AbstractValidator<UpdateRegionRequest>
    {
        public UpdateRegionRequestValidator()
        {
            RuleFor(a => a.Code).NotEmpty();
            RuleFor(a => a.Name).NotEmpty();
            RuleFor(a => a.Area).GreaterThan(0);
            RuleFor(a => a.Long).GreaterThan(0);
            RuleFor(a => a.Lat).GreaterThan(0);
            RuleFor(a => a.Population).GreaterThanOrEqualTo(0);
        }
    }
}

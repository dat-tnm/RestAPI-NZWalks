using FluentValidation;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Validators
{
    public class AddDifficultyRequestValidator : AbstractValidator<AddWDifficultyRequest>
    {
        public AddDifficultyRequestValidator() 
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }

    public class UpdateDifficultyRequestValidator : AbstractValidator<UpdateWDifficultyRequest>
    {
        public UpdateDifficultyRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}

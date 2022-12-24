using FluentValidation;
using ProjectSetup.Commands;

namespace ProjectSetup.Validation
{
	public class CreateFastPostValidator : AbstractValidator<CreateFastPostCommand>
	{
		public CreateFastPostValidator()
		{
			RuleFor(customer => customer.Text).NotEmpty().Length(1, 200).WithMessage("Text must be between 1 and 200 characters.");
			RuleFor(customer => customer.CategoryId).NotEmpty().WithMessage("Please add a category.");
		}
	}
}

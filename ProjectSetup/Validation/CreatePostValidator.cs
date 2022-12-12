using FluentValidation;
using ProjectSetup.Contracts.V1.Requests;

namespace ProjectSetup.Validation
{
	public class CreatePostValidator : AbstractValidator<CreatePostRequest>
	{
		public CreatePostValidator()
		{
			RuleFor(customer => customer.Text).NotNull().NotEmpty().Length(1, 400).WithMessage("Text must be between 1 and 400 characters.");
			RuleFor(customer => customer.CategoryId).NotNull().NotEmpty().WithMessage("Please add a category.");
			RuleFor(customer => customer.CreatedById).NotNull().NotEmpty();
		}
	}
}

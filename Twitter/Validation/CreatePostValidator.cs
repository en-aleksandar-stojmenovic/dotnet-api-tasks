using FluentValidation;
using Twitter.Contracts.V1.Requests;

namespace Twitter.Validation
{
	public class CreatePostValidator : AbstractValidator<CreatePostRequest>
	{
		public CreatePostValidator()
		{
			RuleFor(customer => customer.Text).NotEmpty().Length(1, 400).WithMessage("Text must be between 1 and 400 characters.");
			RuleFor(customer => customer.CategoryId).NotEmpty().WithMessage("Please add a category.");
		}
	}
}

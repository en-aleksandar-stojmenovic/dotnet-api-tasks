using FluentValidation;
using ProjectSetup.Contracts.V1.Requests;

namespace ProjectSetup.Validation
{
	public class UpdatePostValidator : AbstractValidator<UpdatePostRequest>
	{
		public UpdatePostValidator()
		{
			RuleFor(post => post.Id).NotEmpty().WithMessage("Please add post Id.");
			RuleFor(post => post.Text).NotEmpty().Length(1, 400).WithMessage("Text must be between 1 and 400 characters.");
			RuleFor(post => post.CategoryId).NotEmpty().WithMessage("Please add a category.");
		}
	}
}

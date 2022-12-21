using FluentValidation.TestHelper;
using ProjectSetup.Contracts.V1.Requests;
using ProjectSetup.Validation;
using Tests.Helpers;

namespace Tests.Validation
{
	public class CreatePostValidatorTests
	{
		private CreatePostValidator _createPostValidator;

		public CreatePostValidatorTests()
		{
			_createPostValidator = new CreatePostValidator();
		}

		[Fact]
		public void ValidateTextIsEmpty()
		{
			var createPostRequest = new CreatePostRequest { Text = "", CategoryId = Guid.NewGuid() };

			var result = _createPostValidator.TestValidate(createPostRequest);

			result.ShouldHaveValidationErrorFor(x => x.Text);
			result.ShouldNotHaveValidationErrorFor(x => x.CategoryId);
		}

		[Fact]
		public void ValidateCategoryIdIsEmpty()
		{
			var createPostRequest = new CreatePostRequest { Text = "Some text", CategoryId = Guid.Empty };

			var result = _createPostValidator.TestValidate(createPostRequest);

			result.ShouldNotHaveValidationErrorFor(x => x.Text);
			result.ShouldHaveValidationErrorFor(x => x.CategoryId);
		}

		[Fact]
		public void ValidateTextIsMoreThan400Characters()
		{
			var createPostRequest = new CreatePostRequest { Text = StringHelpers.GetRandomString(401), CategoryId = Guid.NewGuid() };

			var result = _createPostValidator.TestValidate(createPostRequest);

			result.ShouldHaveValidationErrorFor(x => x.Text);
			result.ShouldNotHaveValidationErrorFor(x => x.CategoryId);
		}

		[Fact]
		public void ValidateCreatePostRequestIsValid()
		{
			var createPostRequest = new CreatePostRequest { Text = "Some text", CategoryId = Guid.NewGuid() };

			var result = _createPostValidator.TestValidate(createPostRequest);

			result.ShouldNotHaveAnyValidationErrors();
		}
	}
}

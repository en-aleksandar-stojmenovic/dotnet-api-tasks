using FluentValidation.TestHelper;
using ProjectSetup.Contracts.V1.Requests;
using ProjectSetup.Validation;
using Tests.Helpers;

namespace Tests.Validation
{
	public class UpdatePostValidatorTests
	{
		private UpdatePostValidator _updatePostValidator;

		public UpdatePostValidatorTests()
		{
			_updatePostValidator = new UpdatePostValidator();
		}

		[Fact]
		public void ValidateTextIsEmpty()
		{
			var createPostRequest = new UpdatePostRequest { Text = "", CategoryId = Guid.NewGuid() };

			var result = _updatePostValidator.TestValidate(createPostRequest);

			result.ShouldHaveValidationErrorFor(x => x.Text);
			result.ShouldNotHaveValidationErrorFor(x => x.CategoryId);
		}

		[Fact]
		public void ValidateTextHasWhiteSpaces()
		{
			var createPostRequest = new UpdatePostRequest { Text = "     ", CategoryId = Guid.NewGuid() };

			var result = _updatePostValidator.TestValidate(createPostRequest);

			result.ShouldHaveValidationErrorFor(x => x.Text);
			result.ShouldNotHaveValidationErrorFor(x => x.CategoryId);
		}

		[Fact]
		public void ValidateTextIsNull()
		{
			var createPostRequest = new UpdatePostRequest { Text = null, CategoryId = Guid.NewGuid() };

			var result = _updatePostValidator.TestValidate(createPostRequest);

			result.ShouldHaveValidationErrorFor(x => x.Text);
			result.ShouldNotHaveValidationErrorFor(x => x.CategoryId);
		}

		[Fact]
		public void ValidateCategoryIdIsEmpty()
		{
			var createPostRequest = new UpdatePostRequest { Text = "Some text", CategoryId = Guid.Empty };

			var result = _updatePostValidator.TestValidate(createPostRequest);

			result.ShouldNotHaveValidationErrorFor(x => x.Text);
			result.ShouldHaveValidationErrorFor(x => x.CategoryId);
		}

		[Fact]
		public void ValidateTextIsMoreThan400Characters()
		{
			var createPostRequest = new UpdatePostRequest { Text = StringHelpers.GetRandomString(401), CategoryId = Guid.NewGuid() };

			var result = _updatePostValidator.TestValidate(createPostRequest);

			result.ShouldHaveValidationErrorFor(x => x.Text);
			result.ShouldNotHaveValidationErrorFor(x => x.CategoryId);
		}

		[Fact]
		public void ValidateCreatePostRequestIsValid()
		{
			var createPostRequest = new UpdatePostRequest { Text = "Some text", CategoryId = Guid.NewGuid() };

			var result = _updatePostValidator.TestValidate(createPostRequest);

			result.ShouldNotHaveAnyValidationErrors();
		}
	}
}

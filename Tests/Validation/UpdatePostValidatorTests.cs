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
			var updatePostRequest = new UpdatePostRequest { Id = Guid.NewGuid(), Text = "", CategoryId = Guid.NewGuid() };

			var result = _updatePostValidator.TestValidate(updatePostRequest);

			result.ShouldNotHaveValidationErrorFor(x => x.Id);
			result.ShouldHaveValidationErrorFor(x => x.Text);
			result.ShouldNotHaveValidationErrorFor(x => x.CategoryId);
		}

		[Fact]
		public void ValidateTextHasWhiteSpaces()
		{
			var updatePostRequest = new UpdatePostRequest { Id = Guid.NewGuid(), Text = "     ", CategoryId = Guid.NewGuid() };

			var result = _updatePostValidator.TestValidate(updatePostRequest);

			result.ShouldNotHaveValidationErrorFor(x => x.Id);
			result.ShouldHaveValidationErrorFor(x => x.Text);
			result.ShouldNotHaveValidationErrorFor(x => x.CategoryId);
		}

		[Fact]
		public void ValidateTextIsNull()
		{
			var updatePostRequest = new UpdatePostRequest { Id = Guid.NewGuid(), Text = null, CategoryId = Guid.NewGuid() };

			var result = _updatePostValidator.TestValidate(updatePostRequest);

			result.ShouldNotHaveValidationErrorFor(x => x.Id);
			result.ShouldHaveValidationErrorFor(x => x.Text);
			result.ShouldNotHaveValidationErrorFor(x => x.CategoryId);
		}

		[Fact]
		public void ValidateCategoryIdIsEmpty()
		{
			var updatePostRequest = new UpdatePostRequest { Id = Guid.NewGuid(), Text = "Some text", CategoryId = Guid.Empty };

			var result = _updatePostValidator.TestValidate(updatePostRequest);

			result.ShouldNotHaveValidationErrorFor(x => x.Id);
			result.ShouldNotHaveValidationErrorFor(x => x.Text);
			result.ShouldHaveValidationErrorFor(x => x.CategoryId);
		}

		[Fact]
		public void ValidatePostIdIsEmpty()
		{
			var updatePostRequest = new UpdatePostRequest { Id = Guid.Empty, Text = "Some text", CategoryId = Guid.NewGuid() };

			var result = _updatePostValidator.TestValidate(updatePostRequest);

			result.ShouldHaveValidationErrorFor(x => x.Id);
			result.ShouldNotHaveValidationErrorFor(x => x.Text);
			result.ShouldNotHaveValidationErrorFor(x => x.CategoryId);
		}

		[Fact]
		public void ValidateTextIsMoreThan400Characters()
		{
			var updatePostRequest = new UpdatePostRequest { Id = Guid.NewGuid(), Text = StringHelpers.GetRandomString(401), CategoryId = Guid.NewGuid() };

			var result = _updatePostValidator.TestValidate(updatePostRequest);

			result.ShouldNotHaveValidationErrorFor(x => x.Id);
			result.ShouldHaveValidationErrorFor(x => x.Text);
			result.ShouldNotHaveValidationErrorFor(x => x.CategoryId);
		}

		[Fact]
		public void ValidateCreatePostRequestIsValid()
		{
			var updatePostRequest = new UpdatePostRequest { Id = Guid.NewGuid(), Text = "Some text", CategoryId = Guid.NewGuid() };

			var result = _updatePostValidator.TestValidate(updatePostRequest);

			result.ShouldNotHaveAnyValidationErrors();
		}
	}
}

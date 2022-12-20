using FluentAssertions;
using FluentValidation.TestHelper;
using ProjectSetup.Contracts.V1.Requests;
using ProjectSetup.Validation;

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

			result.Errors.First().ErrorCode.Should().Be("NotEmptyValidator");
		}

		[Fact]
		public void ValidateCategoryIdIsEmpty()
		{
			var createPostRequest = new CreatePostRequest { Text = "Some text", CategoryId = Guid.Empty };

			var result = _createPostValidator.TestValidate(createPostRequest);

			result.Errors.First().ErrorCode.Should().Be("NotEmptyValidator");
		}

		[Fact]
		public void ValidateTextIsMoreThan400Characters()
		{
			var createPostRequest = new CreatePostRequest { Text = GetRandomString(401), CategoryId = Guid.NewGuid() };

			var result = _createPostValidator.TestValidate(createPostRequest);

			result.Errors.First().ErrorMessage.Should().Be("Text must be between 1 and 400 characters.");
		}

		private string GetRandomString(int length)
		{
			Random rand = new Random();
			int randValue;
			string str = "";
			char letter;

			for (int i = 0; i < length; i++)
			{
				randValue = rand.Next(0, 26);

				letter = Convert.ToChar(randValue + 65);

				str = str + letter;
			}

			return str;
		}
	}
}

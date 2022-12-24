namespace ProjectSetup.Contracts.V1
{
	public static class ApiRoutes
	{
		public const string Root = "api";
		public const string Version = "v1";
		public const string Base = Root + "/" + Version;

		public static class Categories
		{
			public const string GetAll = Base + "/categories";
			public const string GetCategory = Base + "/categories/{categoryId}";
			public const string GetCategoryByName = Base + "/categories/name/{categoryName}";
			public const string Create = Base + "/categories";
			public const string Delete = Base + "/categories/{categoryId}";
			public const string DeleteByName = Base + "/categories/name/{categoryName}";
		}

		public static class Post
		{
			public const string GetAll = Base + "/posts";
			public const string GetPost = Base + "/posts/{postId}";
			public const string Create = Base + "/posts";
			public const string Update = Base + "/posts";
			public const string Delete = Base + "/posts/{postId}";
		}

		public static class FastPost
		{
			public const string GetAll = Base + "/fastposts";
			public const string GetPost = Base + "/fastposts/{postId}";
			public const string Create = Base + "/fastposts";
			public const string Delete = Base + "/fastposts/{postId}";
		}

		public static class User
		{
			public const string Register = Base + "/user/register";
			public const string Login = Base + "/user/login";
		}
	}
}

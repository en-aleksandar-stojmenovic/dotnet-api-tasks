namespace ProjectSetup.Contracts.V3
{
	public static class ApiRoutes
	{
		public const string Root = "api";
		public const string Version = "v3";
		public const string Base = Root + "/" + Version;

		public static class Categories
		{
			public const string GetAll = Base + "/categories";
		}
	}
}

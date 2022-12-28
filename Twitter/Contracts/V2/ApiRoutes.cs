﻿namespace Twitter.Contracts.V2
{
	public static class ApiRoutes
	{
		public const string Root = "api";
		public const string Version = "v2";
		public const string Base = Root + "/" + Version;

		public static class Categories
		{
			public const string GetAll = Base + "/categories";
		}
	}
}
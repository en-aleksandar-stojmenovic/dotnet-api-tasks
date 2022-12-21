namespace Tests.Helpers
{
	public static class StringHelpers
	{
		public static string GetRandomString(int length)
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

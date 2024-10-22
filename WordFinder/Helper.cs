namespace WordFinder
{
	public static class Helper
	{
        public static IEnumerable<string> GetMatrix()
        {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var matrix = Enumerable.Range(0, 64)
                .Select(i => new string(Enumerable.Repeat(chars, 64)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray()))
                .ToList();

            return matrix;
        }

        public static IEnumerable<string> GetWordStream(int size, int charsize)
        {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var wordStream = Enumerable.Range(0, size)
                .Select(i => new string(Enumerable.Repeat(chars, charsize)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray()))
                .ToList();

            return wordStream;
        }
    }
}


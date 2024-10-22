namespace WordFinder;

class Program
{
    static void Main(string[] args)
    {
        var matrix = Helper.GetMatrix(); // max 64 * 64 matrix of words
        var wordStream = Helper.GetWordStream(10000, 4);

        WordFinder finder = new WordFinder(matrix);
        
        var highFrequncywords = finder.Find(wordStream);
        
        foreach (string word in highFrequncywords)
        {
            Console.WriteLine(word);
        }

        Console.ReadLine();
    }
}


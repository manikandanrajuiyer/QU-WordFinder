namespace WordFinder.UnitTests;

public class WordFinderTests
{
    
    [Test]
    public void FindTopFrequncyWordWithGivenInput()
    {
        List<string> matrix = new() { "APPLEAPPLE", "APPLE123", "ABCDCD" };
        List<string> wordStream = new() { "APPLE", "CD" };

        WordFinder finder = new WordFinder(matrix);
        var mostRepeatedwords = finder.Find(wordStream);

        var result = new List<string> { "APPLE", "CD" };

        Assert.True(mostRepeatedwords.SequenceEqual(result));
    }

    [Test]
    public void FindTopFrequncyWordWithDuplicatates()
    {
        List<string> matrix = new() { "APPLEAPPLE", "APPLE123", "ABCDCD" };
        List<string> wordStream = new() { "APPLE", "CD", "APPLE", "APPLE" };

        WordFinder finder = new WordFinder(matrix);
        var mostRepeatedwords = finder.Find(wordStream);

        var result = new List<string> { "APPLE", "CD" };

        Assert.True(mostRepeatedwords.SequenceEqual(result));
    }

    [Test]
    public void FindTopFrequncyWordWithCaseInsensitive()
    {
        List<string> matrix = new() { "APPLEAPPLE", "APPLE123", "ABCDCD", "accdCDAB", "SUN", "MOON"};
        List<string> wordStream = new() { "APPLE", "cd", "APPLE", "APPLE", "SUN" };

        WordFinder finder = new WordFinder(matrix);
        var mostRepeatedwords = finder.Find(wordStream);

        var result = new List<string> { "cd", "APPLE", "SUN"};

        Assert.True(mostRepeatedwords.SequenceEqual(result));
    }

    [Test]
    public void FindTopFrequncyWordReturnsEmptyIfNoMatch()
    {
        List<string> matrix = new() { "APPLEAPPLE", "APPLE123", "ABCDCD", "accdCDAB" };
        List<string> wordStream = new() { "MANI", "WORD", "45", "SUN" };

        WordFinder finder = new WordFinder(matrix);
        var mostRepeatedwords = finder.Find(wordStream);

        var result = new List<string> { "cd", "APPLE" };

        Assert.True(!mostRepeatedwords.Any());
    }

    [Test]
    public void FindTopFrequncyWordWith500StreamOfWords()
    {
        List<string> matrix = new() { "APPLEAPPLE", "APPLE123", "ABCDCD", "accdCDAB" };
        Random random = new Random();
        var wordStream = Helper.GetWordStream(500, 10);

        WordFinder finder = new WordFinder(matrix);
 
        Assert.DoesNotThrow(() => finder.Find(wordStream));
    }

    [Test]
    public void FindTopFrequncyWordWithMillionWordStream()
    {
        var matrix = Helper.GetMatrix(); // max 64 * 64 matrix of words
        var wordStream = Helper.GetWordStream(1000000, 10);

        WordFinder finder = new WordFinder(matrix);

        Assert.DoesNotThrow(() => finder.Find(wordStream));
    }
}

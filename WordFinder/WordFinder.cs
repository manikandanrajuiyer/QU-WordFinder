namespace WordFinder
{
    public class WordFinder
    {
        private  IEnumerable<string> _matrix;
        private readonly TrieNode _trieRoot;

        public WordFinder(IEnumerable<string> matrix)
        {
            _matrix = matrix;
            _trieRoot = new TrieNode();
        }

        public IEnumerable<string> Find(IEnumerable<string> wordStream)
        {
            // 1. Remove Duplicates and make it case Insensitive
            // 2. Performance and memory impact based on large word stream not because of matrix since it is max of 64 * 64
            // 3. Try avoiding string manupulation as much as possible
            // 4. Using efficient trie search algorithm for faster lookup match
          
            
            int topNFrequenceywords = 10; // Need only top 10 words
            Dictionary<string, int> wordCounts = new(); // stores count of each word in the matrix

            //word stream may contain duplicate words and different cases, so to make it case insentive
            //using Hashset that automatically handles removing duplicates
            HashSet<string> uniqueWords = new(wordStream, StringComparer.OrdinalIgnoreCase);

            // If matrix or wordstream is empty, return to avoid unnecessary processing
            if (!_matrix.Any() || !uniqueWords.Any())
            {
                return Enumerable.Empty<string>();
            }

            // matrix length is fixed, 64 * 64, however wordstream can be any number in the range of 100000, possible?
            // for performance and memory, try to avoid using any string maupulation substring, indexOf etc
            // which will impact memory utilization since it creates new strings
            // Insert all words from wordstream into the Trie - make trie case insenstive
            foreach (var word in uniqueWords)
            {
                // we have a limit for 64 charectors for matrix so do not process if word is more than 64 char
                if (word.Length <= 64)
                {
                    BuildTrie(word);
                }
            }

            //Loop through matrix to check against Trie and count number of occurances of each word in wordstream
            // each _matrix string may contain multiple occurances of word in wordstream
            foreach (var matrixWord in _matrix)
            {
                CountFrequencyOfOccurances(matrixWord, wordCounts);
            }

            // we may need to sort dictionary to find top 10 words
            //sorting may impact performance depending on number of items in dictionary
            // so using min heap implementation of .net priority queue to always maintain only top 10 items

            PriorityQueue<KeyValuePair<string, int>, int> highFrequncywords = new();

            // only maintain top 10 words found
            foreach (var wordCount in wordCounts)
            {
                highFrequncywords.Enqueue(wordCount, wordCount.Value);
                if (highFrequncywords.Count > topNFrequenceywords)
                {
                    highFrequncywords.Dequeue();
                }
            }

            // take only words to output final results
            //it has only 10 items so it will not affect performance
            List<string> result = new(topNFrequenceywords);

            while (highFrequncywords.Count > 0)
            {
                result.Add(highFrequncywords.Dequeue().Key);
            }

            // Reverse the result list to show the highest frequencies first
            result.Reverse();

            return result;
        }

        // Insert a word into the Trie case-insensitive
        private void BuildTrie(string word)
        {
            var currentNode = _trieRoot;

            // Insert each character, make it case-insensitive
            foreach (var ch in word)
            {
                //ToLower()? will it affect performance? its a char not a string, so may be minimal overhead
                // if problem then we may need to look at other solution to avoid this
                char normalizedChar = char.ToLower(ch);

                if (!currentNode.Children.ContainsKey(normalizedChar))
                {
                    currentNode.Children[normalizedChar] = new TrieNode();
                }

                currentNode = currentNode.Children[normalizedChar];
            }
            currentNode.IsWord = true;
            currentNode.Word = word; // Store the full word at the leaf node
        }

        // Find all occurrences of words from the matrix in the Trie case-insensitive
        private void CountFrequencyOfOccurances(string word, Dictionary<string, int> wordCounts)
        {
            int length = word.Length;

            //loop through and match by each charactors
            for (int start = 0; start < length; start++)
            {
                var currentNode = _trieRoot;

                // Try to match the word starting from the current character
                for (int i = start; i < length; i++)
                {
                    char currentChar = word[i];
                    //ToLower()? will it impact memeory? 
                    char normalizedChar = char.ToLower(currentChar); 

                    //  break the loop, if it does not match
                    if (!currentNode.Children.ContainsKey(normalizedChar))
                    {
                        break;
                    }

                    currentNode = currentNode.Children[normalizedChar];

                    // If a word is found, count its occurrence 
                    if (currentNode.IsWord)
                    {
                        string? fullWord = currentNode.Word;

                        if (!string.IsNullOrEmpty(fullWord))
                        {
                            if (wordCounts.ContainsKey(fullWord))
                            {
                                wordCounts[fullWord]++;
                            }
                            else
                            {
                                wordCounts[fullWord] = 1;
                            }
                        }
                    }
                }
            }
        }
    }
}

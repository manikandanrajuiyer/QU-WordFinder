using System;
namespace WordFinder
{
    // Trie Node class
    public class TrieNode
    {
        public Dictionary<char, TrieNode> Children { get; set; } = new Dictionary<char, TrieNode>();
        public bool IsWord { get; set; } = false;
        public string? Word { get; set; } = null; // Store the full word at the leaf node
    }
}


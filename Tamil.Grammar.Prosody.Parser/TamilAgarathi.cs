using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gma.DataStructures.StringSearch;

namespace Tamil.Grammar.Prosody.Parser
{
    public class TamilAgarathi : ITamilAgarathi
    {
        private ITrie<string> Trie { set; get; }
        private bool _isLoaded = false;

        private static IEnumerable<string> GetTamilWordsFromFile(string file)
        {
            using (StreamReader reader = File.OpenText(file))
            {
                Console.WriteLine("Processing file {0} ...", file);
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                int wordCount = 0;
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    wordCount++;
                    List<string> words = new List<string> {line};
                    foreach (string word in words)
                    {
                        yield return word;
                    }

                }
                stopWatch.Stop();
                Console.WriteLine("Total# of Words:{0}\tTime:{1}", wordCount, stopWatch.Elapsed);
            }
        }

        private void BuildUpTamil(string fileName, ITrie<string> trie)
        {
            IEnumerable<string> allWordsInFile = GetTamilWordsFromFile(fileName);
            foreach (var word in allWordsInFile)
            {
                trie.Add(word, word);
            }
            _isLoaded = true;
        }
        public TamilAgarathi(string agarathiFileName= "agarathiValid.txt", int minimumSuffixLength = 1)
        {
            Trie = new SuffixTamilTrie<string>(minimumSuffixLength);
            BuildUpTamil(agarathiFileName, Trie);
        }
        public void Seed(string agarathiFileName, int minimumSuffixLength = 1)
        {
            if (!_isLoaded)
            {
                Trie = new SuffixTamilTrie<string>(minimumSuffixLength);
                BuildUpTamil(agarathiFileName, Trie);
            }
        }

        public IEnumerable<string> Lookup(string word)
        {
            return Trie.Retrieve(word).ToArray();
        }
    }
}

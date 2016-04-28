// This code is distributed under MIT license. Copyright (c) 2013 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php
using System.Collections.Generic;
using System.Linq;
using RjamSoft.Tamil.Grammar.Prosody.Core;

namespace Gma.DataStructures.StringSearch
{
    public class SuffixTamilTrie<T> : ITrie<T>
    {
        private readonly Trie<T> m_InnerTrie;
        private readonly int m_MinSuffixLength;

        public SuffixTamilTrie(int minSuffixLength)
            : this(new Trie<T>(), minSuffixLength)
        {
        }

        private SuffixTamilTrie(Trie<T> innerTrie, int minSuffixLength)
        {
            m_InnerTrie = innerTrie;
            m_MinSuffixLength = minSuffixLength;
        }

        public IEnumerable<T> Retrieve(string query)
        {
            return
                m_InnerTrie
                    .Retrieve(query)
                    .Distinct();
        }

        public void Add(string key, T value)
        {
            foreach (string suffix in GetAllSuffixes(m_MinSuffixLength, key))
            {
                m_InnerTrie.Add(suffix, value);
            }
        }

        private static IEnumerable<string> GetAllSuffixes(int minSuffixLength, string word)
        {
            var latinWord = Transliterator.Tamil2Latin(word);
            for (int i = latinWord.Length - minSuffixLength *2; i >= 0; i-=2)
            {
                var partition = new StringPartition(latinWord, i);
                yield return Transliterator.Latin2Tamil(partition.ToString());
            }
        }
    }
}
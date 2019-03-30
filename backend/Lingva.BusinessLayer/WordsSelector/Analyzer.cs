using Lingva.BusinessLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Lingva.BusinessLayer.Interfaces;

namespace Lingva.BusinessLayer.WordsSelector
{
    public class Analyzer
    {
        private readonly ICommonWord _dataProvider;
        private readonly char[] splitSymbol = {' ', '.', ',', ':', '?', '!', '\n', '\r'};

        public Analyzer(ICommonWord dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public List<Word> ParseToWords(string allSubtitles)
        {
            var splitSubtitle = allSubtitles.Split(splitSymbol, StringSplitOptions.RemoveEmptyEntries);
            var numbers = "[0-9]+";
            var numbersRegex = new Regex(numbers);
            var tag = "<.*>";
            var tagRegex = new Regex(tag);
            var open = ">";
            var openRegex = new Regex(open);
            var closed = "<";
            var closedRegex = new Regex(closed);

            var withoutSing = new List<string>();
            foreach (var item in splitSubtitle)
            {
                string newString;
                newString = numbersRegex.Replace(item, string.Empty);
                newString = tagRegex.Replace(newString, string.Empty);
                newString = openRegex.Replace(newString, string.Empty);
                newString = closedRegex.Replace(newString, string.Empty);
                newString = newString.Replace("[", string.Empty);
                newString = newString.Replace("]", string.Empty);
                newString = newString.Replace("--", string.Empty);
                if (!string.IsNullOrEmpty(newString) && newString.Length > 2 && !newString.Contains("'"))
                    withoutSing.Add(newString.ToLower());
            }

            var words = new List<Word>();

            foreach (var item in withoutSing)
                if (!Contain(item, words))
                {
                    var totalNumber = 0;
                    foreach (var item1 in withoutSing)
                        if (item1 == item)
                            totalNumber++;
                    words.Add(new Word(item, totalNumber));
                }

            return words;
        }

        public List<Word> RemoveSimpleWords(List<Word> words)
        {
            var simpleW = _dataProvider.GetAllSimpleWords();
            foreach (var item in simpleW) DeleteEll(item, words);
            return words;
        }

        public List<Word> RemoveAlreadyAvailableInDictionary(List<Word> words, int userId)
        {
            var userDictionary = _dataProvider.GetUserDictionary(userId);
            var result = new List<Word>();
            foreach (var item in words)
                if (!userDictionary.Contains(item.Name))
                    result.Add(new Word(item.Name, item.Count));

            return result;
        }

        public void Dispose()
        {
            _dataProvider.Dispose();
        }

        public List<Word> RemoveNonExistent(List<Word> words)
        {
            var exist = new List<Word>();
            foreach (var item in words)
                if (_dataProvider.AreExist(item.Name))
                    exist.Add(new Word(item.Name, item.Count));
            return exist;
        }

        private void DeleteEll(string vd, List<Word> words)
        {
            for (var i = 0; i < words.Count; i++)
                if (words[i].Name == vd)
                {
                    words.RemoveAt(i);
                    return;
                }
        }

        private bool Contain(string wd, List<Word> words)
        {
            foreach (var item in words)
                if (item.Name == wd)
                    return true;

            return false;
        }
    }
}
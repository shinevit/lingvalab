using Lingva.BusinessLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Lingva.BusinessLayer.WordsSelector
{
    public class Analyzer
    {
        ICommonWord _dataProvider;
        char[] splitSymbol = new char[] { ' ', '.', ',', ':', '?', '!', '\n', '\r' };

        public Analyzer(ICommonWord dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public List<Word> ParseToWords(string allSubtitles)
        {

            string[] splitSubtitle = allSubtitles.Split(splitSymbol, StringSplitOptions.RemoveEmptyEntries);
            string numbers = "[0-9]+";
            Regex numbersRegex = new Regex(numbers);
            string tag = "<.*>";
            Regex tagRegex = new Regex(tag);
            string open = ">";
            Regex openRegex = new Regex(open);
            string closed = "<";
            Regex closedRegex = new Regex(closed);

            List<string> withoutSing = new List<string>();
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
                {
                    withoutSing.Add(newString.ToLower());
                }
            }

            List<Word> words = new List<Word>();

            foreach (string item in withoutSing)
            {
                if (!Contain(item, words))
                {
                    int totalNumber = 0;
                    foreach (string item1 in withoutSing)
                    {
                        if (item1 == item)
                        {
                            totalNumber++;
                        }
                    }
                    words.Add(new Word(item, totalNumber));
                }
            }

            return words;
        }

        public List<Word> RemoveSimpleWords(List<Word> words)
        {
            List<string> simpleW = _dataProvider.GetAllSimpleWords();
            foreach (string item in simpleW)
            {
                DeleteEll(item, words);
            }
            return words;
        }

        public List<Word> RemoveAlreadyAvailableInDictionary(List<Word> words, int userId)
        {
            List<string> userDictionary = _dataProvider.GetUserDictionary(userId);
            List<Word> result = new List<Word>();
            foreach (Word item in words)
            {
                if (!userDictionary.Contains(item.Name))
                {
                    result.Add(new Word(item.Name, item.Count));
                }
            }

            return result;
        }

        public void Dispose()
        {
            _dataProvider.Dispose();
        }

        public List<Word> RemoveNonExistent(List<Word> words)
        {
            List<Word> exist = new List<Word>();
            foreach (Word item in words)
            {
                if (_dataProvider.AreExist(item.Name))
                {
                    exist.Add(new Word(item.Name, item.Count));
                }
            }
            return exist;
        }

        private void DeleteEll(string vd, List<Word> words)
        {
            for (int i = 0; i < words.Count; i++)
            {
                if (words[i].Name == vd)
                {
                    words.RemoveAt(i);
                    return;
                }
            }
        }

        private bool Contain(string wd, List<Word> words)
        {
            foreach (Word item in words)
            {
                if (item.Name == wd)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

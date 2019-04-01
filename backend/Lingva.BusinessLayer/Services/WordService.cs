using System;
using System.Collections.Generic;
using Lingva.BusinessLayer.Contracts;
using Lingva.DataAccessLayer.Entities;
using Lingva.DataAccessLayer.Repositories;

namespace Lingva.BusinessLayer.Services
{
    public class WordService : IWordService
    {
        private readonly string[] _separators = {" ", ",", ".", "!", "?", ";", ":", "<i>", "<h>"};
        private readonly IUnitOfWorkParser _unitOfWork;

        public WordService(IUnitOfWorkParser unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ParserWord> GetAll()
        {
            return _unitOfWork.ParserWords.GetList();
        }

        public ParserWord GetParserWordByName(string name)
        {
            var word = _unitOfWork.ParserWords.Get(name);
            return word;
        }

        public bool AddWordsFromRow(SubtitleRow row)
        {
            if (row == null || string.IsNullOrEmpty(row.Value)) return false;

            var add = false;

            string[] words;

            if (TryParseWords(row.Value, out words))
            {
                add = true;

                var language = _unitOfWork.Subtitles.Get(s => s.Id == row.SubtitleId).LanguageName ?? "en";

                foreach (var word in words) AddWord(word, language, row.Id);
            }

            return add;
        }

        public bool AddWordFromPhrase(string phrase, string language = "en", int? rowId = null)
        {
            if (string.IsNullOrEmpty(phrase)) return false;

            var add = false;

            string[] words;

            if (TryParseWords(phrase, out words))
            {
                add = true;

                foreach (var word in words) AddWord(word, language, rowId);
            }

            return add;
        }

        public bool ExistsWord(string wordName)
        {
            return _unitOfWork.ParserWords.Get(c => c.Name == wordName) != null;
        }

        private bool TryParseWords(string line, out string[] words)
        {
            if (string.IsNullOrEmpty(line))
            {
                words = null;
                return false;
            }

            words = line.Split(_separators, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length == 0) return false;

            return true;
        }

        private bool AddWord(string word, string language = "en", int? subtitleRowId = null)
        {
            if (ExistsWord(word)) return false;

            var newWord = new ParserWord
            {
                Name = word,
                LanguageName = language,
                SubtitleRowId = subtitleRowId
            };

            _unitOfWork.ParserWords.Create(newWord);
            _unitOfWork.Save();

            return true;
        }
    }
}
using Lingva.BusinessLayer.Contracts;
using Lingva.BusinessLayer.SubtitlesParser.Classes;
using Lingva.DataAccessLayer.Entities;
using Lingva.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Lingva.BusinessLayer.Services
{
    public class ParserWordService: IParserWordService
    {
        private readonly IUnitOfWorkParser _unitOfWork;

        private readonly string[] _separators = { " ", ",", ".", "!", "?", ";", ":", "<i>", "<h>" };

        public ParserWordService(IUnitOfWorkParser unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ParserWord GetParserWordWhere(Expression<Func<ParserWord, bool>> predicate)
        {
            return _unitOfWork.ParserWords.Get(predicate);
        }

        public ParserWord GetParserWord(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return null;
            }

            ParserWord parserWord = _unitOfWork.ParserWords.Get(name);

            return parserWord;
        }

        public IQueryable<ParserWord> GetAllParserWords()
        {
            return _unitOfWork.ParserWords.GetList();
        }

        public bool AddWordsFromRow(SubtitleRow row)
        {
            if (row == null || string.IsNullOrEmpty(row.Value))
            {
                return false;
            }

            bool add = false;

            string[] words;

            if (TryParseWords(row.Value, out words))
            {
                add = true;

                string language = _unitOfWork.Subtitles.Get(s => s.Id == row.SubtitleId).LanguageName ?? "en";

                foreach (string word in words)
                {
                    AddParserWord(word, language, row.Id );
                }
            }
            
            return add;
        }

        public bool AddWordFromPhrase(string phrase, string language = "en", int? rowId = null)
        {
            if (string.IsNullOrEmpty(phrase))
            {
                return false;
            }

            bool add = false;

            string[] words;

            if (TryParseWords(phrase, out words))
            {
                add = true;

                foreach (string word in words)
                {
                    AddParserWord(word, language, rowId);
                }
            }

            return add;
        }

        public bool ExistsParserWord(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return false;
            }

            return _unitOfWork.ParserWords.Get(c => c.Name == name) != null;
        }

       

        public void AddParserWord(ParserWord word)
        {
            AddParserWord(word.Name);

            if (!ExistsParserWord(word.Name))
            {
                _unitOfWork.ParserWords.Create(word);
                _unitOfWork.Save();
            }
        }

        public void UpdateParserWord(ParserWord parserWord)
        {
            if(parserWord == null || String.IsNullOrEmpty(parserWord.Name))
            {
                return;
            }

            ParserWord wordToUpdate = _unitOfWork.ParserWords.Get(parserWord.Name);

            wordToUpdate.LanguageName = parserWord.LanguageName;
            wordToUpdate.SubtitleRowId = parserWord.SubtitleRowId;
            _unitOfWork.ParserWords.Update(wordToUpdate);
            _unitOfWork.Save();
        }

        public void DeleteParserWord(string name)
        {
            if(String.IsNullOrEmpty(name))
            {
                return;
            }

            ParserWord parserWord = _unitOfWork.ParserWords.Get(name);

            if (parserWord == null)
            {
                return;
            }

            _unitOfWork.ParserWords.Delete(parserWord);
            _unitOfWork.Save();
        }

        private bool TryParseWords(string line, out string[] words)
        {
            if (string.IsNullOrEmpty(line))
            {
                words = null;
                return false;
            }

            words = line.Split(_separators, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length == 0)
            {
                return false;
            }

            return true;
        }
        private bool AddParserWord(string word, string language = "en", int? subtitleRowId = null)
        {
            if (ExistsParserWord(word))
            {
                return false;
            }

            ParserWord newWord = new ParserWord
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


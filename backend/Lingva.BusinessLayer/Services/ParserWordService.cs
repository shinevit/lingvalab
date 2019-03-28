using Lingva.BusinessLayer.Contracts;
using Lingva.BusinessLayer.DTO;
using Lingva.DataAccessLayer.Entities;
using Lingva.DataAccessLayer.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Lingva.BusinessLayer.Services
{
    public class ParserWordService: IParserWordService
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IUnitOfWorkParser _unitOfWork;

        private readonly string[] _separators = { " ", ",", ".", "!", "?", ";", ":", "<i>", "<h>" };
        private const string ERR_ARG_NULL_EXP_GET = "Tried to get ParserWord record with null Name primary key.";
        private const string EXP_GENERATED = "The ArgumentNullException exception is generated.";
        private const string ERR_NULL_REF = "There is not any record in the ParserWords table the in database.";
        private const string ERR_GET_ALL = "All ParserWords records is returned to the client side.";

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
                _logger.Warn(ERR_ARG_NULL_EXP_GET);
                _logger.Warn(EXP_GENERATED);

                throw new ArgumentNullException(ERR_ARG_NULL_EXP_GET);
            }

            _logger.Info("Attempt to get \"{name}\" record from the ParserWords table occured.");

            ParserWord parserWord = _unitOfWork.ParserWords.Get(name);

            if(parserWord == null)
            {
                _logger.Info($"There is no ParserWord record with \"{name}\" in the ParserWords table.");
            }

            _logger.Info("Attempt to get \"{name}\" record from ParserWords table succeeded.");

            return parserWord;
        }

        public IEnumerable<ParserWord> GetAllParserWords()
        {
            IEnumerable<ParserWord> result = _unitOfWork.ParserWords.GetList();

            if (result == null)
            {
                _logger.DebugException(ERR_NULL_REF, new NullReferenceException());

                throw new NullReferenceException(ERR_NULL_REF);
            }

            _logger.Debug(ERR_GET_ALL);

            return _unitOfWork.ParserWords.GetList();
        }

        public IEnumerable<ParserWordDTO> AddParserWordsFromRow(SubtitleRow row)
        {
            if (row == null || string.IsNullOrEmpty(row.Value))
            {
                return null;
            }

            string[] words;

            List<ParserWordDTO> wordsDto = new List<ParserWordDTO>();
            List<ParserWord> wordsToCreate = new List<ParserWord>();

            if (TryParseWords(row.Value, out words))
            {
                string language = row.LanguageName;
                int subtitleRowId = row.Id;

                foreach (string word in words)
                {
                    ParserWord newWord = new ParserWord
                    {
                        Name = word,
                        LanguageName = language,
                        SubtitleRowId = subtitleRowId
                    };

                    wordsToCreate.Add(newWord);

                    ParserWordDTO newWordDto = new ParserWordDTO()
                    {
                        Name = word,
                        LanguageName = language,
                        SubtitleRowId = subtitleRowId
                    };

                    wordsDto.Add(newWordDto);
                }

                AddRangeParserWords(wordsToCreate);

                IEnumerable<ParserWordDTO> results = (IEnumerable<ParserWordDTO>)wordsDto; 

                return results;
            }

            return null;
        }

        public void AddRangeParserWords(IEnumerable<ParserWord> words)
        {
            if(words == null)
            {
                return;
            }

            _unitOfWork.ParserWords.CreateRange(words);
            _unitOfWork.Save();
        }
       
        public void AddParserWord(ParserWord word)
        {
            if (word == null || string.IsNullOrEmpty(word.Name))
            {
                return;
            }

            _unitOfWork.ParserWords.Create(word);
            _unitOfWork.Save();
        }

        public void InsertOrUpdateParserWord(ParserWord word)
        {
            if (word == null || string.IsNullOrEmpty(word.Name))
            {
                return;
            }

            _unitOfWork.ParserWords.InsertOrUpdate(word);

            _unitOfWork.Save();
        }

        public ParserWord DeleteParserWord(string name)
        {
            if(String.IsNullOrEmpty(name))
            {
                return null;
            }

            ParserWord parserWord = _unitOfWork.ParserWords.Get(name);

            if (parserWord == null)
            {
                throw new ArgumentNullException("The ParserWord entity does not exist in the database.");
            }

            _unitOfWork.ParserWords.Delete(parserWord);
            _unitOfWork.Save();

            return parserWord;
        }

        public bool ExistsParserWord(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return false;
            }

            return _unitOfWork.ParserWords.Exists(name);
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
    }
}


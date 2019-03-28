using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Lingva.DataAccessLayer.Repositories
{
    public class RepositoryParserWord : Repository<ParserWord>, IRepositoryParserWord
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private const string ERR_ARG_NULL_EXP_GET = "Tried to get ParserWord record with a null Name primary key.";
        private const string ERR_ARG_NULL_EXP_UPDATE = "Tried to insert or update null ParserWord entity.";
        private const string ERR_ARG_NULL_EXP_CREATE = "Tried to create null ParserWord entity.";
        private const string ERR_ARG_NULL_EXP_CREATE_RANGE = "Tried to create null ParserWord range.";
        private const string ERR_ARG_NULL_EXP_DELETE = "Tried to delete null ParserWord record.";
        private const string ERR_ARG_NULL_EXP_CHECK = "Tried to check ParserWord record with null Name primary key for existence.";
        private const string EXP_GENERATED = "The ArgumentNullException exception is generated.";

        public RepositoryParserWord(DictionaryContext context)
            : base(context)
        {
        }

        public override ParserWord Get(object name)
        {
            if(name == null)
            {
                _logger.ErrorException(ERR_ARG_NULL_EXP_GET, new ArgumentNullException(ERR_ARG_NULL_EXP_GET));

                throw new ArgumentNullException(ERR_ARG_NULL_EXP_GET);
            }

            var result = _context.ParserWords.Where(w => w.Name == name.ToString()).Select(w => w).FirstOrDefault();

            if (result == null)
            {
                _logger.Debug("The received object name is not found in the database.");
            }

            _logger.Debug("The record of ParserWords table is found.");

            return result;
        }

        public override void Create(ParserWord word)
        {
            if (word == null || string.IsNullOrEmpty(word.Name))
            {
                _logger.ErrorException(ERR_ARG_NULL_EXP_CREATE, new ArgumentNullException(ERR_ARG_NULL_EXP_CREATE));

                throw new ArgumentNullException(ERR_ARG_NULL_EXP_CREATE); 
            }

            _context.ParserWords.Add(word);
            _logger.Debug("The ParserWord record is added to the database.");
        }

        public override void Delete(ParserWord word)
        {
            if (word == null)
            {
                _logger.ErrorException(ERR_ARG_NULL_EXP_DELETE, new ArgumentNullException(ERR_ARG_NULL_EXP_DELETE));

                throw new ArgumentNullException(ERR_ARG_NULL_EXP_DELETE);
            }

            if (_context.Entry(word).State == EntityState.Detached)
            {
                _context.ParserWords.Attach(word);
            }

            _context.ParserWords.Remove(word);
            _logger.Debug("The ParserWord record is deleted from the database.");
        }

        public void CreateRange(IEnumerable<ParserWord> words)
        {
            if (words == null)
            {
                _logger.ErrorException(ERR_ARG_NULL_EXP_CREATE_RANGE, new ArgumentNullException(ERR_ARG_NULL_EXP_CREATE_RANGE));

                throw new ArgumentNullException(ERR_ARG_NULL_EXP_CREATE_RANGE);
            }

            _entities.AddRange(words);
            _logger.Debug("The range of ParserWord records is added to the database.");
        }

        public bool InsertOrUpdate(ParserWord word)
        {
            if (word == null || string.IsNullOrEmpty(word.Name))
            {
                _logger.ErrorException(ERR_ARG_NULL_EXP_UPDATE, new ArgumentNullException(ERR_ARG_NULL_EXP_UPDATE));

                throw new ArgumentNullException(ERR_ARG_NULL_EXP_UPDATE); 
            }

            if (Exists(word.Name))
            {
                _context.ParserWords.Update(word);
                //_context.Entry(word).CurrentValues.SetValues(word);

                _logger.Debug("The ParserWord record is updated.");

                return true;
            }

            _context.ParserWords.Add(word);
            _logger.Debug("The ParserWord record is added to the database.");

            return false;
        }

        public bool Any()
        {
            return _context.ParserWords.Any();
        }

        public bool Exists(string name)
        {
            if (name == null || string.IsNullOrEmpty(name))
            {
                _logger.ErrorException(ERR_ARG_NULL_EXP_CHECK, new ArgumentNullException(ERR_ARG_NULL_EXP_CHECK));

                throw new ArgumentNullException(ERR_ARG_NULL_EXP_CHECK);
            }

            _logger.Debug("Check if there is any {name} ParserWord record in database.", name);

            return _context.ParserWords.Any(w => w.Name == name);
        }
    }
}


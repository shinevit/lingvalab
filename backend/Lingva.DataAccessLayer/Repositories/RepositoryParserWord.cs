﻿using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Lingva.DataAccessLayer.Repositories
{
    public class RepositoryParserWord : Repository<ParserWord>, IRepositoryParserWord
    {
        private const string ERR_ARG_NULL_EXP_UPDATE = "Tried to insert or update null ParserWord entity.";
        private const string ERR_ARG_NULL_EXP_CREATE = "Tried to create null ParserWord entity.";
        private const string ERR_ARG_NULL_EXP_CREATE_RANGE = "Tried to create null ParserWord range.";

        public RepositoryParserWord(DictionaryContext context)
            : base(context)
        {
        }

        public override ParserWord Get(object name)
        {
            var result = _context.ParserWords.Where(w => w.Name == name.ToString()).Select(w => w).FirstOrDefault();

            return result;
        }

        public override void Create(ParserWord word)
        {
            if (word == null || string.IsNullOrEmpty(word.Name))
            {
                throw new ArgumentNullException(ERR_ARG_NULL_EXP_CREATE); 
            }

            _context.ParserWords.Add(word);
        }

        public override void Delete(ParserWord word)
        {
            if (_context.Entry(word).State == EntityState.Detached)
            {
                _context.ParserWords.Attach(word);
            }

            _context.ParserWords.Remove(word);
        }

        public void CreateRange(IEnumerable<ParserWord> words)
        {
            if (words == null)
            {
                throw new ArgumentNullException(ERR_ARG_NULL_EXP_CREATE_RANGE);
            }

            _entities.AddRange(words);
        }
        public void InsertOrUpdate(ParserWord word)
        {
            if (word == null || string.IsNullOrEmpty(word.Name))
            {
                throw new ArgumentNullException(ERR_ARG_NULL_EXP_UPDATE); 
            }

            if (Exists(word.Name))
            {
                //_context.Update(word);
                _context.Entry(word).CurrentValues.SetValues(word);

                return;
            }

            _context.ParserWords.Add(word);
           
        }

        public bool Any()
        {
            return _context.ParserWords.Any();
        }

        public bool Exists(string wordName)
        {
            return _context.ParserWords.Any(w => w.Name == wordName);
        }
    }
}


using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Lingva.DataAccessLayer.Repositories
{
    public class RepositoryParserWord: Repository<ParserWord>, IRepositoryParserWord  
    {
        private const string ERR_ARG_NULL_EXP = "Tried to insert null ParserWord entity!";
        
        public RepositoryParserWord(DictionaryContext context)
            :base(context)
        {
        }

        public override ParserWord Get(object name)
        {
            var result = _context.ParserWords.Where(w => w.Name == name.ToString()).Select(w => w).FirstOrDefault();

            return result;
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


using System;
using Lingva.DataAccessLayer.Context;

namespace Lingva.DataAccessLayer.Repositories
{
    namespace Lingva.DataAccessLayer.Repositories
    {
        public class UnitOfWorkParser : IUnitOfWorkParser
        {
            private static DictionaryContext _context;
            
            private readonly IRepositoryFilm _films;
            public UnitOfWorkParser(DictionaryContext context, IRepositorySubtitle subtitles, IRepositorySubtitleRow subtitleRows,
                IRepositoryParserWord words, IRepositoryFilm films)
            {
                _context = context;
                Subtitles = subtitles;
                SubtitleRows = subtitleRows;
                ParserWords = words;
                _films = films;
            }

            public IRepositorySubtitle Subtitles { get => _subtitles; }

            public IRepositorySubtitleRow SubtitleRows { get => _subtitleRows; }
      
            public IRepositoryParserWord ParserWords { get => _parserWords; }

            public IRepositoryFilm Films { get => _films; }

            public void Save()
            {
                _context.SaveChanges();
            }

        }
    }
}
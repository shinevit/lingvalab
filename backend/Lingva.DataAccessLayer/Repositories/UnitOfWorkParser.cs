using System;
using System.Collections.Generic;
using System.Text;

namespace Lingva.DataAccessLayer.Repositories
{
    using global::Lingva.DataAccessLayer.Context;
    using System;
    using System.Collections.Generic;
    using System.Text;

    namespace Lingva.DataAccessLayer.Repositories
    {
        public class UnitOfWorkParser : IUnitOfWorkParser
        {
            private static DictionaryContext _context;
            private bool disposed = false;

            private readonly IRepositorySubtitle _subtitles;
            private readonly IRepositorySubtitleRow _subtitleRows;
            private readonly IRepositoryParserWord _parserWords;
            private readonly IRepositoryFilm _films;
            public UnitOfWorkParser(DictionaryContext context, IRepositorySubtitle subtitles, IRepositorySubtitleRow subtitleRows,
                IRepositoryParserWord words, IRepositoryFilm films)
            {
                _context = context;
                _subtitles = subtitles;
                _subtitleRows = subtitleRows;
                _parserWords = words;
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

            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                        _context.Dispose();
                    }
                }
                this.disposed = true;
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }
    }

}

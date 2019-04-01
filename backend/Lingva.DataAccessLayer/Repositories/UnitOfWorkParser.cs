using System;
using Lingva.DataAccessLayer.Context;

namespace Lingva.DataAccessLayer.Repositories
{
    namespace Lingva.DataAccessLayer.Repositories
    {
        public class UnitOfWorkParser : IUnitOfWorkParser
        {
            private static DictionaryContext _context;

            private bool disposed;

            public UnitOfWorkParser(DictionaryContext context, IRepositorySubtitle subtitles,
                IRepositorySubtitleRow subtitleRows, IRepositoryParserWord words)
            {
                _context = context;
                Subtitles = subtitles;
                SubtitleRows = subtitleRows;
                ParserWords = words;
            }

            public IRepositorySubtitle Subtitles { get; }
            public IRepositorySubtitleRow SubtitleRows { get; }
            public IRepositoryParserWord ParserWords { get; }

            public void Save()
            {
                _context.SaveChanges();
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!disposed)
                    if (disposing)
                        _context.Dispose();
                disposed = true;
            }
        }
    }
}
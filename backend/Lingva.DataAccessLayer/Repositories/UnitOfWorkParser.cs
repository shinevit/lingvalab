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
            private readonly IRepositoryWord _words;
            public IRepositorySubtitle Subtitles { get => _subtitles; }
            public IRepositorySubtitleRow SubtitleRows { get => _subtitleRows; }
            public IRepositoryWord Words { get => _words; }

            public UnitOfWorkParser(DictionaryContext context, IRepositorySubtitle subtitles, IRepositorySubtitleRow subtitleRows, IRepositoryWord words)
            {
                _context = context;
                _subtitles = subtitles;
                _subtitleRows = subtitleRows;
                _words = words;
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool dispose)
            {
                if (!this.disposed && dispose)
                {
                    _context.Dispose();
                }

                disposed = true;
            }

            public void Save()
            {
                _context.SaveChanges();
            }
        }
    }

}

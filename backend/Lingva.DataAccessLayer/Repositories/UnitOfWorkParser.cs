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

            private readonly IRepositorySubtitle _subtitles;
            private readonly IRepositorySubtitleRow _subtitleRows;
            private readonly IRepositoryParserWord _words;
            public UnitOfWorkParser(DictionaryContext context, IRepositorySubtitle subtitles, IRepositorySubtitleRow subtitleRows, IRepositoryParserWord words)
            {
                _context = context;
                _subtitles = subtitles;
                _subtitleRows = subtitleRows;
                _words = words;
            }
            public IRepositorySubtitle Subtitles { get => _subtitles; }
            public IRepositorySubtitleRow SubtitleRows { get => _subtitleRows; }
            public IRepositoryParserWord Words { get => _words; }

            public void Save()
            {
                _context.SaveChanges();
            }
           
        }
    }

}

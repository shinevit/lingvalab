using System;
using System.Collections.Generic;
using System.Text;

namespace Lingva.DataAccessLayer.Repositories
{
    public interface IUnitOfWorkParser : IUnitOfWork
    {
        IRepositoryFilm Films { get; }
        IRepositorySubtitle Subtitles { get; }
        IRepositorySubtitleRow SubtitleRows { get; }
        IRepositoryParserWord ParserWords { get; }
    }
}

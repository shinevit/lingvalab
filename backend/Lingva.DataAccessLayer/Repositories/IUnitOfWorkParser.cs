namespace Lingva.DataAccessLayer.Repositories
{
    public interface IUnitOfWorkParser : IUnitOfWork
    {
        IRepositorySubtitle Subtitles { get; }
        IRepositorySubtitleRow SubtitleRows { get; }
        IRepositoryParserWord ParserWords { get; }
    }
}
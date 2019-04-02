using System.Collections.Generic;
using System.IO;
using Lingva.BusinessLayer.DTO;
using Lingva.DataAccessLayer.Entities;

namespace Lingva.BusinessLayer.Contracts
{
    public interface ISubtitlesHandlerService
    {
        Subtitle GetSubtitleById(int id);

        Subtitle GetSubtitleByPath(string path);

        void AddSubtitle(Subtitle subtitle);

        IEnumerable<SubtitleRow> ParseSubtitle(Subtitle subtitle);
    }
}
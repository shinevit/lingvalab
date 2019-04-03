using Lingva.BusinessLayer.DTO;
using Lingva.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lingva.BusinessLayer.Contracts
{
    public interface ISubtitlesHandlerService
    {
        Subtitle GetSubtitleById(int id);

        Subtitle GetSubtitleByPath(string path);

        void AddSubtitle(Subtitle subtitle);

        IEnumerable<SubtitleRow> ParseSubtitle(Subtitle subtitle);

        Subtitle DeleteSubtitle(int id);

        void UpdateSubtitle(Subtitle word);
    }
}

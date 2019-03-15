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
        void AddSubtitles(SubtitlesRowDTO[] subDTO, string path, int? filmId);
        SubtitleRow[] Parse(Stream subtitles);
    }
}

using Lingva.BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lingva.BusinessLayer.Contracts
{
    public interface ISubtitlesHandlerService
    {
        void AddSubtitles(SubtitlesRowDTO[] subDTO, string subtitlesName, int filmId);
        SubtitlesRowDTO[] Parse(Stream subtitles);
    }
}

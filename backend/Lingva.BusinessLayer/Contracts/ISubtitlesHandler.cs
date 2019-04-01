using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Lingva.BusinessLayer.DTO;

namespace Lingva.BusinessLayer.Contracts
{
    public interface ISubtitlesHandler
    {
        void AddSubtitles(SubtitlesRowDTO[] subDTO, string subtitlesName, int filmId);
        SubtitlesRowDTO[] Parse(Stream subtitles);
    }
}

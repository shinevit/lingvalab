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
        void AddSubtitles(SubtitlesRowDTO[] subDTO, string path, int? filmId);//public void AddSubtitles(SubtitleRow[] subDTO, string path, int? filmId)  
        SubtitlesRowDTO[] Parse(Stream subtitles);//public SubtitlesRowDTO[] Parse(Stream subtitles)
    }
}

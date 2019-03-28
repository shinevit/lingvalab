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
        IEnumerable<SubtitleRow> ParseSubtitle(Subtitle subtitle);
    }
}

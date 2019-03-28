using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Lingva.BusinessLayer.Contracts;
using Lingva.BusinessLayer.DTO;
using Lingva.BusinessLayer.Extensions;
using Lingva.DataAccessLayer.Entities;
using Lingva.DataAccessLayer.Repositories;
using SubtitlesParser.Classes;

namespace Lingva.BusinessLayer.Services
{
    public class SubtitlesHandlerService : ISubtitlesHandlerService
    {
        private readonly IUnitOfWorkParser _unitOfWork;

        public SubtitlesHandlerService(IUnitOfWorkParser unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _path = string.Empty;
        }

        public IEnumerable<SubtitleRow> ParseSubtitle(Subtitle subtitle)
        {
            IEnumerable<SubtitleRow> rows;

            using (var sourceStream = File.OpenRead(subtitle.Path))
            {
                rows = ParseStream(sourceStream);
            }

            AddSubtitleWithRows(subtitle, rows);

            return rows;
        }
 
        private void AddSubtitleWithRows(Subtitle subtitle, IEnumerable<SubtitleRow> rows)  
        {
            if(subtitle == null)
            {
                throw new ArgumentNullException("Tried to operate with a null Subtitle object.");
            }

            if (rows == null)
            {
                throw new ArgumentNullException("Tried to operate with a null IEnumerable<SubtitleRow> object.");
            }

            _unitOfWork.Subtitles.Create(new Subtitle()
            {
                Path = subtitle.Path,
                FilmId = subtitle.FilmId,
                LanguageName = subtitle.LanguageName,
            });

            int? subtitleId = _unitOfWork.Subtitles.Get(subtitle.Path);

            if (subtitleId == null)
            {
                throw new ArgumentNullException("Returned null primary key value of the Subtitles table record.");
            }

            foreach (SubtitleRow row in rows)
            {
                row.SubtitleId = subtitleId;
                _unitOfWork.SubtitleRows.Create(row);
            }

            _unitOfWork.Save();
        }

        private IEnumerable<SubtitleRow> ParseStream(Stream subtitleStream)
        {
            var parser = new SubtitlesParser.Classes.Parsers.SrtParser();
            var encoding = DetectEncoding(subtitleStream);

            List<SubtitleItem> items = parser.ParseStream(subtitleStream, encoding);

            IEnumerable<SubtitleRow> rows = new List<SubtitleRow>();

            foreach (SubtitleItem item in items)
            {
                (rows as List<SubtitleRow>).Add(item.ToSubtitleRow());
            };

            return rows;
        }
               
        private Encoding DetectEncoding(Stream stream)
        {
            Ude.CharsetDetector cdet = new Ude.CharsetDetector();
            cdet.Feed(stream);
            cdet.DataEnd();

            if (cdet.Charset == null)
            {
                throw new FormatException("Encoding unrecognized.");
            }

            return Encoding.GetEncoding(cdet.Charset);
        }
    }
}

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
using NLog;
using SubtitlesParser.Classes;

namespace Lingva.BusinessLayer.Services
{
    public class SubtitlesHandlerService : ISubtitlesHandlerService
    {
        private readonly IUnitOfWorkParser _unitOfWork;

        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public SubtitlesHandlerService(IUnitOfWorkParser unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Subtitle GetSubtitleById(int id)
        {
            if (id <= 0)
            {
                _logger.Warn("SubtitlesHandlerService.GetSubtitle(int id): The value of the subtitle Id is incorrect.");
                _logger.Warn("ArgumentException is generated.");

                throw new ArgumentException("The value of the subtitle Id is incorrect.");
            }

            _logger.Info("Attempt to get \"{Id}\" record from the Subtitles table occured.");

            Subtitle subtitle = _unitOfWork.Subtitles.Get(id);

            if (subtitle == null)
            {
                _logger.Info($"There is no Subtitle record with Id = {id} in the Subtitles table.");

                return null;
            }

            _logger.Info($"Attempt to get a Subtitle record with Id = {id} from the Subtitles table is successful.");

            return subtitle;
        }

        public Subtitle GetSubtitleByPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                _logger.Debug("SubtitlesHandlerService.GetSubtitle(int id): The value of the Subtitle Path is incorrect.");
                _logger.Debug("ArgumentNullException is generated.");

                throw new ArgumentNullException("The value of the Subtitle Path is incorrect.");
            }

            _logger.Info($"Attempt to get the Subtitle record by the Path = \"{path}\" record from the Subtitles table occured.");

            Subtitle subtitle = _unitOfWork.Subtitles.Get(s => s.Path == path);

            if (subtitle == null)
            {
                _logger.Info($"There is no Subtitle record with Path = {path} in the Subtitles table.");

                _logger.Info($"Attempt to get a Subtitle record with Path = {path} from the Subtitles table is successful.");

                return subtitle;
            }

 
                return null;
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

        public void AddSubtitle(Subtitle subtitle)
        {
            if (subtitle == null)
            {
                throw new ArgumentNullException("Tried to operate with a null Subtitle object.");
            }

            _unitOfWork.Subtitles.Create(subtitle);

            _unitOfWork.Save();
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

            _unitOfWork.Subtitles.Create(subtitle);

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

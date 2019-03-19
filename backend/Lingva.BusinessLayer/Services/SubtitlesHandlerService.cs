﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Lingva.BusinessLayer.Contracts;
using Lingva.BusinessLayer.DTO;
using Lingva.BusinessLayer.SubtitlesParser.Classes;
using Lingva.DataAccessLayer.Entities;
using Lingva.DataAccessLayer.Repositories;

namespace Lingva.BusinessLayer.Services
{
    public class SubtitlesHandlerService : ISubtitlesHandlerService
    {
        private readonly IUnitOfWorkParser _unitOfWork;

        public SubtitlesHandlerService(IUnitOfWorkParser unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddSubtitles(SubtitlesRowDTO[] subDTO, string path, int? filmId)  
        {
            _unitOfWork.Subtitles.Create(new Subtitle()
            {
                Path = path,
                FilmId = filmId,
                LanguageName = "en"
            });

            int subId = _unitOfWork.Subtitles.Get(p => p.Path == path).Id;
            subDTO.ToList().ForEach(n => n.SubtitleId = subId);

            var subtitles = subDTO.Select(n => new SubtitleRow()
            {
                Value = n.Value,
                SubtitleId = (int)n.SubtitleId,
                StartTime = n.StartTime,
                EndTime = n.EndTime
            });

            foreach (var sub in subDTO)
            {
                _unitOfWork.SubtitleRows.Create((SubtitleRow)sub);
            }

            _unitOfWork.Save();
        }

        public SubtitlesRowDTO[] Parse(Stream subtitles)
        {
            var encoding = DetectEncoding(subtitles);

            var parser = new SubtitlesParser.Classes.Parsers.SrtParser();
            List<SubtitleItem> items = parser.ParseStream(subtitles, encoding);

            SubtitlesRowDTO[] subDTO = items.Select(n => new SubtitlesRowDTO()
            {
                Value = String.Join(" ", n.Lines),
                StartTime = new TimeSpan(0, 0, 0, 0, n.StartTime),
                EndTime = new TimeSpan(0, 0, 0, 0, n.EndTime),
            }).ToArray();

            return subDTO;
        }

        private Encoding DetectEncoding(Stream stream)
        {
            Ude.CharsetDetector cdet = new Ude.CharsetDetector();
            cdet.Feed(stream);
            cdet.DataEnd();
            if (cdet.Charset != null)
            {
                return Encoding.GetEncoding(cdet.Charset);
            }
            else
            {
                throw new FormatException("Encoding unrecognized");
            }
        }
    }
}

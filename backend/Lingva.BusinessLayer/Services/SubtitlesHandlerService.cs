using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Lingva.BusinessLayer.DTO;
using Lingva.BusinessLayer.Interfaces;
using Lingva.BusinessLayer.SubtitlesParser.Classes;
using Lingva.DataAccessLayer.Entities;
using Lingva.DataAccessLayer.Repositories;

namespace Lingva.BusinessLayer.Services
{
    public class SubtitlesHandlerService:ISubtitlesHandler
    {
        private readonly IGenericRepository<Subtitles> _subtitlesRepository;
        private readonly IGenericRepository<SubtitlesRow> _subtitlesRowRepository;

        public SubtitlesHandlerService(IGenericRepository<Subtitles> subtitlesRepository,
            IGenericRepository<SubtitlesRow> subtitlesRowRepository)
        {
            _subtitlesRepository = subtitlesRepository;
            _subtitlesRowRepository = subtitlesRowRepository;
        }

        public void AddSubtitles(SubtitlesRowDTO[] subDTO, string subtitlesName, int filmId)
        {
          
            _subtitlesRepository.Create(new Subtitles()
            {
                Name = subtitlesName,
                FilmId = filmId
            });

            int subId = _subtitlesRepository.Get().FirstOrDefault(p => p.Name == subtitlesName).Id;
            subDTO.ToList().ForEach(n => n.SubtitlesId = subId);

            _subtitlesRowRepository.CreateRange(subDTO.Select(n => new SubtitlesRow()
            {
                LineNumber = n.LineNumber,
                Value = n.Value,
                SubtitlesId = (int)n.SubtitlesId,
                StartTime = n.StartTime,
                EndTime = n.EndTime
            }));

        }

        public SubtitlesRowDTO[] Parse(Stream subtitles)
        {
            var encoding = DetectEncoding(subtitles);


            var parser = new SubtitlesParser.Classes.Parsers.SrtParser();
            List<SubtitleItem> items = parser.ParseStream(subtitles, encoding);

            SubtitlesRowDTO[] subDTO = items.Select((n, index) => new SubtitlesRowDTO()
            {
                LineNumber = index + 1,
                Value = String.Join(null, n.Lines),
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

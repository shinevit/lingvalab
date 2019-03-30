using System;
using System.Collections.Generic;
using System.Text;
using Lingva.DataAccessLayer.Entities;
using Lingva.DataAccessLayer.Repositories;

namespace Lingva.BusinessLayer.Services
{
    public class FilmService : IFilmService
    {
        private IUnitOfWorkFilm _unitOfWorkFilm;

        public FilmService(IUnitOfWorkFilm unitOfWorkFilm)
        {
            _unitOfWorkFilm = unitOfWorkFilm;
        }

        public Film GetFilmInfo (int FilmID)
        {
            Film film = _unitOfWorkFilm.Films.Get(FilmID);

            return film;
        }
    }

}

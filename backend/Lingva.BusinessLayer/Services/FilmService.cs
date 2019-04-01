using System;
using System.Collections.Generic;
using System.Text;
using Lingva.DataAccessLayer.Entities;
using Lingva.DataAccessLayer.Repositories;

namespace Lingva.BusinessLayer.Services
{
    public class FilmService : IFilmService
    {
        private readonly IUnitOfWorkFilm _unitOfWork;

        public FilmService(IUnitOfWorkFilm unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Film> GetFilmList()
        {
            return _unitOfWork.Films.GetList();
        }

        public Film GetFilm(int id)
        {
            Film film = _unitOfWork.Films.Get(id);
            return film;
        }

        public Film GetFilmByTitle(string title)
        {
            Film film = _unitOfWork.Films.Get(f => f.Title == title);
            return film;
        }

        public void AddFilm(Film film)
        {
            _unitOfWork.Films.Create(film);
            _unitOfWork.Save();
        }

        public void UpdateFilm(int id, Film film)
        {
            Film myFilm = _unitOfWork.Films.Get(id);            
            _unitOfWork.Films.Update(film);
            _unitOfWork.Save();
        }

        public void DeleteFilm(int id)
        {
            Film film = _unitOfWork.Films.Get(id);

            if (film == null)
            {
                return;
            }

            _unitOfWork.Films.Delete(film);
            _unitOfWork.Save();
        }
    }

}

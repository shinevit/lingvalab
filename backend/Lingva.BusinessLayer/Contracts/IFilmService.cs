using System;
using System.Collections.Generic;
using System.Text;

namespace Lingva.DataAccessLayer.Entities
{
    public interface IFilmService
    {
        IEnumerable<Film> GetFilmList();

        Film GetFilm(int id);

        Film GetFilmByTitle(string title);

        void AddFilm(Film film);

        void UpdateFilm(int id, Film film);

        void DeleteFilm(int id);
    }
}

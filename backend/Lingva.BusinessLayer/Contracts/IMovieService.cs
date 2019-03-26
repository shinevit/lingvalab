using Lingva.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lingva.BusinessLayer.Contracts
{
    public interface IMovieService
    {
        IEnumerable<Movie> GetMovieList();

        Movie GetMovie(int id);

        Movie GetMovieByTitle(string title);

        void AddMovie(Movie movie);

        void UpdateMovie(int id, Movie movie);

        void DeleteMovie(int id);
    }
}

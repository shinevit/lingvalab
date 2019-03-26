using System;
using System.Collections.Generic;
using System.Text;
using Lingva.BusinessLayer.Contracts;
using Lingva.DataAccessLayer.Entities;
using Lingva.DataAccessLayer.Repositories;

namespace Lingva.BusinessLayer.Services
{
    public class MovieCollectionService : IMovieService
    {
        private readonly IUnitOfWorkMovieCollection _unitOfWork;

        public MovieCollectionService(IUnitOfWorkMovieCollection unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Movie> GetMovieList()
        {
            return _unitOfWork.Movies.GetList();
        }

        public Movie GetMovie(int id)
        {
            Movie movie = _unitOfWork.Movies.Get(id);
            return movie;
        }

        public Movie GetMovieByTitle(string title)
        {
            Movie movie = _unitOfWork.Movies.Get(m => m.Title == title);
            return movie;
        }

        public void AddMovie(Movie movie)
        {   
            _unitOfWork.Movies.Create(movie);
            _unitOfWork.Save();
        }

        public void UpdateMovie(int id, Movie movie)
        {
            Movie myMovie = _unitOfWork.Movies.Get(id);
            //myEvent.Translation = myEventUpdate.Translation;//??
            _unitOfWork.Movies.Update(movie);
            _unitOfWork.Save();
        }

        public void DeleteMovie(int id)
        {
            Movie movie = _unitOfWork.Movies.Get(id);

            if (movie == null)
            {
                return;
            }

            _unitOfWork.Movies.Delete(movie);
            _unitOfWork.Save();
        }
    }
}

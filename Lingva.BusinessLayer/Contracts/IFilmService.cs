using System;
using System.Collections.Generic;
using System.Text;

namespace Lingva.DataAccessLayer.Entities
{
    public interface IFilmService
    {
        Film GetFilmInfo(int FilmID);
    }
}

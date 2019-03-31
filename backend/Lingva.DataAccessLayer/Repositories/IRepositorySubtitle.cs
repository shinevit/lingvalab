﻿using Lingva.DataAccessLayer.Entities;

namespace Lingva.DataAccessLayer.Repositories
{
    public interface IRepositorySubtitle : IRepository<Subtitles>
    {
        int? Get(string path);
    }
}
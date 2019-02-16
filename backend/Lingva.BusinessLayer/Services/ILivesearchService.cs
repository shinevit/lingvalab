using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Lingva.BusinessLayer.Services
{
    public interface ILivesearchService
    {
        IEnumerable Find(string substring);
    }
}

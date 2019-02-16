using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Lingva.BusinessLayer.Services
{
    public class LivesearchService : ILivesearchService
    {
        public IEnumerable Find(string substring)
        {
            return new int[] { 2, 5, 6 };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.WebAPI.Initializer
{
    public interface IDbInitializable
    {
        void InitializeParserWords();
        void DeinitializeParserWords();
    }
}

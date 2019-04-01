using System.Collections;

namespace Lingva.BusinessLayer.Contracts
{
    public interface ILivesearchService
    {
        IEnumerable Find(string substring, int quantity);
    }
}
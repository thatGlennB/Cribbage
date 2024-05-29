using Cribbage.DataTransferObject;
using Cribbage.Model;

namespace Cribbage.Interfaces
{
    public interface IDealValueController
    {
        IEnumerable<ResultDTO> Get(ISet<Card> deal);
    }
}

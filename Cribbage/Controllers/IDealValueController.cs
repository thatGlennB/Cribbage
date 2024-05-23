using Cribbage.Controller.DataTransferObject;
using Cribbage.Model;

namespace Cribbage.Controller
{
    public interface IDealValueController
    {
        IEnumerable<ResultDTO> Get(ISet<Card> deal);
    }
}

using Cribbage.Controller.DataTransferObject;
using Cribbage.Model;

namespace Cribbage.Controller
{
    public interface IDealValueController
    {
        ISet<ResultDTO> Get(ISet<Card> deal);
    }
}

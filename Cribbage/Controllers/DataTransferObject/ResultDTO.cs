using Cribbage.Model;

namespace Cribbage.Controller.DataTransferObject
{
    public record ResultDTO(int HandValue, int DiscardValue, double ExpectedDrawValue, ISet<Card> Hand, ISet<Card> Discard);
}

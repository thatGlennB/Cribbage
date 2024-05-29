using Cribbage.Interfaces;
using Cribbage.Model;

namespace Cribbage.DataTransferObject
{
    public record ResultDTO(int HandValue = 0, int DiscardValue = 0, double ExpectedDrawValue = 0, ISet<Card> Hand = null!, ISet<Card> Discard = null!);
}

using Cribbage.Interfaces;
using Cribbage.Model;

namespace Cribbage.DataTransferObject
{
    public record ResultDTO(int HandValue = 0, int DiscardValue = 0, double ExpectedDrawValue = 0, ISet<ICard> Hand = null!, ISet<ICard> Discard = null!);
}

using Cribbage.Model;
using Cribbage.Model.Enums;

namespace Cribbage.Interfaces
{
    public interface ICard : IComparable<ICard>
    {
        Rank Rank { get; }
        Suit Suit { get; }
    }
}

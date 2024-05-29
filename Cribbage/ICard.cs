using Cribbage.Model.Enums;

namespace Cribbage
{
    internal interface ICard 
    {
        Rank Rank { get; }
        Suit Suit { get; }
    }
}

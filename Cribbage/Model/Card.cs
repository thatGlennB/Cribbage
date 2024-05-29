using Cribbage.Model.Enums;

namespace Cribbage.Model
{
    public record Card(Rank Rank, Suit Suit) : ICard
    {
        public override string ToString()
        {
            return $"{Rank.Name} {Suit}";
        }
    } 
}
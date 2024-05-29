using Cribbage.Model.Enums;

namespace Cribbage.Model
{
    public record Card(Rank Rank, Suit Suit)
    {
        public override string ToString()
        {
            return $"{Rank.Name} {Suit}";
        }
    } 
}
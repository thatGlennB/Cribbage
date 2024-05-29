using Cribbage.Interfaces;
using Cribbage.Model.Enums;

namespace Cribbage.Model
{
    public record Card(Rank Rank, Suit Suit) : IComparable<Card>
    {
        public int CompareTo(Card? other)
        {
            if (other == null) return 1;
            int rankComparison = Rank.CompareTo(other.Rank);
            if (rankComparison == 0)
            {
                return Suit.CompareTo(other.Suit);
            }
            return rankComparison;
        }

        public override string ToString() => $"{Rank.Name()} {Suit}";
    } 
}
using Cribbage.Model.Enums;

namespace Cribbage.Model
{
    public class Card : IComparable<Card>
    {
        public Card(Rank rank, Suit suit)
        {
            Rank = rank;
            Suit = suit;
        }
        public Rank Rank { get; set; }
        public Suit Suit { get; set; }
        public int Value { get => Rank > 9 ? 10 : Rank; }

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

        public static int operator -(Card lhs, Card rhs)
        {
            return lhs.Rank.Value - rhs.Rank.Value;
        }
    }
}
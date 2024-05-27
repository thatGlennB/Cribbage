using Cribbage.Model.Enums;

namespace Cribbage.Model.Utilities
{
    internal static class CardUtil
    {
        internal static int DrawCount(int excludedCards)
        {
            return SuitUtil.Count * Rank.Count() - excludedCards;
        }
        internal static bool IsSequential(this Card card, Card target)
        {
            return Math.Abs(card.Rank - target.Rank) <= 1;
        }
        internal static int CountInRank(this Card[] cards, int rank)
        {
            return cards.Count(o => o.Rank == rank);
        }
        internal static int CountInSuit(this Card[] cards, Suit suit)
        {
            return cards.Count(o => o.Suit == suit);
        }
        internal static Card[] Sort(this Card[] cards)
        {
            Card[] output = new Card[cards.Length];
            for (int i = 0; i < cards.Length; i++)
            {
                Card shunt = cards[i];
                for (int j = 0; i < output.Length; j++)
                {
                    if (output[j] == null)
                    {
                        output[j] = shunt;
                    }
                    // if shunt card is less than current occupant of output[j], then swap cards
                    else if (shunt.Rank.CompareTo(output[j].Rank) < 0)
                    {
                        (output[j], shunt) = (shunt, output[j]);
                    }
                }
            }
            return output;
        }
        internal static Card GetCard(int value, Suit suit)
        {
            // get rank corresponding to value, if one exists
            Rank? rank = Rank.GetRank(value);
            return rank != null ? new Card(rank, suit) : throw new ArgumentOutOfRangeException(nameof(value),$"variable {nameof(value)} must correspond to a valid card rank value");
        }
        public static int Value(this Card card) => card.Rank > 9 ? 10 : card.Rank;

        internal static int Compare(this Card card, Card? other)
        {
            if (other == null) return 1;
            int rankComparison = card.Rank.CompareTo(other.Rank);
            if (rankComparison == 0)
            {
                return card.Suit.CompareTo(other.Suit);
            }
            return rankComparison;
        }
        internal static string Shorthand(this Card card) 
        {
            return string.Concat(card.Rank.Name, card.Suit.ToString().AsSpan(0, 1));
        }
    }
}

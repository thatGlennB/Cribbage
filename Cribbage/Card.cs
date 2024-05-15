namespace Cribbage
{
    public class Card
    {
        public Card(CardRank rank, Suit suit)
        {
            this.Rank = rank;
            this.Suit = suit;
        }
        public CardRank Rank { get; set; }
        public Suit Suit { get; set; }
        public static int operator +(Card lhs, Card rhs)
        {
            return lhs.Rank.Pips + rhs.Rank.Pips;
        }
        public static int operator +(Card lhs, int rhs)
        {
            return lhs.Rank.Pips + rhs;
        }
    }
    public static class CardUtil
    {
        public static bool IsSequential(this Card card, Card target)
        {
            return Math.Abs(card.Rank.Pips - target.Rank.Pips) == 1;
        }
        public static bool IsPair(this Card card, Card target)
        {
            return card.Rank.Pips == target.Rank.Pips;
        }
    }
}

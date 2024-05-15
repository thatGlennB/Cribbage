namespace Cribbage
{
    public class Card
    {
        public Card(Rank rank, Suit suit)
        {
            this.Rank = rank;
            this.Suit = suit;
        }
        public Rank Rank { get; set; }
        public Suit Suit { get; set; }
        public static int operator +(Card lhs, Card rhs)
        {
            return lhs.Rank.Pips + rhs.Rank.Pips;
        }
        public static int operator +(Card lhs, int rhs)
        {
            return lhs.Rank.Pips + rhs;
        }
        public static int operator -(Card lhs, Card rhs)
        {
            return lhs.Rank.Pips - rhs.Rank.Pips;
        }
        public static int operator -(Card lhs, int rhs)
        {
            return lhs.Rank.Pips - rhs;
        }
    }
    public static class CardUtil
    {
        public static bool IsSequential(this Card card, Card target)
        {
            return Math.Abs(card.Rank.Value - target.Rank.Value) <= 1;
        }
        public static bool IsPair(this Card card, Card target)
        {
            return card.Rank.Value == target.Rank.Value;
        }
        public static int CountInRank(this Card[] cards, int rank) 
        {
            return cards.Count(o => o.Rank.Value == rank);
        }
        public static int CountInSuit(this Card[] cards, Suit suit) 
        {
            return cards.Count(o => o.Suit == suit);
        }
        public static Card[] Sort(this Card[] cards) 
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
                        Card hold = shunt;
                        shunt = output[j];
                        output[j] = hold;
                    }
                }
            }
            return output;
        }
    }
}

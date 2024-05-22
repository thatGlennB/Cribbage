using Cribbage.Model.Enums;

namespace Cribbage.Model.Utilities
{
    public static class CardUtil
    {
        public static int DrawCount(int excludedCards)
        {
            return SuitUtil.Count * Rank.Count() - excludedCards;
        }
        public static bool IsSequential(this Card card, Card target)
        {
            return Math.Abs(card.Rank - target.Rank) <= 1;
        }
        public static int CountInRank(this Card[] cards, int rank)
        {
            return cards.Count(o => o.Rank == rank);
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

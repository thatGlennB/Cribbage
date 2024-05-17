﻿namespace Cribbage
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
        public static int operator -(Card lhs, Card rhs)
        {
            return lhs.Rank.Value - rhs.Rank.Value;
        }
    }
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
        public static int Pips(this Card card) => card.Rank > 9 ? 10 : card.Rank;
    }
}

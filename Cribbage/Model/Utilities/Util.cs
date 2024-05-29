﻿using Cribbage.Model.Enums;

namespace Cribbage.Model.Utilities
{
    internal static class Util
    {
        internal static IEnumerable<int[]> Combinations(int n, int r)
            {
                int[] output = new int[r];
                for (int i = 0; i < output.Length; i++)
                {
                    output[i] = i;
                }
                do
                {
                    yield return output;
                    for (int i = 1; i <= output.Length; i++)
                    {
                        int index = output.Length - i;
                        int nextValue = output[index] + 1;
                        if (nextValue < n)
                        {
                            output[index] = nextValue;
                            if (i > 1)
                            {
                                for (int j = 1; j < output.Length - index; j++)
                                {
                                    output[index + j] = output[index] + j;
                                }
                            }
                            break;
                        }
                    }
                } while (output[0] <= n - r);
            }
        internal static int DrawCount(int excludedCards)
        {
            return Suits.Count * Rank.Count() - excludedCards;
        }
        internal static bool IsSequential(this Card card, Card target)
        {
            return Math.Abs(card.Rank - target.Rank) <= 1;
        }
        internal static int CountInRank(this IEnumerable<Card> cards, int rank)
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
        internal static HashSet<Card> Copy(ISet<Card> originals)
        {
            HashSet<Card> output = [];
            foreach (Card original in originals)
            {
                output.Add(new Card(original.Rank, original.Suit));
            }
            return output;
        }
    }
}

using System.ComponentModel.Design;
using System.Runtime.CompilerServices;

namespace Cribbage
{
    public static class Calculations
    {
        public static int GetFifteens(this Card[] cards)
        {
            // sort array
            // start from largest card
            // skip through combinations that are more than 15
            throw new NotImplementedException();
        }
        public static int GetPairs(this Card[] cards)
        {
            int output = 0;
            for (int i = 0; i < cards.Length; i++) {
                for (int j = i + 1; j < cards.Length; j++)
                {
                    if (cards[i].Rank.Value == cards[j].Rank.Value)
                    {
                        output += 2;
                    }
                }
            }
            return output;
        }
        public static int GetRuns(this Card[] cards)
        {
            int output = 0;
            // get trimmed cards
            // check for doubles and triples
            // if no doubles, value is just array length
            // else, uhhhh... a bit more complicated
            throw new NotImplementedException();
        }
        public static int GetHat(this Card[] cards)
        {
            for (int i = 0; i < 4; i++) 
            {
                if (cards[i].Rank == CardValue.JACK && cards[i].Suit == cards[5].Suit) 
                {
                    return 1;
                }
            }
            return 0;
        }
        public static int GetFlush(this Card[] cards)
        {
            for (int i = 1; i < 4; i++)
            {
                if (cards[i].Suit == cards[0].Suit)
                {
                    return 0;
                }
            }
            return 1;
        }
        private static Card[] Sort(this Card[] cards) 
        {
            Card[] orderedCards = new Card[cards.Length];
            for (int i = 0; i < cards.Length; i++)
            {
                for (int j = 0; j < orderedCards.Length; j++)
                {
                    if (orderedCards[j] == null)
                    {
                        orderedCards[0] = cards[i];
                    }
                    else if (cards[i].Rank.Rank < orderedCards[j].Rank.Rank)
                    {
                        orderedCards[j + 1] = orderedCards[j];
                        orderedCards[j] = cards[i];
                    }
                }

            }
            return orderedCards;
        }
        private static Card[] Trim(this Card[] cards) 
        {
            Card[] orderedCards = cards.Sort();
            List<Card> trimmedCards = new List<Card>();
            int currentSequence = 0;
            int sequenceStart = -1;
            for (int i = 1; i < orderedCards.Length; i++)
            {
                if (orderedCards[i].Rank.Rank == orderedCards[i - 1].Rank.Rank + 1)
                {
                    if (sequenceStart < 0) 
                    {
                        sequenceStart = i - 1;
                    }
                    currentSequence++;
                }
                else if (orderedCards[i].Rank.Rank != orderedCards[i - 1].Rank.Rank || 
                    i == orderedCards.Length - 1) 
                {
                    if (currentSequence >= 2) 
                    {
                        for (int j = sequenceStart; j < i; j++) 
                        {
                            trimmedCards.Add(trimmedCards[j]);
                        }
                    }
                    currentSequence = 0;
                }
            }
            return trimmedCards.ToArray();
        }
    }
}

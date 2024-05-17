using Cribbage.FifteenCount;

namespace Cribbage
{
    public class Combination
    {
        public readonly Card[] Hand;
        public readonly Card[] Discard;
        public Combination(Card[] hand, Card[] discard)
        {
            Hand = hand.Sort();
            Discard = discard.Sort();           
        }
        
        /// <summary>
        /// returns true if all cards in hand are the same suit, i.e. a flush
        /// </summary>
        public bool Flush 
        {
            get => Flush;
            init => Flush = Hand.CountInSuit(Hand[0].Suit) == Hand.Length;
        }

        /// <summary>
        /// Contains an array of two integers, specifying the upper and lower bounds of the sequence. No information on replicates is contained here.
        /// </summary>
        public int[] Runs 
        {
            get => Runs;
            init
            {
                Runs = [0, 0];
                Card? sequenceStart = null;
                for (int i = 1; i < Hand.Length; i++)
                {
                    Card sequenceEnd = Hand[i];
                    if (sequenceEnd.IsSequential(Hand[i - 1]))
                    {
                        sequenceStart = Hand[i - 1];
                    }
                    if (!sequenceEnd.IsSequential(Hand[i - 1]) || i == Hand.Length -1 )
                    {
                        if (sequenceStart != null)
                        {
                            Runs[0] = sequenceStart.Rank.Value;
                            Runs[1] = sequenceEnd.Rank.Value;
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Contains a dictionary, specifying the suits where a hat point is possible.
        /// Dictionary keys are the suits of each jack in the hand. Dictionary values are the number of cards (excluding jacks) of each suit in the hand.
        /// </summary>
        public Dictionary<Suit, int> Hats 
        {
            get => Hats;
            init
            {
                Hats = new();
                for (int i = 0; i < Hand.Length; i++)
                {
                    if (Hand[i].Rank == Rank.JACK)
                    {
                        Hats.Add(Hand[i].Suit, 0);
                    }
                }
                for (int i = 0; i < Hand.Length; i++)
                {
                    Suit thisSuit = Hand[i].Suit;
                    if (Hand[i].Rank != Rank.JACK && Hats.Keys.Contains(thisSuit))
                    {
                        Hats[thisSuit]++;
                    }
                }
            }
        }
    }
}

namespace Cribbage
{
    public class Combination
    {
        /// <summary>
        /// There are 46 possible draw cards
        /// </summary>
        private const int NUM_DRAW_CARDS = 46;
        private readonly Card[] Hand;
        private readonly Card[] Discard;
        public Combination(Card[] hand, Card[] discard)
        {
            Hand = hand.Sort();
            Discard = discard.Sort();           
        }
        public int ValueInHand
        {
            get
            {
                int output = 0;
                // flush is worth 4 points
                if (Flush) output += 4;

                // if N cards are of same rank, add N!
                foreach (int rank in Hand.Select(o => o.Rank.Value).Distinct())
                {
                    int countInRank = Hand.CountInRank(rank);
                    int product = 1;
                    for (int i = 2; i <= countInRank; i++) 
                    {
                        product *= i;
                    }
                    output += product;
                }

                // runs?

                // 15s?

                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// The expected value added by the draw card.
        /// 
        /// The expected value is the sum of the value added by each possible draw, divided by the number of possible draws.
        /// </summary>
        public double ExpectedDrawValue
        {
            get
            {
                int output = 0;
                int numSuits = Enum.GetNames(typeof(Suit)).Length;

                // if flush, add one for every possible draw card in suit
                if (Flush)
                {
                    output += CardRank.Count - Hand.Count() - Discard.CountInSuit(Hand[0].Suit); ;
                }
                
                // if any jacks present, add one for every possible draw card in suit
                foreach (Suit suit in Hats.Keys)
                {
                    output += CardRank.Count - Hand.CountInSuit(suit) - Discard.CountInSuit(suit);
                }
                
                // if possible to draw pair, trip or four-of-a-kind, add relevant number of points (2, 4, 6)
                foreach (int rank in Hand.Select(o => o.Rank.Value).Distinct())
                {
                    int countInRank = Hand.CountInRank(rank);
                    output += (numSuits - Discard.CountInRank(rank) - countInRank) * 2 * countInRank;
                }
                
                // runs? runs? how runs?
                // dear lord, 15s
                throw new NotImplementedException();
            }
        }

        private bool Flush 
        {
            get => this.Flush;
            init => this.Flush = this.Hand.CountInSuit(this.Hand[0].Suit) == this.Hand.Length;
        }
        private List<int[]> Runs 
        {
            get => this.Runs;
            init
            {
                this.Runs = new();
                Card? sequenceStart = null;
                for (int i = 1; i < this.Hand.Length; i++)
                {
                    Card sequenceEnd = this.Hand[i];
                    if (sequenceEnd.IsSequential(this.Hand[i - 1]))
                    {
                        sequenceStart = this.Hand[i - 1];
                    }
                    else
                    {
                        if (sequenceStart != null && sequenceEnd - sequenceStart > 2)
                        {
                            this.Runs.Add(new int[] {
                            sequenceStart.Rank.Value,
                            sequenceEnd.Rank.Value
                        });
                        }
                    }
                }
            }
        }
        private Dictionary<Suit, int> Hats 
        {
            get => this.Hats;
            init
            {
                this.Hats = new();
                for (int i = 0; i < this.Hand.Length; i++)
                {
                    if (this.Hand[i].Rank == CardRank.JACK)
                    {
                        this.Hats.Add(this.Hand[i].Suit, 0);
                    }
                }
                for (int i = 0; i < this.Hand.Length; i++)
                {
                    Suit thisSuit = this.Hand[i].Suit;
                    if (this.Hand[i].Rank != CardRank.JACK && this.Hats.Keys.Contains(thisSuit))
                    {
                        this.Hats[thisSuit]++;
                    }
                }
            }
        }

        // TODO: calculate fifteens with tree structure - use composite pattern
    }
}

using System.Net.Sockets;

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
            getReplicates();
            getFlush();
            getHat();
            GetRuns();
        }
        public int ValueInHand => AggregateValue(0);
        /// <summary>
        /// The expected value added by the draw card.
        /// 
        /// The expected value is the sum of the value added by each possible draw, divided by the number of possible draws.
        /// </summary>
        public double ExpectedDrawValue => (double)AggregateValue(1) / NUM_DRAW_CARDS;
        private void getReplicates()
        {
            int replicates = 0;
            for(int i = 1; i < this.Hand.Length; i++) 
            {
                if (this.Hand[i].IsPair(this.Hand[i - 1]))
                {
                    replicates += 1;
                }
                else 
                {
                    switch (replicates) 
                    {
                        case 1:
                            this.Pairs.Add(this.Hand[i - 1].Rank.Value);
                            break;
                        case 2:
                            this.Trips.Add(this.Hand[i - 1].Rank.Value);
                            break;
                        case 3:
                            this.FourOfAKind = this.Hand[i - 1].Rank.Value;
                            break;
                        default: break;
                    }
                    replicates = 0;
                }
            }
        }
        private void getFlush() 
        {
            this.Flush = true;
            for (int i = 1; i < this.Hand.Length; i++) 
            {
                this.Flush = this.Hand[i].Suit == this.Hand[0].Suit && this.Flush;
                if (!this.Flush) break;
            }
        }
        private void getHat() 
        {
            for (int i = 0; i < this.Hand.Length; i++) 
            {
                if (this.Hand[i].Rank == CardRank.JACK) 
                {
                    this.Hat.Add(this.Hand[i].Suit, 0);
                }
            }
            for (int i = 0; i < this.Hand.Length; i++)
            {
                Suit thisSuit = this.Hand[i].Suit;
                if (this.Hand[i].Rank != CardRank.JACK && this.Hat.Keys.Contains(thisSuit))
                {
                    this.Hat[thisSuit]++;
                }
            }
        }
        private void GetRuns() 
        {
            Card? sequenceStart = null;
            for(int i = 1; i < this.Hand.Length; i++) 
            {
                Card sequenceEnd = this.Hand[i];
                if (sequenceEnd.IsSequential( this.Hand[i - 1]))
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
        private List<int> Pairs { get; set; } = new();
        private List<int> Trips { get; set; } = new();
        private int FourOfAKind { get; set; }
        private bool Flush { get; set; }
        private List<int[]> Runs { get; set; }
        private Dictionary<Suit, int> Hat { get; set; } = new();




        // TODO: replace AggregateValue two separate methods and put them in respective getters.
        private int AggregateValue(int index) 
        {
            throw new NotImplementedException();
        }



        // TODO: calculate fifteens with tree structure - use composite pattern
    }
}

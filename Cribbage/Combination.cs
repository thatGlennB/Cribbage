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
            Hand = SortCards(hand);
            Discard = SortCards(discard);
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
                if (this.Hand[i].Rank.Rank == this.Hand[i - 1].Rank.Rank)
                {
                    replicates += 1;
                }
                else 
                {
                    switch (replicates) 
                    {
                        case 1:
                            this.Pairs.Add(this.Hand[i - 1].Rank.Rank);
                            break;
                        case 2:
                            this.Trips.Add(this.Hand[i - 1].Rank.Rank);
                            break;
                        case 3:
                            this.FourOfAKind = this.Hand[i - 1].Rank.Rank;
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
                if (this.Hand[i].Rank == CardValue.JACK) 
                {
                    this.Hat.Add(this.Hand[i].Suit, 0);
                }
            }
            for (int i = 0; i < this.Hand.Length; i++)
            {
                Suit curSuit = this.Hand[i].Suit;
                if (this.Hand[i].Rank != CardValue.JACK && this.Hat.Keys.Contains(curSuit))
                {
                    this.Hat[curSuit]++;
                }
            }
        }
        private void GetRuns() 
        {
            int sequenceStart = 0;
            for(int i = 1; i < this.Hand.Length; i++) 
            {
                int sequenceEnd = this.Hand[i].Rank.Rank;
                if (sequenceEnd == this.Hand[i - 1].Rank.Rank + 1)
                {
                    sequenceStart = this.Hand[i - 1].Rank.Rank;
                }
                else 
                {
                    if (sequenceEnd - sequenceStart > 2) 
                    {
                        this.Runs.Add(new int[] { sequenceStart, sequenceEnd });
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


        

        private Card[] SortCards(Card[] cards) 
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


        // TODO: calculate fifteens with tree structure - use composite pattern
    }
}

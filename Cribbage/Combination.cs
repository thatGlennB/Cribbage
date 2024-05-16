using Cribbage.FifteenCount;

namespace Cribbage
{
    public class Combination
    {
        private int _drawCards { get => SuitUtil.Count * Rank.Count - _hand.Length - _discard.Length; }
        private readonly Card[] _hand;
        private readonly Card[] _discard;
        public Combination(Card[] hand, Card[] discard)
        {
            _hand = hand.Sort();
            _discard = discard.Sort();           
        }
        public int ValueInHand
        {
            get
            {
                int output = 0;
                // flush is worth 4 points
                if (_flush) output += 4;

                // if N cards are of same rank, add N!
                foreach (int rank in _hand.Select(o => o.Rank.Value).Distinct())
                {
                    int countInRank = _hand.CountInRank(rank);
                    int product = 1;
                    for (int i = 2; i <= countInRank; i++) 
                    {
                        product *= i;
                    }
                    output += product;
                }

                // runs
                if (_runs[0] != 0) 
                {
                    int span = _runs[1] - _runs[0] + 1;
                    for (int i = _runs[0]; i <= _runs[1]; i++) 
                    {
                        span *= _hand.CountInRank(i);
                    }
                    output += span;
                }

                // 15s?
                RootNode counter = new RootNode(_hand.Select(o => o.Rank.Pips).ToList());
                counter.Generate();
                if (counter.HasEndpoint()) 
                {
                    output += 2 * counter.GetCombinations().Count();
                }


                return output;
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

                // if flush, add one for every possible draw card in suit
                if (_flush)
                {
                    output += Rank.Count - _hand.Count() - _discard.CountInSuit(_hand[0].Suit); ;
                }
                
                // if any jacks present, add one for every possible draw card in suit
                foreach (Suit suit in _hats.Keys)
                {
                    output += Rank.Count - _hand.CountInSuit(suit) - _discard.CountInSuit(suit);
                }
                
                // if possible to draw pair, trip or four-of-a-kind, add relevant number of points (2, 4, 6)
                foreach (int rank in _hand.Select(o => o.Rank.Value).Distinct())
                {
                    int countInRank = _hand.CountInRank(rank);
                    output += (SuitUtil.Count - _discard.CountInRank(rank) - countInRank) * 2 * countInRank;
                }

                // runs? runs? how runs?
                // dear lord, 15s


                // divide possible points by number of possible draws to get expected value
                double dummyReturn = (double)output / _drawCards;
                throw new NotImplementedException();
            }
        }

        private bool _flush 
        {
            get => _flush;
            init => _flush = _hand.CountInSuit(_hand[0].Suit) == _hand.Length;
        }

        /// <summary>
        /// Contains an array of two integers, specifying the upper and lower bounds of the sequence. No information on replicates is contained here.
        /// </summary>
        private int[] _runs 
        {
            get => _runs;
            init
            {
                _runs = [0, 0];
                Card? sequenceStart = null;
                for (int i = 1; i < _hand.Length; i++)
                {
                    Card sequenceEnd = _hand[i];
                    if (sequenceEnd.IsSequential(_hand[i - 1]))
                    {
                        sequenceStart = _hand[i - 1];
                    }
                    if (!sequenceEnd.IsSequential(_hand[i - 1]) || i == _hand.Length -1 )
                    {
                        if (sequenceStart != null && sequenceEnd - sequenceStart > 2)
                        {
                            _runs[0] = sequenceStart.Rank.Value;
                            _runs[1] = sequenceEnd.Rank.Value;
                        }
                    }
                }
            }
        }
        private Dictionary<Suit, int> _hats 
        {
            get => _hats;
            init
            {
                _hats = new();
                for (int i = 0; i < _hand.Length; i++)
                {
                    if (_hand[i].Rank == Rank.JACK)
                    {
                        _hats.Add(_hand[i].Suit, 0);
                    }
                }
                for (int i = 0; i < _hand.Length; i++)
                {
                    Suit thisSuit = _hand[i].Suit;
                    if (_hand[i].Rank != Rank.JACK && _hats.Keys.Contains(thisSuit))
                    {
                        _hats[thisSuit]++;
                    }
                }
            }
        }

        // TODO: calculate fifteens with tree structure - use composite pattern
    }
}

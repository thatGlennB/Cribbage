using Cribbage.Model;
using Cribbage.Model.Enums;
using Cribbage.Model.Utilities;

namespace Cribbage
{
    public class Selection
    {
        // TODO: separate into model and controller elements
        public Selection(ISet<Card> hand, ISet<Card> discard)
        {
            // pass arguments to instance properties
            Hand = hand;
            Discard = discard;

            // create combination object
            _combinations = new ValidCombinations();

            // create combination tree
            _root = new RootNode(hand, _combinations);

            // non-tree-algorithm point calculations
            HandValue = _getPoints();

            // calculate expected points for a given draw value using tree algorithms
            for (int i = 1; i <= Rank.Count(); i++)
            {
                Card? card = _getCard(i);
                if (card == null) continue;
                _root.Add(card);

                _drawValues.Add(i, _getPoints() - HandValue);

                // remove drawcard from each combination tree
                _root.Remove(card);
            }
            

            // Expected draw value == total added points for all possible draws / number of possible draws
            ExpectedValue = (double)(_drawValues.Values.Sum() + _getHatPoints()) / CardUtil.DrawCount(Hand.Count() + Discard.Count());
        }

        #region Private Properties
        private readonly RootNode _root;
        private readonly ValidCombinations _combinations;
        public readonly IDictionary<int, int> _drawValues = new Dictionary<int, int>();
        #endregion

        #region Public Properties
        public readonly ISet<Card> Hand;
        public readonly ISet<Card> Discard;
        public int HandValue { get; private set; }
        public int DiscardValue
        {
            get
            {
                int output = Discard.Sum(o => o.Value) == 15 ? 2 : 0;
                for (int i = 1; i <= Rank.Count(); i++)
                {
                    if (Discard.Count(o => o.Rank == i) == 2) output += 2;
                }
                return output;
            }
        }
        public double ExpectedValue { get; private set; } = 0; 
        #endregion
        public int DrawValue(int rank) => _drawValues[rank];

        #region Private Methods
        private int _getPoints() 
        {
            int output = 0;
            foreach (Combination combo in _combinations.Set)
            {
                switch (combo.Mode)
                {
                    case Mode.FIFTEENS:
                        output += 2;
                        break;
                    case Mode.RUNS:
                        output += combo.Cards.Count();
                        break;
                    case Mode.FLUSH:
                        output += combo.Cards.Count();
                        break;
                    case Mode.PAIRS:
                        output += combo.Cards.Count();
                        break;
                    default: break;
                }
            }
            return output;
        }
        private Card? _getCard(int value)
        {
            // get rank corresponding to value, if one exists
            Rank? rank = Rank.getRank(value);
            if (rank == null) throw new ArgumentOutOfRangeException();

            // loop through suits
            for (int i = 0; i < SuitUtil.Count; i++)
            {
                // return card if this rank and suit is available (ie not in hand or discard),
                if (!Hand.Any(o => o.Rank == value && (int)o.Suit == i) &&
                    !Discard.Any(o => o.Rank == value && (int)o.Suit == i))
                {
                    return new Card(rank, (Suit)i);
                }
            }

            // no cards available of this rank; return null
            return null;
        }
        private int _getHatPoints()
        {
            int output = 0;
            // for each jack in hand, return one point per possible draw in jack's suit
            foreach (Suit suit in Hand
                .Where(o => o.Rank == Rank.JACK)
                .Select(o => o.Suit))
            {
                output += _possibleDrawsInSuit(suit);
            }
            return output;
        }
        private int _possibleDrawsInSuit(Suit suit)
        {
            return Rank.Count() - Hand.Count(o => o.Suit == suit) - Discard.Count(o => o.Suit == suit);
        }
        #endregion
    }
}
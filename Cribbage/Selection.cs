namespace Cribbage
{
    public class Selection
    {
        public Selection(ISet<Card> hand, ISet<Card> discard)
        {
            // pass arguments to instance properties
            Hand = hand;
            Discard = discard;

            // set up cards singleton
            _cardsSingleton = CardsSingleton.Instance;
            _cardsSingleton.Clear();
            _cardsSingleton.Add(hand);

            // create roots of point combination calculators
            foreach (Mode mode in Enum.GetValues(typeof(Mode)))
            {
                _roots.Add(mode, new Root(mode, _cardsSingleton));
            }

            // non-tree-algorithm point calculations
            _comboPoints = _getComboPoints();
            _flushPoints = _getFlushPoints();

            // calculate expected points for a given draw value using tree algorithms
            for (int i = 1; i <= Rank.Count(); i++)
            {
                _drawValues.Add(i, _getExpectedPoints(i));
            }

            // add expected points from non-tree-algorithms
            int output = 0;
            output += _drawValues.Values.Sum();
            output += _getHatPoints();
            output += _getFlushPoints(true) - _flushPoints;

            // Expected draw value == total added points for all possible draws / number of possible draws
            ExpectedValue = (double)output / CardUtil.DrawCount(Hand.Count() + Discard.Count());
        }
        
        #region Private Properties
        private readonly IDictionary<Mode, Root> _roots = new Dictionary<Mode, Root>();
        private int _comboPoints { get; init; }
        private int _flushPoints { get; init; }
        private readonly CardsSingleton _cardsSingleton;
        private readonly IDictionary<int, int> _drawValues = new Dictionary<int, int>(); 
        #endregion

        #region Public Properties
        public readonly ISet<Card> Hand;
        public readonly ISet<Card> Discard;
        public int HandValue => _flushPoints + _comboPoints;
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

        #region Private Methods
        private int _getExpectedPoints(int rank)
        {
            // get an available card in this rank, if one exists.
            int possibleDraws = SuitUtil.Count - Hand.Count(o => o.Rank == rank) - Discard.Count(o => o.Rank == rank);
            if (possibleDraws < 1) return 0;
            Card? card = _getCard(rank);
            if (card == null) return 0;

            // add drawcard to each combination tree
            foreach (Mode mode in Enum.GetValues(typeof(Mode)))
                _roots[mode].Add(card);

            // points added by draw is calculated:
            // points added = total points (with draw) - total points (without draw)
            int output = possibleDraws * (_getComboPoints() - _comboPoints);

            // remove drawcard from each combination tree
            foreach (Mode mode in Enum.GetValues(typeof(Mode)))
                _roots[mode].Remove(card);

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
        private int _getComboPoints()
        {
            int output = 0;
            output += 2 * _roots[Mode.FIFTEENS].ValidCombinations.Count();
            output += 2 * _roots[Mode.PAIRS].ValidCombinations.Count();
            foreach (ISet<Card> run in _roots[Mode.RUNS].ValidCombinations)
            {
                output += run.Count;
            }
            return output;
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
        private int _getFlushPoints(bool draw = false)
        {
            // if the hand is not flush, return 0
            if (!Hand.All(o => o.Suit == Hand.First().Suit)) return 0;

            // if there is no draw card, flush is worth one point per card in hand
            if (!draw) return Hand.Count();

            // else, flush is worth one point per card in hand, plus one for draw
            // return flush value * number of possible draws in suit
            return (Hand.Count() + 1) * _possibleDrawsInSuit(Hand.First().Suit);
        }
        private int _possibleDrawsInSuit(Suit suit)
        {
            return Rank.Count() - Hand.Count(o => o.Suit == suit) - Discard.Count(o => o.Suit == suit);
        } 
        #endregion
    }
}
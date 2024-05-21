namespace Cribbage.FifteenCount
{
    public class Selection
    {
        private readonly Root _fifteenRoot;
        private readonly Root _runRoot;
        private readonly Root _pairRoot;
        private int _comboPoints { get; set; }
        private readonly Register _register;
        private readonly CardsSingleton _cardsObservable;
        private readonly IDictionary<int, int> _drawValues = new Dictionary<int,int>();
        public readonly ISet<Card> Hand;
        public readonly ISet<Card> Discard;
        public int HandValue { get; private set; } = 0;
        public int DiscardValue { get; private set; } = 0;
        public double ExpectedValue { get; private set; } = 0;
        public Selection(ISet<Card> hand, ISet<Card> discard) 
        {
            // TODO cardsobservable should be a singleton
            _cardsObservable = new CardsSingleton();
            _cardsObservable.Add(hand);
            _fifteenRoot = new Root(Mode.FIFTEENS, _cardsObservable);
            _runRoot = new Root(Mode.RUNS, _cardsObservable);
            _pairRoot = new Root(Mode.PAIRS, _cardsObservable);
            _register = new Register();
            Hand = hand;
            Discard = discard;

            _comboPoints = _getComboPoints();
            HandValue += _getFlushPoints() + _comboPoints;
            DiscardValue = (discard.Sum(o => o.Value) == 15 ? 2 : 0);

            for (int i = 1; i <= Rank.Count(); i++)
            {
                if (discard.Count(o => o.Rank == i) == 2) DiscardValue += 2;
                _drawValues.Add(i, _getExpectedPoints(i));
                
            }
            int output = 0;
            output += _drawValues.Values.Sum();
            output += _getHatPoints();
            output += _getFlushPoints(true);
            ExpectedValue = (double)output / CardUtil.DrawCount(Hand.Count() + Discard.Count());
        }
        private int _getExpectedPoints(int rank) 
        {
            Card? card = _getCard(rank);
            if (card == null) return 0;
            _fifteenRoot.Add(card);
            _runRoot.Add(card);
            _pairRoot.Add(card);

            int possibleDraws = SuitUtil.Count - Hand.Count(o => o.Rank == rank) - Discard.Count(o => o.Rank == rank);
            int dummy = _getComboPoints();
            int output = possibleDraws * (dummy - _comboPoints);

            _fifteenRoot.Remove(card);
            _runRoot.Remove(card);
            _pairRoot.Remove(card);

            return output;
        }
        private Card? _getCard(int value) 
        {
            Rank? rank = Rank.getRank(value);
            if (rank != null)
            {
                for (int i = 0; i < SuitUtil.Count; i++)
                {
                    if (!Hand.Any(o => o.Rank == value && (int)o.Suit == i) &&
                        !Discard.Any(o => o.Rank == value && (int)o.Suit == i)) 
                    {
                        return new Card(rank, (Suit)i);
                    }
                }
            }
            return null;
        }
        private int _getComboPoints() 
        {
            int output = 2 * _fifteenRoot.Set.Count();
            output += 2 * _pairRoot.Set.Count();
            foreach (ISet<Card> run in _runRoot.Set) 
            {
                output += run.Count;
            }
            return output;
        }
        private int _getHatPoints() 
        {
            int output = 0;
            foreach (Suit suit in Hand
                .Where(o => o.Rank == Rank.JACK)
                .Select(o => o.Suit)) 
            {
                output += 9 - Discard.Count(o => o.Suit == suit);
            }
            return output;
        }
        private int _getFlushPoints(bool draw = false) 
        {
            if (!Hand.All(o => o.Suit == Hand.ElementAt(0).Suit)) return 0;
            if (!draw) return 4;
            return 9 - Discard.Count(o => o.Suit == Hand.ElementAt(0).Suit);
        }
    }
}

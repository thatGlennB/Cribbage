namespace Cribbage.FifteenCount
{
    public class Node : IObserver<ISet<Card>>, IDisposable
    {
        private readonly Combinations _output;
        private readonly CardsObservable _cardsObservable;
        private readonly IDisposable _unsubscriber;
        internal readonly ISet<Card> _combination;
        private ISet<Node> _children { get; set; }
        internal ISet<Card> _cards { get; set; }
        internal readonly Mode _mode;

        public Card Card { get => _combination.Last(); }

        public Node(ISet<Card> combination, CardsObservable cardsObservable, Combinations combinations, Mode mode)
        {
            _mode = mode;
            _combination = combination;
            _children = new HashSet<Node>();
            _cards = new HashSet<Card>();
            _cardsObservable = cardsObservable;
            _output = combinations;
            _unsubscriber = _cardsObservable.Subscribe(this);
        }

        private void _updateChildren()
        {
            foreach (Node child in _children)
            {
                if (!_cards.Contains(child.Card))
                {
                    child.Dispose();
                    _children.Remove(child);
                }
            }
            foreach (Card card in _cards)
            {
                if (!_children.Select(o => o.Card).Contains(card) &&
                    Card.CompareTo(card) > 0 &&
                    Card != card)
                {
                    int sum = _combination.Select(o => o.Value).Sum() + card.Value;
                    HashSet<Card> newCombination = _combination.Append(card).ToHashSet();
                    if (this.SuccessCondition(card))
                    {
                        _output.Add(newCombination);
                    }
                    else if (this.ExtensionCondition(card))
                    {
                        _children.Add(new Node(newCombination, _cardsObservable, _output, _mode));
                    }
                }
            }
        }
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(ISet<Card> value)
        {
            _cards = value;
            _updateChildren();
        }

        public void Dispose()
        {
            foreach (Node child in _children)
            {
                child.Dispose();
                _children.Remove(child);
            }
            _unsubscriber.Dispose();
            _output.Remove(_combination);
        }
    }
}

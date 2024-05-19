namespace Cribbage.FifteenCount
{
    public class Node : IObserver<List<Card>>, IDisposable
    {
        private readonly List<Card> _combination;
        private readonly Combinations _output;
        private List<Node> _children { get; set; }
        private List<Card> _cards { get; set; }
        public Card Card { get => _combination.Last(); }
        public Node(List<Card> combination, Combinations output, List<Card> cards) 
        {
            _combination = combination;
            _children = new();
            _cards = cards;
            _output = output;
            _updateChildren();
        }

        // TODO: observer and combinations should be singletons
        // create or destroy children depending on cards observable
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
            foreach(Card card in _cards)
            {
                if (!_children.Select(o => o.Card).Contains(card) &&
                    Card.CompareTo(card) > 0 &&
                    Card != card &&
                    _combination.Select(o => o.Value).Sum() + card.Value < 15) 
                {
                    _children.Add(new Node(_combination.Append(card).ToList(), _output, _cards));
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

        public void OnNext(List<Card> value)
        {
            _cards = value;
            _updateChildren() ;
        }

        public void Dispose()
        {
            foreach (Node child in _children) 
            {
                child.Dispose();
                _children.Remove(child);
            }
            _output.Remove(_combination);
        }
    }
}

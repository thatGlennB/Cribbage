using System.ComponentModel.Design;

namespace Cribbage.FifteenCount
{
    public class Node : IObserver<List<Card>>, IDisposable
    {
        private readonly List<Card> _combination;
        private readonly Combinations _output = Combinations.Instance;
        private readonly CardsObservable _cardsObservable;
        private List<Node> _children { get; set; }
        private List<Card> _cards { get; set; }
        private readonly IDisposable _unsubscriber;
        public Card Card { get => _combination.Last(); }
        public Node(List<Card> combination) 
        {
            _combination = combination;
            _children = new();
            _cards = new List<Card>();
            _cardsObservable = CardsObservable.Instance;
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
            foreach(Card card in _cards)
            {
                if (!_children.Select(o => o.Card).Contains(card) &&
                    Card.CompareTo(card) > 0 &&
                    Card != card) 
                {
                    int sum = _combination.Select(o => o.Value).Sum() + card.Value;
                    List<Card> newCombination = _combination.Append(card).ToList();
                    if (sum < 15) 
                    {
                        _children.Add(new Node(newCombination));
                    }
                    else if (sum == 15) 
                    {
                        _output.Add(newCombination);
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
            _unsubscriber.Dispose();
            _output.Remove(_combination);
        }
    }
}

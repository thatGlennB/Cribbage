namespace Cribbage
{
    public class Node : IDisposable
    {
        private readonly Combinations _output;
        internal readonly ISet<Card> _combination;
        private ISet<Node> _children { get; set; }
        internal ISet<Card> _cards { get; set; } = new HashSet<Card>();
        internal readonly Mode _mode;

        public Card Card { get => _combination.Last(); }

        public Node(ISet<Card> cards, ISet<Card> combination, Combinations combinations, Mode mode)
        {
            _mode = mode;
            _combination = combination;
            _children = new HashSet<Node>();
            _output = combinations;
            Regenerate(cards);
        }

        public void Regenerate(ISet<Card> cards)
        {
            _cards = cards;
            foreach (Node child in _children)
            {
                if (!cards.Contains(child.Card))
                {
                    child.Dispose();
                    _children.Remove(child);
                }
                else
                {
                    child.Regenerate(cards);
                }
            }
            foreach (Card card in cards)
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
                        _children.Add(new Node(cards, newCombination, _output, _mode));
                    }
                }
            }
        }

        public void Dispose()
        {
            foreach (Card card in _cards)
            {
                HashSet<Card> newCombination = _combination.Append(card).ToHashSet();
                if (this.SuccessCondition(card))
                {
                    _output.Remove(newCombination);
                }
            }
            foreach (Node child in _children)
            {
                child.Dispose();
                _children.Remove(child);
            }
            _output.Remove(_combination);
        }
    }
}

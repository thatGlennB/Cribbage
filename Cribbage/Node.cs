namespace Cribbage
{
    public class Node : IDisposable
    {
        private readonly Combinations _output;
        internal readonly ISet<Card> _combination;
        private ISet<Node> _children { get; set; }
        internal readonly Mode _mode;
        internal ISet<Card> _cards { get; set; } = new HashSet<Card>();

        public Card Card { get => _combination.Last(); }

        public Node(ISet<Card> cards, ISet<Card> combination, Combinations combinations, Mode mode)
        {
            _mode = mode;
            _combination = combination;
            _children = new HashSet<Node>();
            _output = combinations;
            Regenerate(cards);
        }

        // If available card set changes, root node calls Regenerate for each child node.
        // process propagates to successive children
        public void Regenerate(ISet<Card> cards)
        {
            // update private cards set
            _cards = cards;

            // for each child, check if associated card is still available
            // if card is not available, dispose of child
            // else, invoke 'Regenerate' method of child
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
            
            // for each card, check if there is an associated child.
            // if no associated child, check if child should be created
            foreach (Card card in cards)
            {
                // if the card value is less than the current card (to avoid duplicate combinations)
                // and if the card is not the current node's card...
                if (!_children.Select(o => o.Card).Contains(card) &&
                    Card.CompareTo(card) > 0 &&
                    Card != card)
                {
                    // create a new combination set - containing the cards for every node in this branch
                    HashSet<Card> newCombination = _combination.Append(card).ToHashSet();

                    // if this combination is a valid point combination, trigger success condition
                    if (this.SuccessCondition(card))
                    {
                        _output.Add(newCombination);
                    }

                    // if this combination is a fragment of a potentially valid point combination, trigger extension condition
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
                
                // if this combination is a valid set, then make sure that it is removed from the combinations register when the node is disposed
                if (this.SuccessCondition(card))
                {
                    _output.Remove(newCombination);
                }
            }

            // propagate Dispose method to children.
            foreach (Node child in _children)
            {
                child.Dispose();
                _children.Remove(child);
            }

            // TODO: is this necessary?
            _output.Remove(_combination);
        }
    }
}

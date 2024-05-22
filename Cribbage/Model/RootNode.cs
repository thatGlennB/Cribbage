namespace Cribbage.Model
{
    public class RootNode
    {
        #region Private Properties
        private ISet<Card> _cards;
        #endregion

        #region Public Properties
        public readonly ISet<Node> Nodes = new HashSet<Node>();
        public ValidCombinations Combinations;
        #endregion

        public RootNode(ISet<Card> cards, ValidCombinations combinations)
        {
            _cards = cards;
            Combinations = combinations;
            // create nodes for each available card
            foreach (Card card in _cards)
            {
                Nodes.Add(new Node(_cards, new HashSet<Card> { card }, Combinations));
            }
        }

        #region Public Properties
        public void Add(Card card)
        {
            if (_cards.Any(o => o.CompareTo(card) == 0)) return;
            _cards.Add(card);
            Nodes.Add(new Node(_cards, new HashSet<Card> { card }, Combinations));
            foreach (Node node in Nodes)
            {
                node.GenerateChildren(_cards);
            }
        }
        public void Remove(Card card)
        {
            _cards.Remove(card);
            Node? node = Nodes.SingleOrDefault(o => o.Card == card);
            if (node != null)
            {
                node.Dispose();
                Nodes.Remove(node);
            }
        }
        #endregion
    }
}

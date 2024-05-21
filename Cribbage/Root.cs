namespace Cribbage
{
    public class Root
    {
        private ISet<Node> _nodes;
        private Combinations _combinations;
        private CardsSingleton _cardsObservable;
        public readonly Mode Mode;
        public ISet<Card> Cards => _cardsObservable.Cards;
        public ISet<ISet<Card>> Set => _combinations.Set;
        public Root(Mode mode, CardsSingleton observer)
        {
            _combinations = new Combinations();
            _cardsObservable = observer;
            _nodes = new HashSet<Node>();
            Mode = mode;

            foreach (Card card in Cards)
            {
                _nodes.Add(new Node(Cards, new HashSet<Card> { card }, _combinations, Mode));
            }
        }
        public void Add(Card card)
        {
            _nodes.Add(new Node(Cards, new HashSet<Card> { card }, _combinations, Mode));
            _cardsObservable.Add(card);
            foreach (Node node in _nodes)
            {
                node.Regenerate(Cards);
            }
        }
        public void Remove(Card card)
        {
            _cardsObservable.Remove(card);
            Node? node = _nodes.SingleOrDefault(o => o.Card == card);
            if (node != null)
            {
                node.Dispose();
                _nodes.Remove(node);
            }
        }
    }
}

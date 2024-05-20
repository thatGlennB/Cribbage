namespace Cribbage.FifteenCount
{
    public class Root
    {
        private ISet<Node> _nodes;
        private Combinations _combinations;
        private CardsObservable _cardsObservable;
        public ISet<Card> Cards => _cardsObservable.Cards;
        public Root(ISet<Card> cards) 
        {
            _combinations = new Combinations();
            _cardsObservable = new CardsObservable();
            _cardsObservable.Add(cards);
            _nodes = new HashSet<Node>();
            foreach(Card card in cards) 
            {
                _nodes.Add(new Node(new HashSet<Card> { card }, _cardsObservable, _combinations));
            }
        }
        public void Add(Card card)
        {
            _cardsObservable.Add(card);
            _nodes.Add(new Node(new HashSet<Card> { card }, _cardsObservable, _combinations));
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
        public ISet<ISet<Card>> Set => _combinations.Set;
    }
}

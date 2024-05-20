namespace Cribbage.FifteenCount
{
    public class Root
    {
        private ISet<Node> _nodes;
        private Combinations _combinations;
        private CardsObservable _cardsObservable;
        public readonly Mode Mode;
        public ISet<Card> Cards => _cardsObservable.Cards;
        public Root(ISet<Card> cards, Mode mode) 
        {
            _combinations = new Combinations();
            _cardsObservable = new CardsObservable();
            _cardsObservable.Add(cards);
            _nodes = new HashSet<Node>();
            Mode = mode;
            foreach(Card card in cards) 
            {
                _nodes.Add(new Node(new HashSet<Card> { card }, _cardsObservable, _combinations, Mode));
            }
        }
        public void Add(Card card)
        {
            _cardsObservable.Add(card);
            _nodes.Add(new Node(new HashSet<Card> { card }, _cardsObservable, _combinations, Mode));
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

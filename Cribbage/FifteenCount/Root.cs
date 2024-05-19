namespace Cribbage.FifteenCount
{
    public class Root
    {
        private List<Node> _nodes;
        private Combinations _combinations;
        private CardsObservable _cardsObservable;
        public List<Card> Cards => _cardsObservable.Cards;
        public Root(List<Card> cards) 
        {
            _combinations = Combinations.Instance;
            _cardsObservable = CardsObservable.Instance;
            _cardsObservable.Add(cards);
            _nodes = new();
            foreach(Card card in cards) 
            {
                _nodes.Add(new Node([card]));
            }
        }
        public void Add(Card card)
        {
            _cardsObservable.Add(card);
            _nodes.Add(new Node([card]));
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
        public List<List<Card>> Set => _combinations.Set;
    }
}

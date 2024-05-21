namespace Cribbage
{
    public class Root
    {
        #region Private Properties
        private readonly ISet<Node> _nodes = new HashSet<Node>();
        private readonly Combinations _combinations = new();
        private readonly CardsSingleton _cardsObservable = CardsSingleton.Instance;
        #endregion

        #region Public Properties
        public readonly Mode Mode;
        // TODO: is this necessary?
        public ISet<Card> Cards => _cardsObservable.Cards;
        public ISet<ISet<Card>> ValidCombinations => _combinations.Set; 
        #endregion
        
        public Root(Mode mode)
        {
            // pass argument to instance property
            Mode = mode;

            // create nodes for each available card
            foreach (Card card in Cards)
            {
                _nodes.Add(new Node(Cards, new HashSet<Card> { card }, _combinations, Mode));
            }
        }

        #region Public Properties
        // TODO: consider incorporating observer pattern here
        public void Add(Card card)
        {
            // TODO: singleton method should not be here. This class should observe the singleton
            _cardsObservable.Add(card);
            _nodes.Add(new Node(Cards, new HashSet<Card> { card }, _combinations, Mode));
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
        #endregion
    }
}

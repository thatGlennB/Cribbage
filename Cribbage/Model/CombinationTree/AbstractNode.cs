using Cribbage.Model.Utilities;

namespace Cribbage.Model.CombinationTree
{
    public abstract class AbstractNode
    {
        // todo: Cards should be a singleton observable
        protected readonly Combinations _combinations;
        public ISet<Node> ChildNodes { get; protected set; }
        public virtual ISet<Card> Cards { get; set; }
        protected AbstractNode(Combinations combinations)
        {
            _combinations = combinations;
            ChildNodes = new HashSet<Node>();
            Cards = new HashSet<Card>();
        }
        public virtual void Regenerate()
        {
            _createAddedCardNodes();
            _destroyRemovedCardNodes();
            foreach (Node node in ChildNodes)
            {
                node.Regenerate();
            }
        }
        virtual protected void _createAddedCardNodes()
        {
            IEnumerable<Card> cardsToAdd = Cards.Where(card =>
                !ChildNodes.Select(node => node.Card).Contains(card));
            foreach (Card card in cardsToAdd)
            {
                _newNode(card);
            }
        }
        virtual protected void _destroyRemovedCardNodes()
        {
            IEnumerable<Node> nodesToRemove = ChildNodes.Where(o => !Cards.Contains(o.Card));
            foreach (Node node in nodesToRemove)
            {
                _combinations.Nodes.Remove(node);
                ChildNodes.Remove(node);
            }
        }
        virtual protected void _newNode(Card card)
        {
            Node newNode = new Node(new HashSet<Card> { card }, _combinations);
            _combinations.Nodes.Add(newNode);
            ChildNodes.Add(newNode);
        }
    }
}

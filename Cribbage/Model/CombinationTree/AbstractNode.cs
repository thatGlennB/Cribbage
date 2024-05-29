using Cribbage.Model.Utilities;

namespace Cribbage.Model.CombinationTree
{
    internal abstract class AbstractNode
    {
        internal ISet<Node> ChildNodes { get; set; }
        internal bool IsDeleted { get; set; } = false;
        protected AbstractNode()
        {
            ChildNodes = new HashSet<Node>();
        }
        internal virtual void Regenerate()
        {
            CreateAddedCardNodes();
            RemoveDeletedChildren();
            foreach(Node node in ChildNodes) 
            {
                node.Regenerate();
            }
        }
        abstract protected void CreateAddedCardNodes();
        virtual protected void RemoveDeletedChildren() 
        {
            foreach(Node node in ChildNodes) 
            {
                if (node.IsDeleted)
                {
                    ChildNodes.Remove(node);
                }
                else 
                {
                    node.RemoveDeletedChildren();
                }
            }
        }
        virtual protected void AddNode(Card card, RootNode root, ISet<Card>? cards = null)
        {
            cards ??= new HashSet<Card>();
            if (!ChildNodes.Any(o => o.Card.Rank == card.Rank && o.Card.Suit == card.Suit))
            {
                Node newNode = new(cards.Append(card).ToHashSet(), root);
                root.AllNodes.Add(newNode);
                ChildNodes.Add(newNode);
            }
        }
    }
}

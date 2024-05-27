namespace Cribbage.Model.CombinationTree
{
    internal abstract class AbstractNode
    {
        internal ISet<Node> ChildNodes { get; set; }
        protected AbstractNode()
        {
            ChildNodes = new HashSet<Node>();
        }
        internal virtual void Regenerate()
        {
            CreateAddedCardNodes();
            DestroyRemovedCardNodes();
            foreach (Node node in ChildNodes)
            {
                node.Regenerate();
            }
        }
        abstract protected void CreateAddedCardNodes();
        abstract protected void DestroyRemovedCardNodes();
        virtual protected void AddNode(ISet<Card> cards, RootNode root)
        {
            Node newNode = new(cards, root);
            root.AllNodes.Add(newNode);
            ChildNodes.Add(newNode);
        }
        virtual protected void RemoveNode(Node node, RootNode root)
        {
            ChildNodes.Remove(node);
            root.AllNodes.Remove(node);
        }
}
}

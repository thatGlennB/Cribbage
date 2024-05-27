using Cribbage.Model.Utilities;

namespace Cribbage.Model.CombinationTree
{
    internal class Node : AbstractNode
    {
        internal readonly ISet<Card> Combination;
        internal readonly RootNode Root;

        internal Card Card { get => Combination.Last(); }

        internal Node(ISet<Card> combination, RootNode root) : base()
        {
            Root = root;
            Combination = combination;
            Regenerate();
        }

        protected override void CreateAddedCardNodes()
        {
            foreach (Card card in Root.Cards)
            {
                if (Card.Compare(card) > 0) 
                {
                    AddNode(Combination.Append(card).ToHashSet(), Root);
                }
            }
        }

        protected override void DestroyRemovedCardNodes()
        {
            foreach (Node node in ChildNodes)
            {
                if (!Root.Cards.Contains(node.Card))
                {
                    ChildNodes.Remove(node);
                    Root.AllNodes.Remove(node);
                }
            }
        }
    }
}

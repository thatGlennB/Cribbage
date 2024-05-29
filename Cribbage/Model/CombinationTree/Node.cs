using Cribbage.Interfaces;
using Cribbage.Model.Utilities;

namespace Cribbage.Model.CombinationTree
{
    internal class Node : AbstractNode
    {
        internal readonly ISet<ICard> Combination;
        internal readonly RootNode Root;

        internal readonly ICard Card;

        internal Node(ISet<ICard> combination, RootNode root) : base()
        {
            Root = root;
            Combination = combination;
            Card = combination.Last();
            Regenerate();
        }

        protected override void CreateAddedCardNodes()
        {
            foreach (ICard card in Root.Cards.HandAndDraw)
            {
                if (Card.CompareTo(card) > 0) 
                {
                    AddNode(card, Root, Combination);
                }
            }
        }
    }
}

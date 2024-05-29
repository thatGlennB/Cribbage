using Cribbage.Model.Utilities;

namespace Cribbage.Model.CombinationTree
{
    internal class Node : AbstractNode
    {
        internal readonly ISet<Card> Combination;
        internal readonly RootNode Root;

        internal readonly Card Card;

        internal Node(ISet<Card> combination, RootNode root) : base()
        {
            Root = root;
            Combination = Util.Copy(combination);
            Card = combination.Last();
            Regenerate();
        }

        protected override void CreateAddedCardNodes()
        {
            foreach (Card card in Root.Cards.HandAndDraw)
            {
                if (Card.Compare(card) > 0) 
                {
                    AddNode(card, Root, Combination);
                }
            }
        }
    }
}

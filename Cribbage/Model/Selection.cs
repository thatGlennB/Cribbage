using Cribbage.Model.CombinationTree;

namespace Cribbage.Model
{
    public class Selection
    {
        public ISet<Node> Nodes;

        public Selection(ISet<Card> hand, ISet<Card> discard)
        {
            // pass arguments to instance properties
            Hand = hand;
            Discard = discard;

            // create combination tree
            Combinations = new Combinations();
            Root = new RootNode(Combinations);
        }

        public readonly RootNode Root;
        public readonly Combinations Combinations;
        public readonly ISet<Card> Hand;
        public readonly ISet<Card> Discard;

    }
}
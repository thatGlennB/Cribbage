using Cribbage.Model.CombinationTree;

namespace Cribbage.Model
{
    internal class Selection
    {

        internal Selection(ISet<Card> hand, ISet<Card> discard)
        {
            // pass arguments to instance properties
            Hand = hand;
            Discard = discard;

            // create combination tree
            Combinations = new Combinations();
            Root = new RootNode(Combinations);
        }

        internal readonly RootNode Root;
        internal readonly Combinations Combinations;
        internal readonly ISet<Card> Hand;
        internal readonly ISet<Card> Discard;

    }
}
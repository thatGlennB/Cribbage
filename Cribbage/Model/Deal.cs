using Cribbage.Interfaces;
using Cribbage.Model.CombinationTree;
using Cribbage.Model.Utilities;

namespace Cribbage.Model
{
    internal class Deal
    {
        internal ISet<RootNode> Roots { get; private set; } = new HashSet<RootNode>();
        internal Deal(ISet<Card> cards)
        {
            HashSet<Card> hand = [];
            if (cards.Count == 5 || cards.Count == 6)
            {
                IEnumerable<IEnumerable<Card>> discardCombinations = cards.GetCombinations(cards.Count - 4);
                foreach (IEnumerable<Card> discard in discardCombinations)
                {
                    hand = cards.Where(o => !discard.Contains(o)).ToHashSet();
                    Roots.Add(new RootNode(new Cards(hand, discard.ToHashSet())));
                }
            }
            else throw new ArgumentOutOfRangeException(nameof(cards), $"A deal object cannot be made for a set of {cards.Count} cards");
        }
    }
}

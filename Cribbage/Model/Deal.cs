using Cribbage.Model.CombinationTree;
using Cribbage.Model.Utilities;

namespace Cribbage.Model
{
    internal class Deal
    {
        internal ISet<RootNode> Roots { get; private set; } = new HashSet<RootNode>();
        internal Deal(ISet<Card> cards)
        {
            HashSet<Card> discard = [];
            HashSet<Card> hand = [];
            if (cards.Count == 5 || cards.Count == 6)
            {
                //HashSet<Card> cardCopy = new();
                //cardCopy = cards.CreateCopy();
                IEnumerable<int[]> combos = Util.Combinations(cards.Count, cards.Count - 4);
                foreach (int[] combo in combos)
                {
                    discard.Clear();
                    foreach (int index in combo)
                    {
                        discard.Add(cards.ElementAt(index));
                    }
                    hand = cards.Where(o => !discard.Contains(o)).ToHashSet();
                    Roots.Add(new RootNode(new Cards(hand, discard)));
                }
            }
            else throw new ArgumentOutOfRangeException(nameof(cards), $"A deal object cannot be made for a set of {cards.Count} cards");
        }
    }
}

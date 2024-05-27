using Cribbage.Model.CombinationTree;

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
                foreach (int[] combo in combinations(cards.Count, cards.Count - 4))
                {
                    discard.Clear();
                    foreach (int index in combo)
                    {
                        discard.Add(cards.ElementAt(index));
                    }
                    hand = cards.Where(o => !discard.Contains(o)).ToHashSet();
                    Roots.Add(new RootNode(hand, discard));
                }
            }
            else throw new ArgumentOutOfRangeException(nameof(cards), $"A deal object cannot be made for a set of {cards.Count} cards");
        }
        internal static IEnumerable<int[]> combinations(int n, int r)
        {
            int[] output = new int[r];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = i;
            }
            do
            {
                yield return output;
                for (int i = 1; i <= output.Length; i++)
                {
                    int index = output.Length - i;
                    int nextValue = output[index] + 1;
                    if (nextValue < n)
                    {
                        output[index] = nextValue;
                        if (i > 1)
                        {
                            for (int j = 1; j < output.Length - index; j++)
                            {
                                output[index + j] = output[index] + j;
                            }
                        }
                        break;
                    }
                }
            } while (output[0] <= n - r);
        }
    }
}

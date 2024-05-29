using Cribbage.Model.Enums;

namespace Cribbage.Model.Utilities
{
    internal static class Util
    {
        internal static IEnumerable<int[]> GetCombinationIndices(int n, int r)
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
        internal static IEnumerable<IEnumerable<T>> GetCombinations<T>(this IEnumerable<T> superset, int count)
        {
            foreach (int[] index in GetCombinationIndices(superset.Count(), count)) 
                yield return GetSelection<T>(superset, index);
        }
        internal static IEnumerable<T> GetSelection<T>(this IEnumerable<T> superset, params int[] indices) 
        {
            foreach (int index in indices) 
                yield return superset.ElementAt(index);
        }
        internal static int PossibleDrawCount(int excludedCards) => typeof(Suit).Count() * typeof(Rank).Max() - excludedCards;
    }
}

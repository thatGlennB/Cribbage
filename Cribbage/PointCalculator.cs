using Cribbage.FifteenCount;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cribbage
{
    public class PointCalculator
    {
        public int GetHandValue(Combination combination) 
        {
            int output = 0;
            // flush is worth 4 points
            if (combination.Flush) output += 4;

            // if N cards are of same rank, add N!
            foreach (int rank in combination.Hand.Select(o => o.Rank.Value).Distinct())
            {
                int countInRank = combination.Hand.CountInRank(rank);
                int product = 1;
                for (int i = 2; i <= countInRank; i++)
                {
                    product *= i;
                }
                output += product;
            }

            // runs
            if (combination.Runs[0] != 0)
            {
                int span = combination.Runs[1] - combination.Runs[0] + 1;
                for (int i = combination.Runs[0]; i <= combination.Runs[1]; i++)
                {
                    span *= combination.Hand.CountInRank(i);
                }
                output += span;
            }

            // 15s?
            RootNode counter = new RootNode(combination.Hand.Select(o => o.Rank.Pips).ToList());
            counter.Generate();
            if (counter.HasEndpoint())
            {
                output += 2 * counter.GetCombinations().Count();
            }


            return output;
        }
        public int GetExpectedDrawValue(Combination combination) 
        {
            int output = 0;

            // if flush, add one for every possible draw card in suit
            if (combination.Flush)
            {
                output += Rank.Count - combination.Hand.Count() - combination.Discard.CountInSuit(combination.Hand[0].Suit); ;
            }

            // if any jacks present, add one for every possible draw card in suit
            foreach (Suit suit in combination.Hats.Keys)
            {
                output += Rank.Count - combination.Hand.CountInSuit(suit) - combination.Discard.CountInSuit(suit);
            }

            // if possible to draw pair, trip or four-of-a-kind, add relevant number of points (2, 4, 6)
            foreach (int rank in combination.Hand.Select(o => o.Rank.Value).Distinct())
            {
                int countInRank = combination.Hand.CountInRank(rank);
                output += (SuitUtil.Count - combination.Discard.CountInRank(rank) - countInRank) * 2 * countInRank;
            }

            // runs? runs? how runs?
            // dear lord, 15s


            // divide possible points by number of possible draws to get expected value
            double dummyReturn = (double)output / CardUtil.DrawCount(combination.Hand.Length + combination.Discard.Length);
            throw new NotImplementedException();
        }
    }
}

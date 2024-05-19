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
            output += getRuns(combination);

            // 15s?
            output += getFifteens(combination);


            return output;
        }

        // TODO: make getFifteens and getRuns static
        // TODO: convert draw to an enum value, once I replace Rank class with enum
        private int getFifteens(Combination input, int draw = 0) 
        {
            int output = 0;
            List<int> pips = input.Hand.Select(o => o.Value).ToList();
            pips.Add(draw > 9 ? 10 : draw);
            Node counter = new Node(pips);
            counter.Regenerate();
            if (counter.HasEndpoint())
                output += 2 * counter.GetCombinations().Count();
            return output;
        }

        private int getRuns(Combination input, int draw = 0) 
        {
            int output = 0;
            if (input.Runs[0] != 0)
            {
                int min = draw > 0 && draw < input.Runs[0] ? draw : input.Runs[0];
                int max = draw > input.Runs[1] ? draw : input.Runs[1];
                int span = max - min + 1;
                if (span > 2)
                {
                    for (int i = min; i <= max; i++)
                    {
                        span *= (input.Hand.CountInRank(i) + draw == i ? 1 : 0);
                    }
                    output += span;
                }
            }
            return output;
        }
        public double GetExpectedDrawValue(Combination combination) 
        {
            int output = 0;

            // if flush, add one for every possible draw card in suit
            if (combination.Flush)
            {
                int countInSuit = Rank.Count() - combination.Hand.Count() - combination.Discard.CountInSuit(combination.Hand[0].Suit);
                output +=  countInSuit;
            }

            // if any jacks present, add one for every possible draw card in suit
            foreach (Suit suit in combination.Hats.Keys)
            {
                output += Rank.Count() - combination.Hand.CountInSuit(suit) - combination.Discard.CountInSuit(suit);
            }

            // if possible to draw pair, trip or four-of-a-kind, add relevant number of points (2, 4, 6)
            foreach (int rank in combination.Hand.Select(o => o.Rank.Value).Distinct())
            {
                int countInRank = combination.Hand.CountInRank(rank);
                output += (SuitUtil.Count - combination.Discard.CountInRank(rank) - countInRank) * 2 * countInRank;
            }

            // for each possible rank of draw card...
            for (int i = 1; i <= 13; i++) 
            {
                // determine number of possible draw
                int rankDrawCards = (SuitUtil.Count - combination.Hand.CountInRank(i) - combination.Discard.CountInRank(i));

                // determine value added by runs for this draw, multiplied by number of draw cards in rank
                output += getRuns(combination, i) * rankDrawCards;

                // determine value added by fifteens for this draw, multiplied by number of draw cards in rank
                output += getFifteens(combination, i) * rankDrawCards;
            }

            // ...and subtract the value of each type which existed in the hand prior to the draw
            // TODO: *** avoid repetition *** => 'runValueInHand' and 'fifteenValueInHand' are also calculated in GetHandValue.
            int totalDrawCards = CardUtil.DrawCount(combination.Hand.Length + combination.Discard.Length);
            output -= (getRuns(combination) - getFifteens(combination)) * totalDrawCards;

            // divide possible points by number of possible draws to get expected value
            return (double)output / totalDrawCards;
        }
    }
}

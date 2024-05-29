using Cribbage.Model;
using Cribbage.Model.CombinationTree;
using Cribbage.Model.Enums;
using Cribbage.Model.Utilities;


namespace Cribbage.Controllers
{
    internal static class PointCombinations
    {
        internal static int GetHandPoints(this RootNode root) 
        {
            int output = 0;
            foreach (Node node in root.AllNodes) 
            {
                output += node.Combination.IsFifteens() ? 2 : 0;
                output += node.Combination.IsPair() ? 2 : 0;
                output += node.IsRun() ? node.Combination.Count : 0;
                output += node.IsFlush() ? node.Root.Cards.HandAndDraw.Count : 0;
            }
            return output;
        }
        internal static int GetDiscardPoints(this ISet<Card> discard) 
        {
            return (IsFifteens(discard) ? 2 : 0) + (IsPair(discard) ? 2 : 0);
        }
        internal static bool IsFifteens(this ISet<Card> cards) => cards.Select(o => o.Value()).Sum() == 15;

        internal static bool IsPair(this ISet<Card> cards) => cards.Count == 2 && cards.First().Rank == cards.Last().Rank;

        internal static bool IsRun(this Node node) 
        {
            if (node.Combination.Count < 3) 
            {
                return false;
            }
            for (int i = 1; i < node.Combination.Count; i++)
            {
                if (node.Combination.ElementAt(i - 1).Rank - node.Combination.ElementAt(i).Rank != 1)
                {
                    return false;
                }
            }
            return !node.Root.Cards.HandAndDraw.Any(o => o.Rank == node.Combination.Last().Rank - 1);
        }

        internal static bool IsFlush(this Node node) 
        {
            return node.Root.Cards.HandAndDraw.Count == node.Combination.Count && node.Root.Cards.HandAndDraw.All(o => o.Suit == node.Root.Cards.HandAndDraw.First().Suit);
        }

        internal static int HatPoints(this Cards cards)
        {
            int output = 0;
            IEnumerable<Card> jacks = cards.Hand.Where(o => o.Rank == Rank.JACK);
            foreach (Card jack in jacks)
            {
                output += Rank.Count() - cards.DealAndDraw.Count(o => o.Suit == jack.Suit);
            }
            return output;
        }

        internal static int GetPointCombinations(this Node node)
        {
            int output = 0;
            ISet<Card> combo = node.Combination;
            ISet<Card> handAndDraw = node.Root.Cards.HandAndDraw;
            if (combo.Select(o => o.Value()).Sum() == 15)
            {
                output += 2;
            }
            if (combo.Count == 2 && combo.First().Rank == combo.Last().Rank)
            {
                output += 2;
            }
            if (handAndDraw.Count == combo.Count &&
                    handAndDraw.All(o => o.Suit == handAndDraw.First().Suit))
            {
                output += handAndDraw.Count;
            }
            bool isRun = true;
            for (int i = 1; i < combo.Count; i++)
            {
                if (combo.ElementAt(i - 1).Rank - combo.ElementAt(i).Rank != 1)
                {
                    isRun = false;
                    break;
                }
            }
            if (isRun && handAndDraw.Any(o => o.Rank == combo.Last().Rank - 1))
            {
                output += combo.Count;
            }
            return output;
        }
    }
}

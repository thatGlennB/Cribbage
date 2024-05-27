using Cribbage.Model;
using Cribbage.Model.CombinationTree;
using Cribbage.Model.Enums;
using Cribbage.Model.Utilities;


namespace Cribbage.Controllers
{
    internal class PointCalculator
    {
        internal readonly RootNode RootNode;
        internal IEnumerable<int> DrawValues { get; private set; }
        internal int Hand { get; private set; }
        internal int Discard { get; private set; }
        internal int HatValue { get; private set; }
        internal DrawCard DrawCard = DrawCard.Instance;
        internal PointCalculator(RootNode root)
        {
            RootNode = root;
            
            Hand = GetPointInSet();
            DrawValues = GetDrawValues();

            HatValue = 0;
            IEnumerable<Card> jacks = RootNode.Cards.Where(o => o.Rank == Rank.JACK);
            if (jacks.Any())
            {
                foreach(Suit suit in jacks.Select(o => o.Suit)) 
                {
                    Card[] possibleDrawsInSuit = RootNode.Hand.Union(RootNode.Discard).ToArray();
                    HatValue += CardUtil.CountInSuit(possibleDrawsInSuit, suit);
                }
            }

            if(RootNode.Discard.Select(o => o.Value()).Sum() == 15) 
            {
                Discard += 2;
            }
            if (RootNode.Discard.Count == 2 && RootNode.Discard.First().Rank == RootNode.Discard.Last().Rank) 
            {
                Discard += 2;
            }
        }
        private List<int> GetDrawValues()
        {
            List<int> output = [];
            for (int i = 1; i <= Rank.Count(); i++) 
            {
                output.Add(GetPointInSet(i) - Hand);
            }
            return output;
        }
        private int GetPointInSet(int drawCardRank = 0) 
        {
            Card? drawCard = null;
            int output = 0;
            if (drawCardRank != 0) 
            {
                for(int i = 0; i < SuitUtil.Count; i++) 
                {
                    drawCard = CardUtil.GetCard(drawCardRank, (Suit)i);
                    if (!RootNode.Hand.Contains(drawCard) && !RootNode.Discard.Contains(drawCard)) { break; }
                }
                if (drawCard != null && !RootNode.Hand.Contains(drawCard) && !RootNode.Discard.Contains(drawCard))
                {
                    DrawCard.Add(drawCard);
                }
            }

            foreach (Node node in RootNode.AllNodes) 
            {
                output += GetPointCombinations(node);
            }

            if (drawCard != null) 
            {
                DrawCard.Clear();
            }
            return output;
        }
        private static int GetPointCombinations(Node node)
        {
            int output = 0;
            ISet<Card> combo = node.Combination;
            ISet<Card> cards = node.Root.Cards;
            if (combo.Select(o => o.Value()).Sum() == 15)
            {
                output += 2;
            }
            if (combo.Count == 2 && combo.First().Rank == combo.Last().Rank)
            {
                output += 2;
            }
            if (cards.Count == combo.Count &&
                    cards.All(o => o.Suit == cards.First().Suit))
            {
                output += cards.Count;
            }
            bool isRun = true;
            // check that combination contains only sequential ranks
            for (int i = 1; i < combo.Count; i++)
            {
                if (combo.ElementAt(i - 1).Rank - combo.ElementAt(i).Rank != 1)
                {
                    isRun = false;
                    break;
                }
            }
            if (isRun && cards.Any(o => o.Rank == combo.Last().Rank - 1))
            {
                output += combo.Count;
            }
            return output;
        }
    }
}

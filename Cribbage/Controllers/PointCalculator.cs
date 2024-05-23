using Cribbage.Model;
using Cribbage.Model.CombinationTree;
using Cribbage.Model.Enums;
using Cribbage.Model.Utilities;


namespace Cribbage.Controllers
{
    internal class PointCalculator
    {
        public readonly Selection Selection;
        public IEnumerable<int> DrawValues { get; private set; }
        public int Hand { get; private set; }
        public int Discard { get; private set; }
        public int HatValue { get; private set; }
        public PointCalculator(Selection selection)
        {
            Selection = selection;
            
            Hand = _getPointInSet();
            DrawValues = _getDrawValues();

            HatValue = 0;
            IEnumerable<Card> jacks = Selection.Hand.Where(o => o.Rank == Rank.JACK);
            if (jacks.Count() > 0) 
            {
                foreach(Suit suit in jacks.Select(o => o.Suit)) 
                {
                    Card[] possibleDrawsInSuit = Selection.Hand.Union(Selection.Discard).ToArray();
                    HatValue += CardUtil.CountInSuit(possibleDrawsInSuit, suit);
                }
            }

            if(Selection.Discard.Select(o => o.Value).Sum() == 15) 
            {
                Discard += 2;
            }
            if (Selection.Discard.Count() == 2 && Selection.Discard.First().Rank == Selection.Discard.Last().Rank) 
            {
                Discard += 2;
            }
        }
        private IEnumerable<int> _getDrawValues () 
        {
            List<int> output = new();
            for (int i = 1; i <= Rank.Count(); i++) 
            {
                output.Add(_getPointInSet(i) - Hand);
            }
            return output;
        }
        private int _getPointInSet(int drawCardRank = 0) 
        {
            Card? drawCard = null;
            int output = 0;
            if (drawCardRank != 0) 
            {
                for(int i = 0; i < SuitUtil.Count; i++) 
                {
                    drawCard = CardUtil.GetCard(drawCardRank, (Suit)i);
                    if (!Selection.Hand.Contains(drawCard) && !Selection.Discard.Contains(drawCard)) { break; }
                }
                if (Selection.Hand.Contains(drawCard) || Selection.Discard.Contains(drawCard))
                {
                    Selection.Combinations.Add(drawCard);
                }
            }

            foreach (Node node in Selection.Combinations.Nodes) 
            {
                output += _getPointCombinations(node);
            }

            if (drawCard != null) 
            {
                Selection.Combinations.Remove(drawCard);
            }
            return output;
        }
        private int _getPointCombinations(Node node)
        {
            int output = 0;
            ISet<Card> combo = node.Combination;
            ISet<Card> cards = node.Cards;
            if (combo.Select(o => o.Value).Sum() == 15)
            {
                output += 2;
            }
            if (combo.Count() == 2 && combo.First().Rank == combo.Last().Rank)
            {
                output += 2;
            }
            if (cards.Count() == combo.Count() &&
                    cards.All(o => o.Suit == cards.First().Suit))
            {
                output += cards.Count();
            }
            bool isRun = true;
            // check that combination contains only sequential ranks
            for (int i = 1; i < combo.Count(); i++)
            {
                if (combo.ElementAt(i - 1).Rank - combo.ElementAt(i).Rank != 1)
                {
                    isRun = false;
                    break;
                }
            }
            if (isRun && cards.Any(o => o.Rank == combo.Last().Rank - 1))
            {
                output += combo.Count();
            }
            return output;
        }
    }
}

using Cribbage.DataTransferObject;
using Cribbage.Interfaces;
using Cribbage.Model;
using Cribbage.Model.CombinationTree;
using Cribbage.Model.Enums;
using Cribbage.Model.Utilities;

namespace Cribbage.Controller
{
    public class DealValueController : IDealValueController
    {
        public IEnumerable<ResultDTO> Get(ISet<Card> cards)
        {
            HashSet<ResultDTO> output = [];
            Deal deal = new(cards);
            foreach (RootNode root in deal.Roots)
            {
                root.Points.Hand = root.GetHandPoints();
                root.Points.Discard = root.Cards.Discard.GetDiscardPoints();
                root.Points.HatValue = root.Cards.HatPoints();
                root.Points.DrawValues.Clear();
                for (int i = 1; i <= RankExtension.Count(); i++)
                {
                    root.Cards.SetDrawRank(i);
                    int possibleDrawsInSuit = SuitExtension.Count - root.Cards.Deal.CountInRank(i);
                    int dummyA = root.GetHandPoints();
                    int dummyB = root.Points.Hand;
                    root.Points.DrawValues.Add(( dummyA - dummyB ) * possibleDrawsInSuit);
                    root.Cards.ClearDraw();
                }
                output.Add(new ResultDTO
                {
                    HandValue = root.Points.Hand,
                    DiscardValue = root.Points.Discard,
                    ExpectedDrawValue = (double)(root.Points.HatValue + root.Points.DrawValues.Sum()) / Util.DrawCount(root.Cards.Deal.Count),
                    Hand = root.Cards.Hand,
                    Discard = root.Cards.Discard
                }) ;
            }
            return output;
        }
    }
}

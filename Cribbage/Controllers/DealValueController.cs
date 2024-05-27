using Cribbage.Controller.DataTransferObject;
using Cribbage.Controllers;
using Cribbage.Interfaces;
using Cribbage.Model;
using Cribbage.Model.CombinationTree;
using Cribbage.Model.Utilities;

namespace Cribbage.Controller
{
    public class DealValueController : IDealValueController
    {
        public IEnumerable<ResultDTO> Get(ISet<Card> cards)
        {
            HashSet<ResultDTO> output = [];
            if (cards.Count == 5 || cards.Count == 6)
            {
                Deal deal = new(cards);
                foreach (RootNode root in deal.Roots) 
                {
                    int drawCount = CardUtil.DrawCount(cards.Count);
                    PointCalculator pc = new(root);
                    output.Add(new ResultDTO
                    {
                        HandValue = pc.Hand,
                        DiscardValue = pc.Discard,
                        ExpectedDrawValue = ((double)(pc.DrawValues.Sum() + pc.HatValue))/drawCount,
                        Hand = root.Hand,
                        Discard = root.Discard
                    }) ;
                }
            }
            else return [];
            return output;
        }
    }
}

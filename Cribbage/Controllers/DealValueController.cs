using Cribbage.Controller.DataTransferObject;
using Cribbage.Controllers;
using Cribbage.Model;
using Cribbage.Model.Utilities;

namespace Cribbage.Controller
{
    public class DealValueController : IDealValueController
    {
        public IEnumerable<ResultDTO> Get(ISet<Card> cards)
        {
            HashSet<ResultDTO> output = new();
            if (cards.Count() == 5 || cards.Count() == 6) 
            {
                Deal deal = new Deal(cards);
                foreach (Selection selection in deal.Selections) 
                {
                    int drawCount = CardUtil.DrawCount(cards.Count());
                    PointCalculator pc = new(selection);
                    output.Add(new ResultDTO
                    {
                        HandValue = pc.Hand,
                        DiscardValue = pc.Discard,
                        ExpectedDrawValue = ((double)(pc.DrawValues.Sum() + pc.HatValue))/drawCount,
                        Hand = selection.Hand,
                        Discard = selection.Discard
                    }) ;
                }
            }
            else return Enumerable.Empty<ResultDTO>();
            return output;
            throw new NotImplementedException();
        }
    }
}

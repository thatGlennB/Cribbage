using Cribbage.Model;
using Cribbage.Model.Enums;

namespace Cribbage.Incomplete
{
    public class Calculator
    {
        public ISet<Selection> Selections { get; private set; } = new HashSet<Selection>();
        public Calculator(ISet<Card> cards)
        {

            // TODO: Controller
            ValidityTest vt = ValidityTest.Instance;
            vt.Calculations.Add(Mode.FIFTEENS, (cds, combo) =>
            {
                return combo.Select(o => o.Value).Sum() == 15;
            });
            vt.Calculations.Add(Mode.RUNS, (cds, combo) =>
            {
                // check that combination contains only sequential ranks
                for (int i = 1; i < combo.Count(); i++)
                {
                    if (combo.ElementAt(i - 1).Rank - combo.ElementAt(i).Rank != 1)
                    {
                        return false;
                    }
                }
                // check that there is no card that could continue the sequence
                if (cds.Any(o => o.Rank == combo.Last().Rank - 1))
                {
                    return false;
                }
                return true;
            });
            vt.Calculations.Add(Mode.PAIRS, (cds, combo) =>
            {
                return combo.Count() == 2 && combo.First().Rank == combo.Last().Rank;
            });
            vt.Calculations.Add(Mode.FLUSH, (cds, combo) =>
            {
                return
                    cds.Count() == combo.Count() &&
                    cds.All(o => o.Suit == cds.First().Suit);
            });



            // TODO: Model
            if (cards.Count == 5)
            {
                foreach (Card card in cards)
                {
                    HashSet<Card> discard = new HashSet<Card> { card };
                    HashSet<Card> hand = cards.Where(o => !discard.Contains(o)).ToHashSet();
                    Selections.Add(new Selection(hand, discard));
                }
            }
            else if (cards.Count == 6)
            {
                foreach (Card card in cards)
                {
                    foreach (Card secondCard in cards.Where(o => o != card))
                    {
                        // create hand and discard
                        HashSet<Card> discard = new HashSet<Card> { card, secondCard };
                        HashSet<Card> hand = cards.Where(o => !discard.Contains(o)).ToHashSet();

                        // check if selections already contains this hand
                        if (Selections.Any(o => o.Hand.All(p => hand.Contains(p)))) continue;
                        Selections.Add(new Selection(hand, discard));
                    }
                }
            }
        }
    }
}

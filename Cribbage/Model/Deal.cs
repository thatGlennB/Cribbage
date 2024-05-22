namespace Cribbage.Model
{
    public class Deal
    {
        public ISet<Selection> Selections { get; private set; } = new HashSet<Selection>();
        public Deal(ISet<Card> cards)
        {
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

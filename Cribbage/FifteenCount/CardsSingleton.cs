namespace Cribbage.FifteenCount
{
    public sealed class CardsSingleton
    {
        public CardsSingleton() { }

        public ISet<Card> Cards = new HashSet<Card>();

        public void Add(Card card)
        {
            if (!Cards.Contains(card))
            {
                Cards.Add(card);
            }

        }

        public void Add(ISet<Card> cards) 
        {
            foreach (Card card in cards) 
            {
                Add(card);
            }
        }

        public void Remove(Card card)
        {
            if (Cards.Contains(card))
            {
                Cards.Remove(card);
            }
        }

    }
}
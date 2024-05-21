namespace Cribbage
{
    public sealed class CardsSingleton
    {
        private CardsSingleton() { }
        private static readonly Lazy<CardsSingleton> _s = new Lazy<CardsSingleton>(() => new CardsSingleton());
        public static CardsSingleton Instance
        {
            get => _s.Value;
        }

        public ISet<Card> Cards { get; private set; } = new HashSet<Card>();

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
        public void Clear() 
        {
            Cards.Clear();
        }

    }
}
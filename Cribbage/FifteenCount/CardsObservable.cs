namespace Cribbage.FifteenCount
{
    public sealed class CardsObservable : IObservable<ISet<Card>>
    {
        public CardsObservable() { }

        public ISet<Card> Cards = new HashSet<Card>();
        public ISet<IObserver<ISet<Card>>> _observers = new HashSet<IObserver<ISet<Card>>>();

        public void Add(Card card)
        {
            if (!Cards.Contains(card))
            {
                Cards.Add(card);
                foreach (IObserver<ISet<Card>> observer in _observers)
                {
                    observer.OnNext(Cards);
                }
            }

        }

        public void Add(ISet<Card> cards) 
        {
            foreach (Card card in cards) 
            {
                Add(card);
            }
        }

        public bool Contains(Card card) => Cards.Contains(card);

        public int Count => Cards.Count;

        public void Remove(Card card)
        {
            if (Cards.Contains(card))
            {
                Cards.Remove(card);
                foreach (IObserver<ISet<Card>> observer in _observers)
                {
                    observer.OnNext(Cards);
                }
            }
        }

        public IDisposable Subscribe(IObserver<ISet<Card>> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
                observer.OnNext(Cards);
            }
            return new Subscriber<ISet<Card>>(_observers, observer);
        }
    }
}
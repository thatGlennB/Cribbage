

namespace Cribbage.FifteenCount
{
    public sealed class CardsObservable : IObservable<List<Card>>
    {
        private static readonly object _lock = new object();
        private CardsObservable() { }
        private static CardsObservable _instance = null!;
        public static CardsObservable Instance 
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new CardsObservable();
                    }
                    return _instance;
                }
            }
        }

        private List<Card> _cards;
        public List<IObserver<List<Card>>> _observers = new();

        public void Add(Card card)
        {
            if (!_cards.Contains(card))
            {
                _cards.Add(card);
                foreach (IObserver<List<Card>> observer in _observers)
                {
                    observer.OnNext(_cards);
                }
            }

        }

        public void Add(List<Card> cards) 
        {
            foreach (Card card in cards) 
            {
                Add(card);
            }
        }

        public bool Contains(Card card) => _cards.Contains(card);

        public int Count => _cards.Count;

        public void Remove(Card card)
        {
            if (_cards.Contains(card))
            {
                _cards.Remove(card);
                foreach (IObserver<List<Card>> observer in _observers)
                {
                    observer.OnNext(_cards);
                }
            }
        }

        public IDisposable Subscribe(IObserver<List<Card>> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
                observer.OnNext(_cards);
            }
            return new Unsubscriber<List<Card>>(_observers, observer);
        }
    }
}



// three objects: root node, cards observable, combinations dump

// Nodes are the observers, stem is the observable, emitting a list of cards!
// OnNext: compare children's cards to list of cards, create new children for any new cards (if sum less than 15), or delete children if not in list.
// Update method updates list of combinations (and therefore point total)



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



// three objects: root node, cards observable, combinations dump

// Nodes are the observers, stem is the observable, emitting a list of cards!
// OnNext: compare children's cards to list of cards, create new children for any new cards (if sum less than 15), or delete children if not in list.
// Update method updates list of combinations (and therefore point total)

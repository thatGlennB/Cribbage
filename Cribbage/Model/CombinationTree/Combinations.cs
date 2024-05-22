using Cribbage.Model.Utilities;

namespace Cribbage.Model.CombinationTree
{
    public class Combinations : IObservable<ISet<Card>>
    {
        public ISet<Node> Nodes { get; set; } = new HashSet<Node>();
        private ISet<Card> _cards { get; set; } = new HashSet<Card>();

        public ISet<Card> Cards
        {
            get
            {
                Card[] cards = new Card[_cards.Count()];
                _cards.CopyTo(cards, 0);
                return cards.ToHashSet();
            }
        }
        public void Add(Card card)
        {
            _cards.Add(card);
            Update();
        }
        public void Add(ISet<Card> cards)
        {
            _cards.UnionWith(cards);
            Update();
        }
        public void Remove(Card card)
        {
            _cards.Remove(card);
            Update();
        }
        public void Clear()
        {
            _cards.Clear();
            Update();
        }

        #region Observable Implementation
        private void Update()
        {
            foreach (IObserver<ISet<Card>> observer in _observers)
            {
                observer.OnNext(_cards);
            }
        }
        public ISet<IObserver<ISet<Card>>> _observers { get; set; } = new HashSet<IObserver<ISet<Card>>>();
        public IDisposable Subscribe(IObserver<ISet<Card>> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
            return new Subscriber<ISet<Card>>(_observers, observer);
        }
        #endregion
    }
}

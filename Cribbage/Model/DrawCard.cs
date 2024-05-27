using Cribbage.Model.Utilities;

namespace Cribbage.Model
{
    internal class DrawCard : IObservable<Card?>
    {
        private readonly HashSet<IObserver<Card?>> _observers = [];
        private DrawCard() { }
        private static readonly Lazy<DrawCard> _s = new(() => new DrawCard());
        internal Card? Card { get; set; } = null;
        internal static DrawCard Instance
        {
            get => _s.Value;
        }
        internal void Add(Card card)
        {
            Card = card;
            Update();
        }
        internal void Clear()
        {
            this.Card = null;
            Update();
        }

        // TODO: need an eventhandler for changes to this.Card
        private void Update()
        {
            foreach (IObserver<Card?> observer in _observers)
            {
                observer.OnNext(Card);
            }
        }

        public IDisposable Subscribe(IObserver<Card?> observer)
        {
            _observers.Add(observer);
            return new Subscriber<Card?>(_observers, observer);
        }
    }
}

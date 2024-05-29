using Cribbage.Model.Utilities;

namespace Cribbage.Model
{
    
    internal class DrawCard : IObservable<DrawCardEventArgs>
    {
        private readonly HashSet<IObserver<DrawCardEventArgs>> _observers = [];
        private DrawCard() { }
        private static readonly Lazy<DrawCard> _s = new(() => new DrawCard());
        private Card? _card = null;
        internal Card? Card {
            get => _card;
            set
            {
                foreach (IObserver<DrawCardEventArgs> observer in _observers)
                {
                    observer.OnNext(new DrawCardEventArgs(_card,value));
                }
                _card = value;
            }
        }
        internal static DrawCard Instance
        {
            get => _s.Value;
        }


        public IDisposable Subscribe(IObserver<DrawCardEventArgs> observer)
        {
            _observers.Add(observer);
            return new Subscriber<DrawCardEventArgs>(_observers, observer);
        }
    }
}

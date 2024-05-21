namespace Cribbage
{
    public sealed class Subscriber<T> : IDisposable
    {
        private readonly ISet<IObserver<T>> _observers;
        private readonly IObserver<T> _observer;
        public Subscriber(ISet<IObserver<T>> observers, IObserver<T> observer)
        {
            _observer = observer;
            _observers = observers;
        }
        public void Dispose()
        {
            _observers.Remove(_observer);
        }
    }
}

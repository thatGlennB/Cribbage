namespace Cribbage.Model.Utilities
{
    internal sealed class Subscriber<T>(ISet<IObserver<T>> observers, IObserver<T> observer) : IDisposable
    {
        private readonly ISet<IObserver<T>> _observers = observers;
        private readonly IObserver<T> _observer = observer;

        public void Dispose()
        {
            _observers.Remove(_observer);
        }
    }
}

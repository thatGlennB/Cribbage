using Cribbage.HelperTypes;
using Cribbage.Interfaces;
using Cribbage.Model.Enums;
using Cribbage.Model.Utilities;

namespace Cribbage.Model.CombinationTree
{
    internal class Cards : IObservable<DrawCardEventArgs>
    {
        private readonly HashSet<IObserver<DrawCardEventArgs>> _observers = [];
        public Cards(ISet<ICard> hand, ISet<ICard> discard)
        {
            Hand = hand;
            Discard = discard;
        }
        private ISet<ICard> _hand = null!;
        private ISet<ICard> _discard = null!;
        private ICard? _draw;
        public ISet<ICard> Hand
        {
            get => _hand;
            set => _hand = value.Copy().ToHashSet();
        }
        public ISet<ICard> Discard
        {
            get => _discard;
            set => _discard = value.Copy().ToHashSet();
        }
        public ICard? Draw
        {
            get => _draw;
            set
            {
                if (value != _draw && (value == null || !IsInvalidDraw(value)))
                {
                    ICard? prev = _draw;
                    _draw = value;
                    foreach (IObserver<DrawCardEventArgs> observer in _observers)
                    {
                        observer.OnNext(new(prev, value));
                    }
                }
            }
        }
        private bool IsInvalidDraw(ICard draw) => Hand.Contains(draw) || Discard.Contains(draw);
        public ISet<ICard> Deal { get => Hand.Union(Discard).ToHashSet(); }
        public ISet<ICard> DealAndDraw { get => Draw == null ? Deal : Deal.Append(Draw).ToHashSet(); }
        public ISet<ICard> HandAndDraw { get => Draw == null ? Hand : Hand.Append(Draw).ToHashSet(); }
        public void ClearDraw() => Draw = null;
        public void SetDrawRank(int rank)
        {
            for (int i = 0; i < SuitExtension.Count; i++)
            {
                if (!Deal.Any(o => (int)o.Rank == rank && (int)o.Suit == i))
                {
                    Rank rnk = (Rank)rank;
                    Draw = new Card(rnk, (Suit)i);
                    return;
                }
            }
            ClearDraw();
        }
        public IDisposable Subscribe(IObserver<DrawCardEventArgs> observer)
        {
            _observers.Add(observer);
            return new Subscriber<DrawCardEventArgs>(_observers, observer);
        }
    }
}

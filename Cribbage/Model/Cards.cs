using Cribbage.Model.Enums;
using Cribbage.Model.Utilities;

namespace Cribbage.Model
{
    internal class Cards : IObservable<DrawCardEventArgs>
    {
        private readonly HashSet<IObserver<DrawCardEventArgs>> _observers = [];
        public Cards(ISet<Card> hand, ISet<Card> discard)
        {
            Hand = hand;
            Discard = discard;
        }
        private ISet<Card> _hand = null!;
        private ISet<Card> _discard = null!;
        private Card? _draw;
        public ISet<Card> Hand
        {
            get => _hand;
            set => _hand = Util.Copy(value);
        }
        public ISet<Card> Discard
        {
            get => _discard;
            set => _discard = Util.Copy(value);
        }
        public Card? Draw
        {
            get => _draw;
            set
            {
                if (value != _draw && (value == null || !IsInvalidDraw(value)))
                {
                    Card? prev = _draw;
                    _draw = value;
                    foreach (IObserver<DrawCardEventArgs> observer in _observers) 
                    { 
                        observer.OnNext(new(prev,value)); 
                    }
                }
            }
        }

        private bool IsInvalidDraw(Card draw)
        {
            return Hand.Contains(draw) || Discard.Contains(draw);
        }

        public ISet<Card> Deal { get => Hand.Union(Discard).ToHashSet(); }
        public ISet<Card> DealAndDraw { get => Draw == null ? Deal : Deal.Append(Draw).ToHashSet(); }
        public ISet<Card> HandAndDraw { get => Draw == null ? Hand : Hand.Append(Draw).ToHashSet(); }
        public void ClearDraw()
        {
            Draw = null;
        }
        public void SetDrawRank(int rank)
        {
            for (int i = 0; i < Suits.Count; i++)
            {
                if (!Deal.Any(o => o.Rank.Value == rank && (int)o.Suit == i))
                {
                    Rank? rnk = Rank.GetRank(rank) ?? throw new NullReferenceException();
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

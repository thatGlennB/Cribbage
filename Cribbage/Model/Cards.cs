using Cribbage.Model.Enums;
using Cribbage.Model.Utilities;
using System.Reflection.Metadata.Ecma335;

namespace Cribbage.Model
{
    internal class Cards : IObservable<Card?>
    {
        private readonly HashSet<IObserver<Card?>> _observers = [];
        public Cards(ISet<Card> hand, ISet<Card> discard) 
        {
            Hand = hand;
            Discard = discard;
        }
        private ISet<Card> _hand;
        private ISet<Card> _discard;
        private Card? _draw;
        public ISet<Card> Hand 
        {
            get => _hand;
            set => _hand = Copy(value); 
        }
        public ISet<Card> Discard 
        {
            get => _discard; 
            set => _discard = Copy(value);
        }
        public Card? Draw
        {
            get => _draw;
            set 
            {
                if (value != _draw && (value == null || !IsInvalidDraw(value))) 
                {
                    _draw = value;
                    foreach(IObserver<Card?> observer in _observers) { observer.OnNext(_draw); }
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
        private static HashSet<Card> Copy(ISet<Card> originals)
        {
            HashSet<Card> output = [];
            foreach (Card original in originals)
            {
                output.Add(new Card(original.Rank, original.Suit));
            }
            return output;
        }
        public void ClearDraw() 
        {
            Draw = null;
        }
        public void SetDrawRank(int rank)
        {
            for (int i = 0; i < Suits.Count; i++)
            {
                if (Deal.Any(o => o.Rank.Value == rank && (int)o.Suit == i))
                {
                    ClearDraw();
                }
                else
                {
                    Rank? rnk = Rank.GetRank(rank) ?? throw new NullReferenceException();
                    Draw = new Card(rnk, (Suit)i);
                    return;
                }
            }
        }

        public IDisposable Subscribe(IObserver<Card?> observer)
        {
            _observers.Add(observer);
            return new Subscriber<Card?>(_observers, observer);
        }
    }
}

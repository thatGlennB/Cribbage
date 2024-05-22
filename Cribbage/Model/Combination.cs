using Cribbage.Model.Enums;

namespace Cribbage.Model
{
    public class Combination
    {
        public int Index { get; init; }
        public Mode Mode { get; init; }
        public ISet<Card> Cards { get; init; }
        public Combination(int index, Mode mode, ISet<Card> cards)
        {
            Index = index;
            Mode = mode;
            Cards = cards;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            if (obj != null && obj.GetType() == typeof(Combination))
            {
                Combination cb = (Combination)obj;
                return
                    cb.Index == Index &&
                    cb.Mode == Mode &&
                    cb.Cards.All(o =>
                        Cards.Any(p =>
                        p.CompareTo(o) == 0));
            }
            return false;
        }
    }
}

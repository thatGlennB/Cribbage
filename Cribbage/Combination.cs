namespace Cribbage
{
    public class Combination
    {
        public Card[] Hand { get; set; } = new Card[4];
        public Card[] Discard { get; set; } = new Card[2];
        public int ExpectedValue { get; set; }
    }
}

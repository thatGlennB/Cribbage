namespace Cribbage
{
    public enum Suit
    {
        HEARTS, DIAMONDS, CLUBS, SPADES
    }
    public static class SuitUtil 
    {
        public static int Count => Enum.GetNames(typeof(Suit)).Length;
    }
}

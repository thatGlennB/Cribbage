namespace Cribbage
{
    public enum Suit
    {
        HEARTS, DIAMONDS, CLUBS, SPADES
    }
    public static class SuitUtil 
    {
        public static int Count => Enum.GetNames(typeof(Suit)).Length;
        public static IEnumerable<char> ToCharArray()
        {
            List<char> output = new();
            foreach (string name in Enum.GetNames(typeof(Suit))) 
            {
                output.Add(name.ToUpper()[0]);
            }
            return output;
        }
    }
}

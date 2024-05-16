namespace Cribbage
{

    // TODO: replace with enumerable - or figure out better way to implement Count and have implicit conversion to integer
    // TODO: is Pips even necessary? I could have an extension method to convert rank to pips.
    public class Rank : IComparable
    {
        public static Rank ACE = new("A", 1, 1);
        public static Rank TWO = new("2", 2, 2);
        public static Rank THREE = new("3", 3, 3);
        public static Rank FOUR = new("4", 4, 4);
        public static Rank FIVE = new("5", 5, 5);
        public static Rank SIX = new("6", 6, 6);
        public static Rank SEVEN = new("7", 7, 7);
        public static Rank EIGHT = new("8", 8, 8);
        public static Rank NINE = new("9", 9, 9);
        public static Rank TEN = new("10", 10, 10);
        public static Rank JACK = new("J", 10, 11);
        public static Rank QUEEN = new("Q", 10, 12);
        public static Rank KING = new("K", 10, 13);
        private Rank(string name, int pips, int value)
        {
            Name = name;
            Pips = pips;
            Value = value;
        }
        public static int Count => 13;
        public string Name { get; set; }
        public int Pips { get; set; }
        public int Value { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public int CompareTo(object? obj)
        {
            if (obj == null) return 1;
            return Value.CompareTo(((Rank)obj).Value);
        }
    }
}

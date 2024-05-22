using System.Reflection;

namespace Cribbage.Model.Enums
{

    // TODO: replace with enumerable - or have implicit conversion to integer


    public class Rank : IComparable
    {
        public static Rank ACE = new("A", 1);
        public static Rank TWO = new("2", 2);
        public static Rank THREE = new("3", 3);
        public static Rank FOUR = new("4", 4);
        public static Rank FIVE = new("5", 5);
        public static Rank SIX = new("6", 6);
        public static Rank SEVEN = new("7", 7);
        public static Rank EIGHT = new("8", 8);
        public static Rank NINE = new("9", 9);
        public static Rank TEN = new("10", 10);
        public static Rank JACK = new("J", 11);
        public static Rank QUEEN = new("Q", 12);
        public static Rank KING = new("K", 13);
        private Rank(string name, int value)
        {
            Name = name;
            Value = value;
        }
        public static int Count()
        {
            return typeof(Rank).GetMembers().Count(o => o.MemberType == MemberTypes.Field);
        }
        public string Name { get; set; }
        public int Value { get; set; }
        public static IEnumerable<string> GetNames()
        {
            List<string> output = new();
            for (int i = 0; i < Count(); i++)
            {
                Rank? rank = getRank(i);
                if (rank == null) continue;
                yield return rank.Name;
            }
        }
        public int CompareTo(object? obj)
        {
            if (obj == null) return 1;
            return Value.CompareTo(((Rank)obj).Value);
        }
        public static implicit operator int(Rank rank) => rank.Value;
        public static Rank? getRank(int rank)
        {
            foreach (FieldInfo field in typeof(Rank).GetFields())
            {
                object? result = field.GetValue(null);
                if (result == null) continue;
                if (result.GetType() == typeof(Rank))
                {
                    Rank output = (Rank)result;
                    if (output.Value == rank) return output;
                }
            }
            return null;
        }
    }
}

using System.Reflection;
using System.Runtime.CompilerServices;

namespace Cribbage.Model.Enums
{

    // TODO: can Lazy do this?

    public enum Rank : int
    {
        NONE = 0,
        ACE = 1,
        TWO =  2,
        THREE =  3,
        FOUR = 4,
        FIVE = 5,
        SIX = 6,
        SEVEN = 7,
        EIGHT = 8,
        NINE = 9,
        TEN = 10,
        JACK = 11,
        QUEEN = 12,
        KING = 13
    }

    public static class RankExtension
    {
        public static int Count()
        {
            return 13;
        }
        public static string Name(this Rank rank) => (int)rank == 1 ? "A" : (int)rank <= 10 ? ((int)rank).ToString() : rank.ToString()[..1];
        internal static IEnumerable<char> ToCharArray()
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                yield return rank.Name().ToUpper()[0];
        }
    }
}

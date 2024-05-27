using Cribbage.Model.Enums;

namespace Cribbage.Model.Utilities
{ 
    internal static class Suits
    {
        internal static int Count => Enum.GetNames(typeof(Suit)).Length;
        internal static IEnumerable<char> ToCharArray()
        {
            List<char> output = [];
            foreach (string name in Enum.GetNames(typeof(Suit)))
            {
                output.Add(name.ToUpper()[0]);
            }
            return output;
        }
    }
}

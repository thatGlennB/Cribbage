using Cribbage.Interfaces;
using Cribbage.Model.Enums;

namespace Cribbage.Model.Utilities
{
    internal static class CardExtensions
    {
        internal static bool IsSequential(this Card card, Card target) => Math.Abs(card.Rank - target.Rank) <= 1;
        public static int Value(this Card card) => (int)card.Rank > 9 ? 10 : (int)card.Rank;
        internal static int CountInRank(this IEnumerable<Card> cards, int rank) => cards.Count(o => (int)o.Rank == rank);
        internal static int CountInSuit(this IEnumerable<Card> cards, Suit suit) => cards.Count(o => o.Suit == suit);

        internal static string Shorthand(this Card card) => string.Concat(card.Rank.Name(), card.Suit.ToString().AsSpan(0, 1));

        internal static IEnumerable<Card> Copy(this IEnumerable<Card> originals)
        {
            foreach (Card original in originals)
                yield return new Card(original.Rank, original.Suit);
        }
    }
}

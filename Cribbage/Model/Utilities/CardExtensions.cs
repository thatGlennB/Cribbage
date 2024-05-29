using Cribbage.Interfaces;
using Cribbage.Model.Enums;

namespace Cribbage.Model.Utilities
{
    internal static class CardExtensions
    {
        internal static bool IsSequential(this ICard card, ICard target) => Math.Abs(card.Rank - target.Rank) <= 1;
        public static int Value(this ICard card) => (int)card.Rank > 9 ? 10 : (int)card.Rank;
        internal static int CountInRank(this IEnumerable<ICard> cards, int rank) => cards.Count(o => (int)o.Rank == rank);
        internal static int CountInSuit(this IEnumerable<ICard> cards, Suit suit) => cards.Count(o => o.Suit == suit);

        internal static string Shorthand(this ICard card) => string.Concat(card.Rank.Name(), card.Suit.ToString().AsSpan(0, 1));

        internal static IEnumerable<ICard> Copy(this IEnumerable<ICard> originals)
        {
            foreach (ICard original in originals)
                yield return new Card(original.Rank, original.Suit);
        }
    }
}

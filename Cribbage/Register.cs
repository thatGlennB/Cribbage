namespace Cribbage
{
    // TODO: replace list with more appropriate string holder
    // stringbuilder? streamwriter? not sure...
    // TODO: create text formats
    public class Register
    {
        public readonly List<string> Messages = new();
        public Register()
        {
            Messages.Add(
                Format.Title("New Cribbage Hand"));
        }

        public void AddCard(Card card)
        {
            Messages.Add(
                Format.Event("Card added: " + card.Shorthand()));
        }
        public void RemoveCard(Card card)
        {
            Messages.Add(
                Format.Event("Card removed: " + card.Shorthand()));
        }
        public void NewHand(List<Card> cards)
        {
            Messages.Add(
                Format.Event("Cards in hand: " + cards.Shorthand()));
        }
        public void PrintCombinations(ISet<ISet<Card>> combos, Mode mode)
        {
            Messages.Add(Format.Title("Ways to make " + mode.ToString()));
            foreach (ISet<Card> combo in combos)
            {
                Messages.Add(combo.Shorthand());
            }
        }
    }
    internal static class Format
    {
        public static string Shorthand(this IEnumerable<Card> cards)
        {
            return string.Join(",", cards.Select(o => o.Shorthand()));
        }
        public static string Shorthand(this Card card) => $"{card.Rank.Name}{card.Suit.ToString().Substring(0, 1)}";
        public static string Title(string message) => $"\n --- {message.ToUpper()} --- \n";
        public static string Event(string message) => $"{message}";
    }
}
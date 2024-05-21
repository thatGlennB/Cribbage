// See https://aka.ms/new-console-template for more information

using Cribbage;
using Cribbage.FifteenCount;
using System.Reflection;

Console.WriteLine("Hello, World!");

HashSet<Card> cards = new();

cards.Add(new Card(Rank.ACE, Suit.HEARTS));
cards.Add(new Card(Rank.ACE, Suit.SPADES));
cards.Add(new Card(Rank.KING, Suit.DIAMONDS));
cards.Add(new Card(Rank.KING, Suit.CLUBS));
cards.Add(new Card(Rank.KING, Suit.HEARTS));
cards.Add(new Card(Rank.KING, Suit.SPADES));

Calculator calc = new Calculator(cards);

foreach (Selection selection in calc.Selections) 
{
    Console.Write(string.Join(",",selection.Hand.Select(o => o.Rank.ToString() + o.Suit.ToString().Substring(0,1))));
    Console.Write("\t" + string.Join(",", selection.Discard.Select(o => o.Rank.ToString() + o.Suit.ToString().Substring(0, 1))));
    Console.Write($"\t{selection.HandValue}");
    Console.Write($"\t{selection.DiscardValue}");
    Console.Write($"\t{selection.ExpectedValue.ToString("#.##")}\n");
}

Console.WriteLine("");

/* TODO: "selection" object containing:
 *  - register object
 *  - roots for each mode (3 in total)
 *  - method to check for flush
 *  - method to check for hat
 *  - method that loops draw card and calculates score
 *  - score register
 */

/* TODO: "Hand" class, containing six cards and 6C4 = 15 "Selection" objects
 *  - stringbuilder containing output of calculations:
 *      * score for every hand
 *      * ordered in descending total value
 *  - ability to access registers of each "selection" object if more granular output is needed
 */

/* TODO: "Menu" class, that mediates between user and program
 *  - presents options to user and registers user input:
 *      * records cards in user's hand
 *      * outputs calculation to user
 *      * gives user the option to explore more granular statistics
 */

// TODO: organize project in terms of MVC
// TODO: create more appealing view (blazor? api?)
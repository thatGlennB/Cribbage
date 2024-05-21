// See https://aka.ms/new-console-template for more information

using Cribbage;
using Cribbage.FifteenCount;

Console.WriteLine("Hello, World!");

//Console.WriteLine("\tKey\tKeyChar\tModifiers");
//int i = 0;
//do 
//{
//    ConsoleKeyInfo cki = Console.ReadKey();
//    Console.WriteLine($"\t{cki.Key.ToString()}\t{cki.KeyChar.ToString()}\t{cki.Modifiers.ToString()}");
//    if (cki.Key == ConsoleKey.Escape) break;
//} while (i++ < 100);

Card? selected = null;

while (true)
{
    Console.Clear();
	Console.WriteLine("Select a card by typing in a rank [A, 2, 3, ... K] and a suit [H, C, D, S]. (eg. 6S = 6 of spades)");
    string? line = Console.ReadLine();
	if (line == null || line == "Q" || line.Length < 1) 
	{
		break;
	}
	string strRank = line.Substring(0, line.Length - 1);
	char chrSuit = line.Substring(line.Length - 1).ToCharArray().First();
	string[] validRanks = ["A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"];
	char[] validSuits = ['H', 'D', 'C', 'S'];
	if (!validRanks.Contains(strRank))
	{
		Console.WriteLine($"Input not recognized: {strRank} is not a valid rank");
	}
	else if (!validSuits.Contains(chrSuit))
	{
		Console.WriteLine($"Input not recognized: {chrSuit} is not a valid suit");
	}
	else 
	{
		Console.WriteLine($"Card selected: {strRank}{chrSuit.ToString()}\nAdd to hand?(Y/N)");
    }
	Console.WriteLine("Press esc to quit or any other key to continue");
    ConsoleKeyInfo cki = Console.ReadKey();
    if (cki.Key == ConsoleKey.Escape) break;
}




Console.WriteLine("\nEND");
Console.ReadKey();


//HashSet<Card> cards = new();

//cards.Add(new Card(Rank.ACE, Suit.HEARTS));
//cards.Add(new Card(Rank.ACE, Suit.SPADES));
//cards.Add(new Card(Rank.KING, Suit.DIAMONDS));
//cards.Add(new Card(Rank.KING, Suit.CLUBS));
//cards.Add(new Card(Rank.KING, Suit.HEARTS));
//cards.Add(new Card(Rank.KING, Suit.SPADES));

//Calculator calc = new Calculator(cards);

//foreach (Selection selection in calc.Selections) 
//{
//    Console.Write(string.Join(",",selection.Hand.Select(o => o.Rank.ToString() + o.Suit.ToString().Substring(0,1))));
//    Console.Write("\t" + string.Join(",", selection.Discard.Select(o => o.Rank.ToString() + o.Suit.ToString().Substring(0, 1))));
//    Console.Write($"\t{selection.HandValue}");
//    Console.Write($"\t{selection.DiscardValue}");
//    Console.Write($"\t{selection.ExpectedValue.ToString("#.##")}\n");
//}


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
// See https://aka.ms/new-console-template for more information
using Cribbage;
using Cribbage.FifteenCount;

Console.WriteLine("Hello, World!");

HashSet<Card> cards = new();
cards.Add(new Card(Rank.TEN, Suit.HEARTS));
cards.Add(new Card(Rank.TEN, Suit.SPADES));
cards.Add(new Card(Rank.NINE, Suit.HEARTS));
cards.Add(new Card(Rank.FIVE, Suit.HEARTS));


HashSet<Card> bcards = new();
bcards.Add(new Card(Rank.EIGHT, Suit.HEARTS));
bcards.Add(new Card(Rank.EIGHT, Suit.SPADES));
bcards.Add(new Card(Rank.SEVEN, Suit.HEARTS));
bcards.Add(new Card(Rank.SEVEN, Suit.SPADES));



HashSet<Card> ccards = new();
ccards.Add(new Card(Rank.TEN, Suit.HEARTS));
ccards.Add(new Card(Rank.TWO, Suit.SPADES));
ccards.Add(new Card(Rank.THREE, Suit.HEARTS));
ccards.Add(new Card(Rank.FIVE, Suit.HEARTS));

Root root = new Root(ccards);

printResult(root);


void printResult(Root root) 
{
    Console.WriteLine(titleLine("new hand"));
    Console.Write("Cards in Hand: ");
    Console.Write(String.Join(", ", root.Cards.Select(o => outRS(o))));
    Console.Write("\n");
    Console.WriteLine(titleLine("Ways to make fifteen"));
    foreach (HashSet<Card> combo in root.Set) 
    {
        Console.WriteLine(String.Join(", ", combo.Select(o => outRS(o))));
    }
}
string titleLine(string message) => "\n --- " + message.ToUpper() + " --- \n";
string outRS(Card card) => $"{card.Rank.Name}{card.Suit.ToString().Substring(0, 1)}";


// there are 6 cards in your hand, of which you need to choose 4
// 6C4 = 15 possible hands
// for each hand, there are 52 - 6 = 46 possible draws
// so in total, there are 15 * 46 = 690 possible combinations
// number of possible combinations can be reduced by ignoring suit (15 * 13 = 195):
//  - do calculation for each rank
//  - multiply value for number of cards of that rank not in the deal
//  - if there is a jack of a given suit, add 1 point for that suit
//  - divide sum by 46


/*  
    ABCD 
    ABCE
    ABCF
    ABDE
    ABDF

    ABEF
    ACDE
    ACDF
    ACEF
    ADEF

    BCDE
    BCDF
    BCEF
    BDEF
    CDEF
 */

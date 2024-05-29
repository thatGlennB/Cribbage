// See https://aka.ms/new-console-template for more information

using Cribbage.Controller;
using Cribbage.Controller.DataTransferObject;
using Cribbage.Interfaces;
using Cribbage.Model;
using Cribbage.Model.Enums;
using Cribbage.Model.Utilities;

Console.WriteLine("Hello, World!");



/* TODO
 */


/* TODO: "Menu" class, that mediates between user and program
 *  - presents options to user and registers user input:
 *      * records cards in user's hand
 *      * outputs calculation to user
 *      * gives user the option to explore more granular statistics
 */

/* TODO: organize project in terms of MVC
 *  - View: observer that watches model and updates user interface
 *  - Model: data structure - everything should be "dumb"
 *  - Controller: interface that accepts user input and updates model
 */

// TODO: unit tests
// TODO: create more appealing view (blazor? api?)

List<Card> cards =
[
    new Card(Rank.FIVE, Suit.HEARTS),
    new Card(Rank.FIVE, Suit.CLUBS),
    new Card(Rank.FIVE, Suit.SPADES),
    new Card(Rank.JACK, Suit.DIAMONDS),
    new Card(Rank.ACE, Suit.HEARTS),
    new Card(Rank.EIGHT, Suit.HEARTS),
];


IEnumerable<ResultDTO> result = new DealValueController().Get(cards.ToHashSet());
foreach (ResultDTO resultDTO in result)
{
    Console.Write($"\t{string.Join(",", resultDTO.Hand.Select(o => o.Shorthand()))}");
    Console.Write($"\t{string.Join(",", resultDTO.Discard.Select(o => o.Shorthand()))}");
    Console.Write($"\t{resultDTO.HandValue}");
    Console.Write($"\t{resultDTO.DiscardValue}");
    Console.Write($"\t{resultDTO.ExpectedDrawValue}");
    Console.Write($"\n");
}

Console.WriteLine("end");


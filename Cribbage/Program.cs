// See https://aka.ms/new-console-template for more information

using Cribbage.Controller;
using Cribbage.Controller.DataTransferObject;
using Cribbage.Model;
using Cribbage.Model.Enums;

Console.WriteLine("Hello, World!");


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

List<Card> cards = new();
cards.Add(new Card(Rank.FIVE, Suit.HEARTS));
cards.Add(new Card(Rank.FIVE, Suit.CLUBS));
cards.Add(new Card(Rank.FIVE, Suit.SPADES));
cards.Add(new Card(Rank.JACK, Suit.DIAMONDS));
cards.Add(new Card(Rank.ACE, Suit.HEARTS));
cards.Add(new Card(Rank.EIGHT, Suit.HEARTS));




IDealValueController dvc = new DealValueController();
IEnumerable<ResultDTO> result = dvc.Get(cards.ToHashSet());

Console.WriteLine("end");


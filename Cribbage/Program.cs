// See https://aka.ms/new-console-template for more information

using Cribbage.Model;
using Cribbage.Model.Enums;

Console.WriteLine("Hello, World!");



/* TODO: replace 3 roots in Selection with a single root
 *   * do not test combination validity when constructing tree; build any node where child card is less than parent, regardless of validity
 *   * implement "chain of responsibility" pattern, to pass point-scoring criteria into tree
 *   
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


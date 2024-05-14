// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

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

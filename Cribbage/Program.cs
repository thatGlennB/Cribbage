// See https://aka.ms/new-console-template for more information
using Cribbage.FifteenCount;

Console.WriteLine("Hello, World!");


List<int> values = [10, 6, 5, 4];
List<int> newValues = [10, 9, 6, 5, 4];
Node root = new Node(values);


Node copyRoot = (Node)root.Copy();

Run(root.GetCombinations(), values);
Run(copyRoot.GetCombinations(), values);

root.Append(9);

Run(root.GetCombinations(), newValues);
Run(copyRoot.GetCombinations(), values);

Console.WriteLine("Done");
Console.ReadLine();

void Run(List<int[]> combos, List<int> values) 
{
    Console.WriteLine(" --- --- ");
    foreach (int[] combo in combos) 
    {
        List<int> result = new();
        foreach (int value in combo) 
        {
            result.Add(values.ElementAt(value));
        }
        Console.WriteLine(String.Join(", ",result));
    }
}

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

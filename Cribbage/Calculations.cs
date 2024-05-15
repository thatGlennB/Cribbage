//using System.ComponentModel.Design;
//using System.Runtime.CompilerServices;

//namespace Cribbage
//{
//    public static class Calculations
//    {
//        public static IEnumerable<int>GetFifteens(this Card[] cards)
//        {
//            // sort array
//            // start from largest card
//            // skip through combinations that are more than 15
//            throw new NotImplementedException();
//        }
//        public static IEnumerable<int> GetPairs(this Card[] cards)
//        {
//            int output = 0;
//            for (int i = 0; i < cards.Length; i++) {
//                for (int j = i + 1; j < cards.Length; j++)
//                {
//                    if (cards[i].Rank.Pips == cards[j].Rank.Pips)
//                    {
//                        output += 2;
//                    }
//                }
//            }
//            return output;
//        }
//        public static IEnumerable<int> GetRuns(this Card[] cards)
//        {
//            int output = 0;
//            // get trimmed cards
//            // check for doubles and triples
//            // if no doubles, value is just array length
//            // else, uhhhh... a bit more complicated
//            throw new NotImplementedException();
//        }
//        public static IEnumerable<int> GetHat(this IEnumerable<Card> cards)
//        {
//            for (int i = 0; i < cards.Count(); i++) 
//            {
//                if (cards[i].Rank == CardValue.JACK && cards[i].Suit == cards[5].Suit) 
//                {
//                    return 1;
//                }
//            }
//            return 0;
//        }
//        public static IEnumerable<int> GetFlush(this IEnumerable<Card> cards)
//        {
//            for (int i = 1; i < cards.Count(); i++)
//            {
//                if (cards.ElementAt(i).Suit != cards.ElementAt(0).Suit)
//                {
//                    return 0;
//                }
//            }
//            return 1;
//        }
//    }
//}

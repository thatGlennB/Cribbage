﻿namespace Cribbage
{
    public class CardRank : IComparable
    {
        public static CardRank ACE = new("A", 1, 1);
        public static CardRank TWO = new("2", 2, 2);
        public static CardRank THREE = new("3", 3, 3);
        public static CardRank FOUR = new("4", 4, 4);
        public static CardRank FIVE = new("5", 5, 5);
        public static CardRank SIX = new("6", 6, 6);
        public static CardRank SEVEN = new("7", 7, 7);
        public static CardRank EIGHT = new("8", 8, 8);
        public static CardRank NINE = new("9", 9, 9);
        public static CardRank TEN = new("10", 10, 10);
        public static CardRank JACK = new("J", 10, 11);
        public static CardRank QUEEN = new("Q", 10, 12);
        public static CardRank KING = new("K", 10, 13);
        private CardRank(string name, int pips, int value)
        {
            Name = name;
            Pips = pips;
            Value = value;
        }
        public string Name { get; set; }
        public int Pips { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(object? obj)
        {
            if (obj == null) return 1;
            return Value.CompareTo(((CardRank)obj).Value);
        }
    }
}

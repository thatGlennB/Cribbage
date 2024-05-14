namespace Cribbage
{
    public class CardRank : IComparable
    {
        public CardRank(string name, int value, int rank)
        {
            Name = name;
            Value = value;
            Rank = rank;
        }
        public string Name { get; set; }
        public int Value { get; set; }
        public int Rank { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(object? obj)
        {
            return Rank.CompareTo(((CardRank)obj).Rank);
        }
    }
}

namespace Cribbage.FifteenCount
{
    public class Combinations
    {
        public List<List<Card>> Set { get; init; } = new();
        public void Add(List<Card> combination) 
        {
            if (!Set.Contains(combination)) 
                Set.Add(combination);
        }
        public void Remove(List<Card> combination) 
        {
            if (Set.Contains(combination))
                Set.Remove(combination);
        }
    }
}

namespace Cribbage.FifteenCount
{
    public class Combinations
    {
        public Combinations() { }
        public ISet<ISet<Card>> Set { get; init; } = new HashSet<ISet<Card>>();
        public void Add(ISet<Card> combination) 
        {
            if (!Set.Contains(combination)) 
                Set.Add(combination);
        }
        public void Remove(ISet<Card> combination) 
        {
            if (Set.Contains(combination))
                Set.Remove(combination);
        }
    }
}

namespace Cribbage.FifteenCount
{
    public class Combinations
    {
        public Combinations() { }
        public ISet<ISet<Card>> Set { get; init; } = new HashSet<ISet<Card>>();
        public void Add(ISet<Card> combination) 
        {
            // IF there is any combination in the set where...
            // All of the members of the set are also in 'combination'
            // THEN do not add 'combination' to the set
            if (!Set.Any(o => o.All(p => combination.Any(r => r.CompareTo(p) == 0)))) 
                Set.Add(combination);
        }
        public void Remove(ISet<Card> combination) 
        {
            ISet<Card>? cards = Set.SingleOrDefault(o => o.All(p => combination.Any(r => r.CompareTo(p) == 0)));
            if (cards != null) 
            {
                Set.Remove(cards);
            }
        }
    }
}

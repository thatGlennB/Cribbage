namespace Cribbage.FifteenCount
{
    public class Combinations
    {
        private Combinations() { }
        private static readonly object _lock = new object();
        private static Combinations _instance = null!;
        public static Combinations Instance 
        {
            get
            {
                lock(_lock)
                    {
                        if (_instance == null) 
                        {
                            _instance = new Combinations();
                        }
                        return _instance;
                    }
            }
        }
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

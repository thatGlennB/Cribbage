namespace Cribbage.Model
{
    public class ValidCombinations
    {
        public ISet<Combination> Set { get; init; } = new HashSet<Combination>();
        public void Add(Combination combination)
        {
            if (!Set.Contains(combination)) Set.Add(combination);
        }
        public void Remove(Combination combination)
        {
            if (Set.Contains(combination))
            {
                Set.Remove(combination);
            }
        }
        public void Remove(int index)
        {
            foreach (Combination combo in Set)
            {
                if (combo.Index == index)
                {
                    Set.Remove(combo);
                }
            }
        }
    }
}

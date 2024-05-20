namespace Cribbage.FifteenCount
{
    public enum Mode : ushort
    {
        FIFTEENS = 0, RUNS = 1, PAIRS = 2
    }
    internal static class CalculationMode
    {
        static internal bool ExtensionCondition(this Node node, Card card)
        {
            switch (node._mode)
            {
                case Mode.FIFTEENS:
                    return node._combination.Select(o => o.Value).Sum() + card.Value < 15;
                case Mode.RUNS:
                    return node._combination.Any(o => o.Rank == card.Rank + 1);
                case Mode.PAIRS:
                    return false;
                default:
                    throw new NotImplementedException();
            }
        }
        static internal bool SuccessCondition(this Node node, Card card)
        {
            switch (node._mode)
            {
                case Mode.FIFTEENS:
                    return node._combination.Select(o => o.Value).Sum() + card.Value == 15;
                case Mode.RUNS:
                    return 
                        node._combination.Any(o => o.Rank == card.Rank + 1) &&
                        node._combination.Any(o => o.Rank == card.Rank + 2) &&
                        !node._cards.Any(o => o.Rank == card.Rank - 1);
                case Mode.PAIRS:
                    return node._combination.Any(o => o.Rank == card.Rank);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

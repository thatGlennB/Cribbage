namespace Cribbage.Model.CombinationTree
{
    public class Node : AbstractNode
    {
        public readonly ISet<Card> Combination;

        public Card Card { get => Combination.Last(); }

        public Node(ISet<Card> combination, Combinations combinations) : base(combinations)
        {
            Combination = combination;
            Regenerate();
        }

        protected override void _createAddedCardNodes()
        {
            IEnumerable<Card> cardsToAdd = Cards.Where(o =>
                !ChildNodes.Select(p => p.Card).Contains(o) && Card.CompareTo(o) > 0);
            foreach (Card card in cardsToAdd)
            {
                _newNode(card);
            }
        }
    }
}

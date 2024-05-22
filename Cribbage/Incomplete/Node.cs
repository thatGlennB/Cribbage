using Cribbage.Incomplete;
using Cribbage.Model.Enums;
using Cribbage.Model.Utilities;

namespace Cribbage.Model
{
    public class Node : IDisposable
    {
        public readonly ISet<Card> Combination;
        public readonly int Index;
        public ISet<Node> Children { get; }
        public ISet<Card> Cards { get; private set; } = new HashSet<Card>();

        public Card Card { get => Combination.Last(); }

        public Node(ISet<Card> cards, ISet<Card> combination, ValidCombinations combinations)
        {
            Combination = combination;
            Index = Indexer.Next;
            Children = new HashSet<Node>();

            GenerateChildren(cards);

            // TODO: move this to controller
            _combinations = combinations;
            CalculateValidCombinations();
        }


        // If available card set changes, root node calls Regenerate for each child node.
        // process propagates to successive children
        public void GenerateChildren(ISet<Card> cards)
        {
            // update private cards set
            Cards = cards;

            // for each child, check if associated card is still available
            // if card is not available, dispose of child
            // else, invoke 'Regenerate' method of child
            foreach (Node child in Children)
            {
                if (!cards.Contains(child.Card))
                {
                    child.Dispose();
                    Children.Remove(child);
                }
                else
                {
                    child.GenerateChildren(cards);
                }
            }

            // for each card, check if there is an associated child.
            // if no associated child, check if child should be created
            foreach (Card card in cards)
            {
                // if the card value is less than the current card (to avoid duplicate combinations)
                // and if the card is not the current node's card...
                if (!Children.Select(o => o.Card).Contains(card) &&
                    Card.CompareTo(card) > 0 &&
                    Card != card)
                {
                    // create new node
                    Children.Add(new Node(cards, Combination.Append(card).ToHashSet(), _combinations));
                }
            }
        }








        // ******  ******   TODO - Move all this to controller  ******  ******

        // TODO: move calculation to controller
        private readonly ValidCombinations _combinations;

        public void CalculateValidCombinations()
        {
            ValidityTest test = ValidityTest.Instance;
            foreach (Mode mode in test.Calculations.Keys)
            {
                if (test.Calculations[mode](Cards, Combination))
                {
                    _combinations.Add(new Combination(Index, mode, Combination));
                }
            }
        }

        // TODO: move all this to controller, do not implement IDispose
        public void Dispose()
        {

            // TODO: move to controller
            // remove combinations associated with this node
            _combinations.Remove(Index);

            // propagate Dispose method to children.
            foreach (Node child in Children)
            {
                child.Dispose();
                Children.Remove(child);
            }
        }
    }
}

namespace Cribbage.Model.CombinationTree
{
    internal class RootNode : AbstractNode, IObserver<Card?>
    {
        internal RootNode(ISet<Card> cards, ISet<Card> discard) : base()
        {
            Cards = cards;
            Hand = cards;
            Discard = discard;
            AllNodes = new HashSet<Node>();
            Regenerate();
        }
        internal readonly ISet<Node> AllNodes;
        internal ISet<Card> Cards;
        internal readonly ISet<Card> Discard;
        internal readonly ISet<Card> Hand;

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Card? value)
        {
            if (value == null)
            {
                Cards = Hand;
            }
            else
            {
                Cards = Hand.Append(value).ToHashSet();
            }
            Regenerate();
        }

        protected override void CreateAddedCardNodes()
        {
            foreach (Card card in Cards) 
            {
                AddNode(new HashSet<Card> { card }, this);
            }
        }

        protected override void DestroyRemovedCardNodes()
        {
            foreach (Node node in ChildNodes) 
            {
                if (!this.Cards.Contains(node.Card)) 
                {
                    ChildNodes.Remove(node);
                    this.AllNodes.Remove(node);
                }
            }
        }
    }
}

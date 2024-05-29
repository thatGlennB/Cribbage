using Cribbage.HelperTypes;

namespace Cribbage.Model.CombinationTree
{
    internal class RootNode : AbstractNode, IObserver<DrawCardEventArgs>
    {
        internal RootNode(Cards cards) : base()
        {
            Cards = cards;
            Subscription = cards.Subscribe(this);
            AllNodes = new HashSet<Node>();
            Points = new Points(0, 0, 0, []);
            Regenerate();
        }
        internal readonly ISet<Node> AllNodes;
        internal readonly Cards Cards;
        internal readonly Points Points;
        private IDisposable Subscription { get; set; }

        public void OnCompleted()
        {
            Subscription.Dispose();
            throw new NotImplementedException();
        }
        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }
        public void OnNext(DrawCardEventArgs value)
        {
            if (value.PrecedingDraw != null)
            {
                foreach (Node node in AllNodes.Where(o => o.Combination.Contains(value.PrecedingDraw)))

                {
                    node.IsDeleted = true;
                    AllNodes.Remove(node);
                }
                RemoveDeletedChildren();
            }
            if (value.NextDraw != null)
            {
                CreateAddedCardNodes();
            }
            foreach (Node node in ChildNodes)
            {
                node.Regenerate();
            }
        }

        protected override void CreateAddedCardNodes()
        {
            foreach (Card card in Cards.HandAndDraw)
            {
                AddNode(card, this);
            }
        }
    }
}

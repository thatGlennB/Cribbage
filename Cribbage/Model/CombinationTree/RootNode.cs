namespace Cribbage.Model.CombinationTree
{
    public class RootNode : AbstractNode, IObserver<ISet<Card>>
    {
        public RootNode(Combinations combinations) : base(combinations)
        {
            // create nodes for each available card
            foreach (Card card in Cards)
            {
                _newNode(card);
            }
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(ISet<Card> value)
        {
            Regenerate();
        }
    }
}

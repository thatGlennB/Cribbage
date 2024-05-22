namespace Cribbage.Model.Utilities
{
    public class Indexer
    {
        #region Singleton pattern
        private Indexer() { }
        private static readonly Lazy<Indexer> _s = new Lazy<Indexer>(() => new Indexer());
        protected int _count { get; set; } = 0;
        #endregion
        public static int Next
        {
            get => ++_s.Value._count;
        }
    }
}

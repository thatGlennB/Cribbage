using Cribbage.Model.CombinationTree;
using M = Cribbage.Model;

namespace Cribbage.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            RootNode selection = new(Enumerable.Empty<M.Card>().ToHashSet(), Enumerable.Empty<M.Card>().ToHashSet());
        }
    }
}
using Cribbage.Model;

namespace Cribbage.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Selection selection = new Selection(Enumerable.Empty<Card>().ToHashSet(), Enumerable.Empty<Card>().ToHashSet());
        }
    }
}
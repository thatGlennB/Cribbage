using Cribbage.Model;
using Cribbage.Model.Enums;

namespace Cribbage.Incomplete
{
    public class ValidityTest
    {
        #region Singleton Implementation
        private ValidityTest() { }
        private static readonly Lazy<ValidityTest> _s = new Lazy<ValidityTest>(() => new ValidityTest());
        public static ValidityTest Instance
        {
            get => _s.Value;
        }
        #endregion

        public delegate bool Calculation(ISet<Card> cards, ISet<Card> combination);
        public IDictionary<Mode, Calculation> Calculations = new Dictionary<Mode, Calculation>();
    }
}

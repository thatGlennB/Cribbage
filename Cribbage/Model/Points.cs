namespace Cribbage.Model
{
    internal class Points
    {
        public int Hand { get; set; }
        public int Discard { get; set; }
        public int HatValue { get; set; }
        public List<int> DrawValues { get; set; }
        internal Points(int hand, int discard, int hatValue, List<int> drawValues) 
        {
            Hand = hand;
            Discard = discard;
            HatValue = hatValue;
            DrawValues = drawValues;
        }
    }
}

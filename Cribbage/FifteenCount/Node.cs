namespace Cribbage.FifteenCount
{
    public interface IComposite
    {
        List<int[]> GetCombinations();
        bool HasEndpoint();
    }
    public class EndPoint : IComposite 
    {
        public EndPoint(int index) => _index = index;
        private int _index;
        public List<int[]> GetCombinations() => [[_index]];
        public bool HasEndpoint() => true;
    }
    public class Node : IComposite
    {
        private readonly List<int> _residue;
        private readonly int _index;
        private readonly int _sum;
        private List<IComposite> _children;

        public Node(List<int> residue, int index, int sum)
        {
            _residue = residue;
            _index = index;
            _sum = sum;
        }

        public List<int[]> GetCombinations() 
        {
            List<int[]> output = new();
            foreach (IComposite child in _children) 
            {
                if (child.HasEndpoint()) 
                {
                    foreach (int[] combination in child.GetCombinations()) 
                    {
                        int[] newCombination = new int[combination.Length + 1];
                        combination.CopyTo(newCombination, 0);
                        newCombination[combination.Length] = _index;
                        output.Add(newCombination);
                    }
                }
            }
            return output;
        }

        public bool HasEndpoint() 
        {
            foreach (IComposite child in _children) 
            {
                if (child.HasEndpoint())
                    return true;
            }
            return false;
        }

        public void Generate() 
        {
            for(int i = 0; i < _residue.Count; i++) 
            {
                int newIndex = _index + i + 1;
                int newSum = _residue[i] + _sum;
                if (newSum < 15 && i < _residue.Count - 1) 
                {
                    List<int> newResidue = _residue.Slice(i + 1, _residue.Count - i - 1);
                    Node newNode = new Node(newResidue, newIndex, newSum);
                    newNode.Generate();
                    _children.Add(newNode);
                }
                else if (newSum == 15) 
                {
                    _children.Add(new EndPoint(newIndex));
                }
            }
        }
    }
}

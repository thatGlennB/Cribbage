namespace Cribbage.FifteenCount
{
    public interface IComposite
    {
        List<int[]> GetCombinations();
        bool HasEndpoint();
    }
    public interface INonTerminatingNode
    {
        void Append(int residueElement);
        void Generate();
    }
    public class EndPoint : IComposite
    {
        public EndPoint(int index) => _index = index;
        private int _index;
        public List<int[]> GetCombinations() => [[_index]];
        public bool HasEndpoint() => true;
    }
    // TODO find a better name than "residue".
    public class Node : IComposite, INonTerminatingNode
    {
        private readonly int _index;
        private readonly int _sum;
        private List<IComposite> _children;
        private List<int> _residue;

        public Node(List<int> residue, int index, int sum)
        {
            _index = index;
            _sum = sum;
            _children = new();
            _residue = residue;
        }
        public Node(List<int> residue) : this(residue, 0, 0){ }

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

        public void Generate()
        {
            for (int i = 0; i < _residue.Count; i++)
            {
                IComposite? newNode = _createNode(i);
                if(newNode != null) 
                {
                    _children.Add(newNode);
                }
            }
        }
        public void Append(int residueElement)
        {
            for (int i = 0; i < _residue.Count; i++)
            {
                if (_residue[i] > residueElement && _children[i].GetType().GetInterface(nameof(INonTerminatingNode)) != null)
                {
                    ((INonTerminatingNode)_children[i]).Append(residueElement);
                }
                else 
                {
                    _residue.Insert(i, residueElement);
                    _createNode(i);
                    break;
                }
            }
        }

        private IComposite? _createNode(int residueIndex) 
        {
            int newIndex = 1 + _index + residueIndex;
            int newSum = _residue[residueIndex] + _sum;
            if (newSum < 15 && residueIndex < _residue.Count - 1)
            {
                List<int> newResidue = _residue.Slice(residueIndex + 1, _residue.Count - residueIndex - 1);
                return new Node(newResidue, newIndex, newSum);
            }
            else if (newSum == 15)
            {
                return new EndPoint(newIndex);
            }
            else return null;
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
    }
}

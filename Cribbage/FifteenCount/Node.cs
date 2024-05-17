namespace Cribbage.FifteenCount
{
    public interface IComposite
    {
        List<int[]> GetCombinations();
        bool HasEndpoint();
        IComposite Copy();
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
        public IComposite Copy() => new EndPoint(_index);
    }
    // TODO find a better name than "residue".
    public class Node : IComposite, INonTerminatingNode
    {
        private readonly int _sum;
        private int _index;
        private List<IComposite> _children;
        private List<int> _residue;

        public IComposite Copy() 
        {
            int[] copyResidue = new int[_residue.Count()];
            _residue.CopyTo(copyResidue);
            return new Node(copyResidue.ToList(), _index, _sum);
        }

        public Node(List<int> residue, int index, int sum)
        {
            _index = index;
            _sum = sum;
            _children = new();
            _residue = residue;
            _residue.Sort();
            Generate();
        }
        public Node(List<int> residue) : this(residue, -1, 0){ }

        public List<int[]> GetCombinations()
        {
            List<int[]> output = new();
            foreach (IComposite child in _children)
            {
                if (child.HasEndpoint())
                {
                    foreach (int[] combination in child.GetCombinations())
                    {
                        // TODO refactor
                        if (_index >= 0)
                        {
                            int[] newCombination = new int[combination.Length + 1];
                            combination.CopyTo(newCombination, 0);
                            newCombination[combination.Length] = _index;
                            output.Add(newCombination);
                        }
                        else
                        {
                            output.Add(combination);
                        }
                    }
                }
            }
            return output;
        }

        public void Generate()
        {
            _children.Clear();
            _residue.Sort();
            _residue.Reverse();
            for (int i = 0; i < _residue.Count; i++)
            {
                IComposite? newNode = _createNode(i);
                if(newNode != null) 
                {
                    if (!newNode.IsTerminating()) 
                    {
                        ((INonTerminatingNode)newNode).Generate();
                    }
                    _children.Add(newNode);
                }
            }
        }
        public void Append(int residueElement)
        {
            _residue.Add(residueElement);
            Generate();
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
    public static class NodeUtil
    {
        public static bool IsTerminating(this IComposite node) 
        {
            return node.GetType().GetInterface(nameof(INonTerminatingNode)) == null;
        }
    }
}

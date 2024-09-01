#pragma warning disable CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type
namespace BFS;

internal class Program
{
    unsafe class Node
    {
        public Node* Parent;
        public string? Data { get; set; }
        public Node* Left { get; set; }
        public Node* Right { get; set; }

        public Node(string? data, Node* parent)
        {
            Data = data;
            Left = null;
            Right = null;
            Parent = parent;
        }
    }

    unsafe class Tree
    {
        private Node* root = null;

        public bool Insert(string? info)
        {
            if (root == null)
            {
                root = (Node*)System.Runtime.InteropServices.Marshal.AllocHGlobal(sizeof(Node));
                *root = new Node(info, null);
                return true;
            }

            return RealInsert(info, root, null);
        }

        private bool RealInsert(string? info, Node* node, Node* parent)
        {
            if (node == null)
            {
                node = (Node*)System.Runtime.InteropServices.Marshal.AllocHGlobal(sizeof(Node));
                *node = new Node(info, parent);
                return true;
            }

            if (info == null) return false;

            if (info[0] <= node->Data[0])
            {
                if (node->Left == null)
                {
                    node->Left = (Node*)System.Runtime.InteropServices.Marshal.AllocHGlobal(sizeof(Node));
                    *node->Left = new Node(info, node);
                }
                else
                {
                    RealInsert(info, node->Left, node);
                }
            }
            else
            {
                if (node->Right == null)
                {
                    node->Right = (Node*)System.Runtime.InteropServices.Marshal.AllocHGlobal(sizeof(Node));
                    *node->Right = new Node(info, node);
                }
                else
                {
                    RealInsert(info, node->Right, node);
                }
            }

            return true;
        }

        public void PrintTree()
        {
            PrintTreeReal(root);
        }

        private void PrintTreeReal(Node* node)
        {
            if (node == null) return;
            PrintTreeReal(node->Left);
            PrintTreeReal(node->Right);
            Console.WriteLine(node->Data);
        }
    }

    public static void Main(string[] args)
    {
        Tree tree = new Tree();

        tree.Insert("hello");
        tree.Insert("world");
        tree.Insert("world2");
        tree.Insert("world3");

        tree.PrintTree();
    }
}

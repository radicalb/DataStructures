using System;
using System.Collections.Generic;
/// <summary>
/// My implementation of BST. It Has two classes. Node & BSTree.
/// </summary>
namespace BinarySearchTree
{
    class Program
    {
        static void Main(string[] args)
        {

            BSTree bst = new BSTree(new Node(20));
            bst.Insert(15);
            bst.Insert(25);
            bst.Insert(18);
            bst.Insert(10);
            bst.Insert(new Node(19));
            bst.Insert(new Node(16));
            bst.Insert(new Node(17));
            /*
                  20
                 /  \
                15  25
               /  \
              10  18
                 /  \
                16  19
                  \
                  17

            */

            bst.PrintTraversedTree(BSTree.TreeTraversalForm.BreathFirstSearch);
            //bst.Delete(15);
            bst.Delete(20);

            /*
                 20
                /  \
               16  25
              /  \
             10  18
                /  \
               17  19

           */

            bst.PrintTraversedTree(BSTree.TreeTraversalForm.BreathFirstSearch);

            Console.WriteLine("Searching for node...");
            Node devetnajst = bst.BinarySearch(19);
            if (devetnajst != null)
                Console.WriteLine($"Node value: {devetnajst.Value}");
            else
                Console.WriteLine("Node not found!");

            Console.WriteLine("Searching for node...");
            Node stNiVDrevesu = bst.BinarySearch(23);
            if(stNiVDrevesu!=null)
                Console.WriteLine($"Node value: {stNiVDrevesu.Value}");
            else
                Console.WriteLine("Node not found!");

            Console.WriteLine("Searching for node...");
            Node someNode = bst.BinarySearchI(17);
            if (someNode != null)
                Console.WriteLine($"Node value: {someNode.Value}");
        }
    }

    /// <summary>
    /// class Node represents nodes of the binary three.
    /// </summary>
    class Node
    {
        public Node(int value)
        {
            this.Value = value;
        }
        public int Value { get; set; }
        public Node Left { get; set; } = null;
        public Node Right { get; set; } = null;

        /// <summary>
        /// Returns true if node Is Leaf
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool IsLeaf()
        {
            if (this.Left == null && this.Right == null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns true if this Is Left child of parent
        /// </summary>
        /// <param name="node"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public bool IsLeftChildOf(Node parent)
        {
            if (this == parent.Left)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns true if this Is Right child of parent
        /// </summary>
        /// <param name="node"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public bool IsRightChildOf(Node parent)
        {
            if (this == parent.Right)
                return true;
            else
                return false;
        }

        /// <summary>
        /// return true if node Has only one child
        /// </summary>
        /// <returns></returns>
        public bool HasOnlyOneChild()
        {
            if (!this.IsLeaf() && (this.Left == null || this.Right == null))
                return true;
            else
                return false;
        }

    }

    /// <summary>
    /// class BSTree represent Binary Search Tree. 
    /// </summary>
    class BSTree
    {
        Node _root=null; // tree root
        int[] _treeTraversal; //three traversal -- dynamic programing
        int _nodeCounter=0; //nr of nodes - used to declare _treeTraversal size
        int _treeTraversalIndex = 0; //used to position node in array _treeTraversal
        int _currentTraverseForm = -1; //if -1 -> no valid traverse of tree 

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="root"></param>
        public BSTree(Node root) {
            _root = root;
            _nodeCounter++;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rootValue"></param>
        public BSTree(int rootValue)
        {
            _root = new Node(rootValue);
            _nodeCounter++;
        }

        private void InvalidateTraversal()
        {
            _currentTraverseForm = -1;
        }

        /// <summary>
        /// Insert value into Tree
        /// </summary>
        /// <param name="value"></param>
        public void Insert(int value)
        {
            Insert(new Node(value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public void Insert(Node node) {
            Insert(node,null);
        }

        /// <summary>
        /// Insert Node into tree
        /// </summary>
        /// <param name="node">Node to be inserted.</param>
        /// <param name="root"></param>
        private void Insert(Node node, Node root=null)
        {
            if (root == null)
            {
                root = _root; // if no root Is specified use tree root
            }

            if (node.Value == root.Value)
            {
                Console.WriteLine($"Unable to insert! Value {node.Value} allready exIst!");
                return;
            }

            if (node.Value<root.Value)
            {
                if (root.Left == null)
                {
                    root.Left = node;
                    _nodeCounter++;
                    InvalidateTraversal(); // when you insert new node current stored traverse Is not valid anymore
                }
                else
                {
                    Insert(node, root.Left);
                }
            }
            else
            {
                if (root.Right == null)
                {
                    root.Right = node;
                    _nodeCounter++;
                    InvalidateTraversal();
                }
                else
                {
                    Insert(node, root.Right);
                }
            }
 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Node BinarySearch(int value)
        {
            return BinarySearch(value, null);
        }

        /// <summary>
        /// Binary Search throught the tree for value - recursive
        /// </summary>
        /// <param name="value">searched value</param>
        /// <param name="root"></param>
        /// <returns>Node if found, otherwIse returns null</returns>
        private Node BinarySearch(int value, Node root = null)
        {
            if (root == null)
            {
                root = _root;
            }

            if (value == root.Value)
            {
                return root;
            }

            if (value < root.Value)
            {
                if (root.Left != null)
                {
                    return BinarySearch(value, root.Left);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (root.Right != null)
                {
                    return BinarySearch(value, root.Right);
                }
                else
                {
                    return null;
                }
            }


        }

        /// <summary>
        /// Binary Search Iterative
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Node BinarySearchI(int value)
        {
            Node node = _root;

            for (int i = 0; i < _nodeCounter;i++) {
                if (value == node.Value)
                {
                    return node;
                }
                else if (value < node.Value && node.Left != null)
                {
                    node = node.Left;
                }
                else if (node.Right != null)
                {
                    node = node.Right;
                }
                else
                {
                    Console.WriteLine("Value not found!");
                    break;
                }

            }
            return null;
        }



        /// <summary>
        /// get Next inorder - FindLeftmostDescendent from Right subtree
        /// </summary>
        /// <param name="root"></param>
        public Node GetNextInorder(Node node)
        {

            return FindLeftmostDescendent(node.Right);
        }

        /// <summary>
        /// get smallest from from root - most Left in subtree or tree
        /// </summary>
        /// <param name="root"></param>
        private Node FindLeftmostDescendent(Node root)
        {

            while (root.Left != null)
            {
                root = root.Left;
            }

            return root;
        }

        /// <summary>
        /// Deletes the node using value and binary search
        /// </summary>
        /// <param name="value"></param>
        /// <param name="root"></param>
        public void Delete(int value, Node root = null)
        {
            Delete(BinarySearch(value));
        }

        public void Delete(Node node)
        {
            Delete(node, null);
        }

        /// <summary>
        /// Deletes the node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="root"></param>

        public void Delete(Node node, Node root = null)
        {
            if (node == null) {
                throw new System.ArgumentException("Parameter cannot be null", "original");
                return;
            }

            if (root == null)
            {
                root = _root;
                Console.WriteLine($"Deleting node: {node.Value}");
            }

            //if node Is child of root->we found parents of child 
            if ((node.IsLeftChildOf(root) || node.IsRightChildOf(root)) || root.Value==node.Value) 
            {
                if (node.IsLeaf()) // if Is Leaf just remove it - remove reference at parrent
                {
                    if (node.IsLeftChildOf(root))
                        root.Left = null;
                    else
                        root.Right = null;

                    InvalidateTraversal();
                    _nodeCounter--;
                }
                else if (node.HasOnlyOneChild()) // if only one child replace node with child
                {
                    if (node.Left == null)
                    {
                        node.Value = node.Right.Value;
                        node.Left = node.Right.Left;
                        node.Right = node.Right.Right;
                    }
                    else
                    {
                        node.Value = node.Left.Value;
                        node.Left = node.Left.Left;
                        node.Right = node.Left.Right;
                    }

                    InvalidateTraversal();
                    _nodeCounter--;

                }
                else //else replace node with next in-order.
                {
                    Node tmpNode = GetNextInorder(node);
                    node.Value = tmpNode.Value;
                    Delete(tmpNode, node);

                    InvalidateTraversal();
                }

            }
            else // else we need to dig deeper to the Left or Right
            {
                if (root.Left != null && node.Value < root.Value)
                    Delete(node, root.Left);
                else if(root.Right!=null)
                    Delete(node, root.Right);
            }
            
        }

        /// <summary>
        /// enum specifaing posible tree traversal forms
        /// DFS - Depth-first search
        /// </summary>
        public enum TreeTraversalForm
        {
            DFSpreorder,
            DFSinorder,
            DFSoutorder,
            DFSpostorder,
            BreathFirstSearch
        };

        /// <summary>
        /// Maps the tree traversal form with appropriate method
        /// </summary>
        /// <param name="traversalForm"></param>
        /// <returns></returns>
        public int[] TraverseTree(TreeTraversalForm traversalForm)
        {

            //if tree Is already traversed -> dont do it again
            if ((int)traversalForm != _currentTraverseForm)
            {
                switch (traversalForm)
                {
                    case TreeTraversalForm.DFSinorder:
                        this.Inorder();
                        break;
                    case TreeTraversalForm.DFSoutorder:
                        this.Outorder();
                        break;
                    case TreeTraversalForm.DFSpostorder:
                        this.Postorder();
                        break;
                    case TreeTraversalForm.DFSpreorder:
                        this.Preorder();
                        break;
                    case TreeTraversalForm.BreathFirstSearch:
                        this.BreathFirstSearch();
                        break;
                    default:
                        throw new System.InvalidOperationException("Unknown traversal form!");
                        break;
                }
            }

            return (int[])_treeTraversal.Clone();
        }

        /// <summary>
        /// Prints traversed tree to Console
        /// </summary>
        /// <param name="traversalForm"></param>
        public void PrintTraversedTree(TreeTraversalForm traversalForm)
        {

            //if tree Is already traversed -> dont do it again
            if ((int)traversalForm != _currentTraverseForm)
            {
                this.TraverseTree(traversalForm);
            }

            Console.Write(traversalForm.ToString() + ": ");
            foreach (int val in _treeTraversal)
            {
                Console.Write($"{val} ");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Creates DFS - Pre-order traverse and stors it in _treeTraversal
        /// </summary>
        /// <param name="root"></param>
        void Preorder(Node root = null)
        {
            if (root == null)
            {
                root = _root;
                _treeTraversal = new int[_nodeCounter];
                _treeTraversalIndex = 0;
                _currentTraverseForm = (int)TreeTraversalForm.DFSpreorder;
            }

            _treeTraversal[_treeTraversalIndex] = root.Value;
            _treeTraversalIndex++;

            if (root.Left != null)
                Preorder(root.Left);

            if (root.Right != null)
                Preorder(root.Right);
        }

        /// <summary>
        /// Creates DFS - In-order traverse and stors it in _treeTraversal
        /// </summary>
        /// <param name="root"></param>
        void Inorder(Node root = null)
        {
            if (root == null)
            {
                root = _root;
                _treeTraversal = new int[_nodeCounter];
                _treeTraversalIndex = 0;
                _currentTraverseForm = (int)TreeTraversalForm.DFSinorder;
            }


            if (root.Left != null)
                Inorder(root.Left);

            _treeTraversal[_treeTraversalIndex] = root.Value;
            _treeTraversalIndex++;

            if (root.Right != null)
                Inorder(root.Right);
        }

        /// <summary>
        /// Creates DFS - Post-order traverse and stors it in _treeTraversal
        /// </summary>
        /// <param name="root"></param>
        void Postorder(Node root = null)
        {
            if (root == null)
            {
                root = _root;
                _treeTraversal = new int[_nodeCounter];
                _treeTraversalIndex = 0;
                _currentTraverseForm = (int)TreeTraversalForm.DFSpostorder;
            }


            if (root.Left != null)
                Postorder(root.Left);

            if (root.Right != null)
                Postorder(root.Right);

            _treeTraversal[_treeTraversalIndex] = root.Value;
            _treeTraversalIndex++;
        }

        /// <summary>
        /// Creates DFS - Out-order traverse and stors it in _treeTraversal
        /// </summary>
        /// <param name="root"></param>
        void Outorder(Node root = null)
        {
            if (root == null)
            {
                root = _root;
                _treeTraversal = new int[_nodeCounter];
                _treeTraversalIndex = 0;
                _currentTraverseForm = (int)TreeTraversalForm.DFSoutorder;
            }


            if (root.Right != null)
                Outorder(root.Right);

            _treeTraversal[_treeTraversalIndex] = root.Value;
            _treeTraversalIndex++;

            if (root.Left != null)
                Outorder(root.Left);

        }

        /// <summary>
        /// Creates BFS - BreathFirstSearch traverse and stors it in _treeTraversal
        /// </summary>
        /// <param name="root"></param>
        void BreathFirstSearch(Node root = null)
        {
            if (root == null)
            {
                root = _root;
                _treeTraversal = new int[_nodeCounter];
                _treeTraversalIndex = 0;
                _currentTraverseForm = (int)TreeTraversalForm.BreathFirstSearch;

                _treeTraversal[_treeTraversalIndex] = root.Value;
                _treeTraversalIndex++;
            }

            

            if (root.Left != null)
            {
                _treeTraversal[_treeTraversalIndex] = root.Left.Value;
                _treeTraversalIndex++;
            }
                
            if (root.Right != null)
            {
                _treeTraversal[_treeTraversalIndex] = root.Right.Value;
                _treeTraversalIndex++;
            }

            if (root.Left != null)
                BreathFirstSearch(root.Left);
            if (root.Right != null)
                BreathFirstSearch(root.Right);
        }
    }
}

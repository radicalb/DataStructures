using System;
using System.Collections.Generic;
/// <summary>
/// My implementation of BST. It has two classes. Node & BSTree.
/// </summary>
namespace BinarySearchTree
{
    class Program
    {
        static void Main(string[] args)
        {
            //Random rnd = new Random();
            //BSTree bst = new BSTree(new Node(rnd.Next(0,100)));
            //bst.Insert(new Node(rnd.Next(0, 100)));

            BSTree bst = new BSTree(new Node(20));
            bst.Insert(new Node(15));
            bst.Insert(new Node(25));
            bst.Insert(new Node(18));
            bst.Insert(new Node(10));
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

            Console.WriteLine("Searching for node...");
            Node devetnajst = bst.BinarySearch(19);
            if (devetnajst != null)
                Console.WriteLine($"Node value: {devetnajst.value}");
            else
                Console.WriteLine("Node not found!");

            Console.WriteLine("Searching for node...");
            Node stNiVDrevesu = bst.BinarySearch(23);
            if(stNiVDrevesu!=null)
                Console.WriteLine($"Node value: {stNiVDrevesu.value}");
            else
                Console.WriteLine("Node not found!");
        }
    }

    /// <summary>
    /// class Node represents nodes of the binary three.
    /// </summary>
    class Node
    {
        public Node(int value)
        {
            this.value = value;
        }
        public int value { get; set; }
        public Node left { get; set; } = null;
        public Node right { get; set; } = null;

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

        public BSTree(Node root) {
            _root = root;
            _nodeCounter++;
        }

        /// <summary>
        /// Insert Node into tree
        /// </summary>
        /// <param name="node">Node to be inserted.</param>
        /// <param name="root"></param>
        public void Insert(Node node, Node root=null)
        {
            if (root == null)
            {
                root = _root; // if no root is specified use tree root
            }

            if (node.value == root.value)
            {
                Console.WriteLine($"Unable to insert! Value {node.value} allready exist!");
                return;
            }

            if (node.value<root.value)
            {
                if (root.left == null)
                {
                    root.left = node;
                    _nodeCounter++;
                    _currentTraverseForm = -1; // when you insert new node current stored traverse is not valid anymore
                }
                else
                {
                    Insert(node, root.left);
                }
            }
            else
            {
                if (root.right == null)
                {
                    root.right = node;
                    _nodeCounter++;
                    _currentTraverseForm = -1;
                }
                else
                {
                    Insert(node, root.right);
                }
            }
 
        }

        /// <summary>
        /// Binary Search throught the tree for value
        /// </summary>
        /// <param name="value">searched value</param>
        /// <param name="root"></param>
        /// <returns>Node if found, otherwise returns null</returns>
        public Node BinarySearch(int value, Node root = null)
        {
            if (root == null)
            {
                root = _root;
            }

            if (value == root.value)
            {
                return root;
            }

            if (value < root.value)
            {
                if (root.left != null)
                {
                    return BinarySearch(value, root.left);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (root.right != null)
                {
                    return BinarySearch(value, root.right);
                }
                else
                {
                    return null;
                }
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

            //if tree is already traversed -> dont do it again
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
                        Console.WriteLine("Unknown form!");
                        break;
                }
            }

            return _treeTraversal;
        }

        /// <summary>
        /// Prints traversed tree to Console
        /// </summary>
        /// <param name="traversalForm"></param>
        public void PrintTraversedTree(TreeTraversalForm traversalForm)
        {

            //if tree is already traversed -> dont do it again
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

            _treeTraversal[_treeTraversalIndex] = root.value;
            _treeTraversalIndex++;

            if (root.left != null)
                Preorder(root.left);

            if (root.right != null)
                Preorder(root.right);
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


            if (root.left != null)
                Inorder(root.left);

            _treeTraversal[_treeTraversalIndex] = root.value;
            _treeTraversalIndex++;

            if (root.right != null)
                Inorder(root.right);
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


            if (root.left != null)
                Postorder(root.left);

            if (root.right != null)
                Postorder(root.right);

            _treeTraversal[_treeTraversalIndex] = root.value;
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


            if (root.right != null)
                Outorder(root.right);

            _treeTraversal[_treeTraversalIndex] = root.value;
            _treeTraversalIndex++;

            if (root.left != null)
                Outorder(root.left);

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

                _treeTraversal[_treeTraversalIndex] = root.value;
                _treeTraversalIndex++;
            }

            if (root.left != null)
            {
                _treeTraversal[_treeTraversalIndex] = root.left.value;
                _treeTraversalIndex++;
            }
                
            if (root.right != null)
            {
                _treeTraversal[_treeTraversalIndex] = root.right.value;
                _treeTraversalIndex++;
            }

            if (root.left != null)
                BreathFirstSearch(root.left);
            if (root.right != null)
                BreathFirstSearch(root.right);
        }
    }
}

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
            bst.Delete(15);

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
                Console.WriteLine($"Node value: {devetnajst.value}");
            else
                Console.WriteLine("Node not found!");

            Console.WriteLine("Searching for node...");
            Node stNiVDrevesu = bst.BinarySearch(23);
            if(stNiVDrevesu!=null)
                Console.WriteLine($"Node value: {stNiVDrevesu.value}");
            else
                Console.WriteLine("Node not found!");

            Console.WriteLine("Searching for node...");
            Node someNode = bst.BinarySearchI(17);
            if (someNode != null)
                Console.WriteLine($"Node value: {someNode.value}");
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

        /// <summary>
        /// Returns true if node is Leaf
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool isLeaf()
        {
            if (this.left == null && this.right == null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns true if this is left child of parent
        /// </summary>
        /// <param name="node"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public bool isLeftChildOf(Node parent)
        {
            if (this == parent.left)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns true if this is right child of parent
        /// </summary>
        /// <param name="node"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public bool isRightChildOf(Node parent)
        {
            if (this == parent.right)
                return true;
            else
                return false;
        }

        /// <summary>
        /// return true if node has only one child
        /// </summary>
        /// <returns></returns>
        public bool hasOnlyOneChild()
        {
            if (!this.isLeaf() && (this.left == null || this.right == null))
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

        /// <summary>
        /// Insert value into Tree
        /// </summary>
        /// <param name="value"></param>
        public void Insert(int value)
        {
            Insert(new Node(value));
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
        /// Binary Search throught the tree for value - recursive
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
        /// Binary Search Iterative
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Node BinarySearchI(int value)
        {
            Node node = _root;

            for (int i = 0; i < _nodeCounter;i++) {
                if (value == node.value)
                {
                    return node;
                }
                else if (value < node.value && node.left != null)
                {
                    node = node.left;
                }
                else if (node.right != null)
                {
                    node = node.right;
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
        /// get Next inorder - smalest from right subtree
        /// </summary>
        /// <param name="root"></param>
        public Node GetNextInorder(Node node)
        {

            return Smalest(node.right);
        }

        /// <summary>
        /// get smallest from from root - most left in subtree or tree
        /// </summary>
        /// <param name="root"></param>
        private Node Smalest(Node root)
        {

            Node minNode = root.left;

            while (root.left != null)
            {
                root = root.left;
                minNode = root;
            }

            return minNode;
        }

        /// <summary>
        /// Deletes the node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="root"></param>

        public void Delete(Node node, Node root = null)
        {
            if (node == null) {
                Console.WriteLine("Please enter valid node!");
                return;
            }

            if (root == null)
            {
                root = _root;
                Console.WriteLine($"Deleting node: {node.value}");
            }

            //if node is child of root->we found parents of child 
            if (node.isLeftChildOf(root) || node.isRightChildOf(root)) 
            {
                if (node.isLeaf()) // if is Leaf just remove it - remove reference at parrent
                {
                    if (node.isLeftChildOf(root))
                        root.left = null;
                    else
                        root.right = null;

                    _currentTraverseForm = -1;
                    _nodeCounter--;
                }
                else if (node.hasOnlyOneChild()) // if only one child replace node with child
                {
                    if (node.left == null)
                    {
                        node.value = node.right.value;
                        node.left = node.right.left;
                        node.right = node.right.right;
                    }
                    else
                    {
                        node.value = node.left.value;
                        node.left = node.left.left;
                        node.right = node.left.right;
                    }

                    _currentTraverseForm = -1;
                    _nodeCounter--;

                }
                else //else replace node with next in-order.
                {
                    Node tmpNode = GetNextInorder(node);
                    node.value = tmpNode.value;
                    Delete(tmpNode, node);

                    _currentTraverseForm = -1;
                }

            }
            else // else we need to dig deeper to the left or right
            {
                if (root.left != null && node.value < root.value)
                    Delete(node, root.left);
                else if(root.right!=null)
                    Delete(node, root.right);
            }
            
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

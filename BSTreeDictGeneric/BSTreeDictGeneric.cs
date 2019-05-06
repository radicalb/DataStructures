using System;

namespace RadicalB.DataStructures.BSTreeDictGeneric
{
    /// <summary>
    /// class BSTree represent Binary Search Tree. 
    /// </summary>
    public class BSTreeDict<T>
    {
        INode _root = null; // tree root
        int[] _treeTraversal; //tree traversal -- dynamic programing
        T[] _treeTraversalValues; //three traversal -- dynamic programing
        int _nodeCounter = 0; //nr of nodes - used to declare _treeTraversal size
        int _treeTraversalIndex = 0; //used to position node in array _treeTraversal
        int _currentTraverseForm = -1; //if -1 -> no valid traverse of tree 


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rootKey"></param>
        public BSTreeDict(int rootKey)
        {
            _root = new Node(rootKey);
            _nodeCounter++;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="root"></param>
        public BSTreeDict(int rootKey, T value)
        {
            _root = new Node(rootKey, value);
            _nodeCounter++;
        }

        private void InvalidateTraversal()
        {
            _currentTraverseForm = -1;
        }

        /// <summary>
        /// Insert key into Tree
        /// </summary>
        /// <param name="key"></param>
        public void Insert(int key, T value)
        {
            Insert(new Node(key, value));
        }

        /// <summary>
        /// Insert INode into tree
        /// </summary>
        /// <param name="node">INode to be inserted.</param>
        /// <param name="root"></param>
        private void Insert(INode node, INode root = null)
        {
            if (root == null)
            {
                root = _root; // if no root Is specified use tree root
            }

            if (node.Key == root.Key)
            {
                throw new System.ArgumentException("Unable to insert! Key allready exists!", "original");
            }

            if (node.Key < root.Key)
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
        /// <returns></returns>
        public bool Contains(int key, bool searchRecursively = false)
        {
            if (searchRecursively == true)
                return BinarySearch(key) != null ? true : false;
            else
                return BinarySearchI(key) != null ? true : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public T GetValue(int key, bool searchRecursively = false)
        {
            if (searchRecursively == true)
                return BinarySearch(key).Value;
            else
                return BinarySearchI(key).Value;
        }

        /// <summary>
        /// Binary Search throught the tree for key - recursive
        /// </summary>
        /// <param name="key">searched key</param>
        /// <param name="root"></param>
        /// <returns>INode if found, otherwIse returns null</returns>
        private INode BinarySearch(int key, INode root = null)
        {
            if (root == null)
            {
                root = _root;
            }

            if (key == root.Key)
            {
                return root;
            }

            if (key < root.Key)
            {
                if (root.Left != null)
                {
                    return BinarySearch(key, root.Left);
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
                    return BinarySearch(key, root.Right);
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
        /// <param name="key"></param>
        /// <returns></returns>
        private INode BinarySearchI(int key)
        {
            INode node = _root;

            for (int i = 0; i < _nodeCounter; i++)
            {
                if (key == node.Key)
                {
                    return node;
                }
                else if (key < node.Key && node.Left != null)
                {
                    node = node.Left;
                }
                else if (node.Right != null)
                {
                    node = node.Right;
                }

            }
            return null;
        }



        /// <summary>
        /// get Next inorder - FindLeftmostDescendent from Right subtree
        /// </summary>
        /// <param name="root"></param>
        private INode GetNextInorder(INode node)
        {

            return FindLeftmostDescendent(node.Right);
        }

        /// <summary>
        /// get smallest from from root - most Left in subtree or tree
        /// </summary>
        /// <param name="root"></param>
        private INode FindLeftmostDescendent(INode root)
        {

            while (root.Left != null)
            {
                root = root.Left;
            }

            return root;
        }

        /// <summary>
        /// Deletes the node using key and binary search
        /// </summary>
        /// <param name="key"></param>
        /// <param name="root"></param>
        public void Delete(int key, bool searchRecursively = false)
        {
            if (searchRecursively)
                Delete(BinarySearch(key));
            else
                Delete(BinarySearchI(key));
        }

        /// <summary>
        /// Deletes the node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="root"></param>

        private void Delete(INode node, INode root = null)
        {
            if (node == null)
            {
                throw new System.ArgumentException("Parameter cannot be null", "original");
            }

            if (root == null)
            {
                root = _root;
            }

            //if node Is child of root->we found parents of child 
            if ((node.IsLeftChildOf(root) || node.IsRightChildOf(root)) || root.Key == node.Key)
            {
                if (node.IsLeaf()) // if Is Leaf just remove it - remove reference at parrent
                {
                    if (node.IsLeftChildOf(root))
                    {
                        root.Left = null;
                    }
                    else
                    {
                        root.Right = null;
                    }

                    InvalidateTraversal();
                    _nodeCounter--;
                }
                else if (node.HasOnlyOneChild()) // if only one child replace node with child
                {
                    if (node.Left==null)
                    {
                        if (node.IsLeftChildOf(root))
                        {
                            root.Left = node.Right;
                        }else
                        {
                            root.Right = node.Right;
                        }
                    }
                    else
                    {
                        if (node.IsLeftChildOf(root))
                        {
                            root.Left = node.Left;
                        }
                        else
                        {
                            root.Right = node.Left;
                        }
                    }

                    InvalidateTraversal();
                    _nodeCounter--;

                }
                else //else replace node with next in-order.
                {
                    INode tmpNode = GetNextInorder(node);
                    node.Key = tmpNode.Key;
                    node.Value = tmpNode.Value;
                    Delete(tmpNode, node.Right.IsLeaf()?node:node.Right);
                }

            }
            else // else we need to dig deeper to the Left or Right
            {
                if (root.Left != null && node.Key < root.Key)
                    Delete(node, root.Left);
                else if (root.Right != null)
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
        public int[] TraverseKeys(TreeTraversalForm traversalForm)
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
                }
            }

            return (int[])_treeTraversal.Clone();
        }

        public T[] TraverseValues(TreeTraversalForm traversalForm)
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
                }
            }

            return (T[])_treeTraversalValues.Clone();
        }

        /// <summary>
        /// Return traversed tree keys as String
        /// </summary>
        /// <param name="traversalForm"></param>
        public string PrintTraversedKeys(TreeTraversalForm traversalForm)
        {

            if ((int)traversalForm != _currentTraverseForm)
            {
                this.TraverseKeys(traversalForm);
            }

            string tmpReturn = traversalForm.ToString() + ": ";
            foreach (int val in _treeTraversal)
            {
                tmpReturn += $"{val} ";
            }
            return tmpReturn;
        }

        /// <summary>
        /// Creates DFS - Pre-order traverse and stors it in _treeTraversal
        /// </summary>
        /// <param name="root"></param>
        void Preorder(INode root = null)
        {
            if (root == null)
            {
                root = _root;
                InitiateTreeTraversal(TreeTraversalForm.DFSpreorder);
            }

            AppendTreeTraversalArr(root);

            if (root.Left != null)
                Preorder(root.Left);

            if (root.Right != null)
                Preorder(root.Right);
        }


        /// <summary>
        /// Creates DFS - In-order traverse and stors it in _treeTraversal
        /// </summary>
        /// <param name="root"></param>
        void Inorder(INode root = null)
        {
            if (root == null)
            {
                root = _root;
                InitiateTreeTraversal(TreeTraversalForm.DFSinorder);
            }


            if (root.Left != null)
                Inorder(root.Left);

            AppendTreeTraversalArr(root);

            if (root.Right != null)
                Inorder(root.Right);
        }

        /// <summary>
        /// Creates DFS - Post-order traverse and stors it in _treeTraversal
        /// </summary>
        /// <param name="root"></param>
        void Postorder(INode root = null)
        {
            if (root == null)
            {
                root = _root;
                InitiateTreeTraversal(TreeTraversalForm.DFSpostorder);
            }


            if (root.Left != null)
                Postorder(root.Left);

            if (root.Right != null)
                Postorder(root.Right);

            AppendTreeTraversalArr(root);
        }


        /// <summary>
        /// Creates DFS - Out-order traverse and stors it in _treeTraversal
        /// </summary>
        /// <param name="root"></param>
        void Outorder(INode root = null)
        {
            if (root == null)
            {
                root = _root;
                InitiateTreeTraversal(TreeTraversalForm.DFSoutorder);
            }


            if (root.Right != null)
                Outorder(root.Right);

            AppendTreeTraversalArr(root);

            if (root.Left != null)
                Outorder(root.Left);

        }

        /// <summary>
        /// Creates BFS - BreathFirstSearch traverse and stors it in _treeTraversal
        /// </summary>
        /// <param name="root"></param>
        void BreathFirstSearch(INode root = null)
        {
            if (root == null)
            {
                root = _root;
                InitiateTreeTraversal(TreeTraversalForm.BreathFirstSearch);

                AppendTreeTraversalArr(root);
            }



            if (root.Left != null)
            {
                AppendTreeTraversalArr(root.Left);
            }

            if (root.Right != null)
            {
                AppendTreeTraversalArr(root.Right);
            }

            if (root.Left != null)
                BreathFirstSearch(root.Left);
            if (root.Right != null)
                BreathFirstSearch(root.Right);
        }

        private void InitiateTreeTraversal(TreeTraversalForm treeTraversalForm)
        {
            _treeTraversal = new int[_nodeCounter];
            _treeTraversalValues = new T[_nodeCounter];
            _treeTraversalIndex = 0;
            _currentTraverseForm = (int)treeTraversalForm;
        }


        private void AppendTreeTraversalArr(INode node)
        {
            _treeTraversal[_treeTraversalIndex] = node.Key;
            _treeTraversalValues[_treeTraversalIndex] = node.Value;
            _treeTraversalIndex++;
        }

        public int GetNumberOfNodes()
        {
            return _nodeCounter;
        }

        public interface INode
        {
            int Key { get; set; }
            T Value { get; set; }
            INode Left { get; set; }
            INode Right { get; set; }

            bool IsLeaf();
            bool IsLeftChildOf(INode parent);

            bool IsRightChildOf(INode parent);

            bool HasOnlyOneChild();

            INode Clone();

            void CopyTo(INode node);
        }

        /// <summary>
        /// class INode represents nodes of the binary three.
        /// </summary>
        private class Node : INode
        {
            public int Key { get; set; }
            public T Value { get; set; }
            public INode Left { get; set; } = null;
            public INode Right { get; set; } = null;

            public Node(int key)
            {
                Key = key;
            }

            public Node(int key, T value)
            {
                Key = key;
                Value = value;
            }

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
            public bool IsLeftChildOf(INode parent)
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
            public bool IsRightChildOf(INode parent)
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



            public INode Clone()
            {
                INode newINode = new Node(this.Key);
                newINode.Left = this.Left;
                newINode.Right = this.Right;
                newINode.Value = this.Value;
                return newINode;
            }

            public void CopyTo(INode node)
            {
                node.Key = this.Key;
                node.Left = this.Left;
                node.Right = this.Right;
                node.Value = this.Value;
            }

        }
    }
}

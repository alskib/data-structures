﻿// Franklin Leung
// Lab 4 - COSC 3319
// Fall 2012

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab4csharp
{
    class BinarySearchTree
    {
        private TreeNode root = new TreeNode(null);
        private TreeNode head = new TreeNode(null);
        private TreeNode P = new TreeNode(null);

        public BinarySearchTree()
        {
            head.setLTag(false);
            head.setLLink(head);
            head.setInfo(null, null);
            head.setRLink(head);
            head.setRTag(true);
        }

        public TreeNode getHead()
        {
            return head;
        }

        public TreeNode getRoot()
        {
            return root;
        }
        
        public void InsertBinarySearchTree(Customer myCustomer)
        {
            TreeNode Q = new TreeNode(myCustomer);
            bool found;

            if (!head.getLTag())
            {
                root = Q;
                root.setLLink(head);
                root.setRLink(head);
                head.setLLink(root);
                head.setLTag(true);
            }
            else
            {
                found = false;
                P = root;
                do
                {
                    if (String.Compare(myCustomer.getName(), P.getName()) < 0) // if alphabetically less
                    {
                        if (!P.getLTag()) // empty left subtree
                        {
                            Q.setAllAttr(P.getLTag(), P.getLLink(), P, false);
                            P.setLLink(Q);
                            P.setLTag(true);
                            found = true;
                        }
                        else
                        {
                            P = P.getLLink();
                        }
                    }
                    else
                    {
                        if (!P.getRTag()) // empty right subtree
                        {
                            Q.setAllAttr(false, P, P.getRLink(), P.getRTag());
                            P.setRLink(Q);
                            P.setRTag(true);
                            if (Q.getRTag())
                            {
                                TreeNode t = InOrderSuccessor(P);
                                t.setLLink(Q);
                            }
                            found = true;
                        }
                        else
                        {
                            P = P.getRLink();
                        }
                    }

                } while (!found); // stop when spot found
            }
        }

        public TreeNode BinarySearch(String name)
        {
            TreeNode P = this.getRoot();
            do
            {
                if (name == P.getName())
                    return P;
                else if (name.CompareTo(P.getName()) < 0)
                    P = InOrderPredecessor(P);
                else
                    P = InOrderSuccessor(P);
            } while (P != this.getHead());
            return null;
        }

        public TreeNode FindCustomerIterative(String name)
        {
            return BinarySearch(name);
        }

        public TreeNode FindCustomerRecursive(TreeNode searchPoint, String name)
        {
            if (name == searchPoint.getName())
                return searchPoint;
            else if (String.Compare(name, searchPoint.getName()) < 0)
            {
                if (!searchPoint.getLTag())
                    return null;
                else
                    return FindCustomerRecursive(searchPoint.getLLink(), name);
            }
            else
            {
                if (!searchPoint.getRTag())
                    return null;
                else
                    return FindCustomerRecursive(searchPoint.getRLink(), name);
            }
        }

        public TreeNode PreOrderSuccessor(TreeNode P)
        {
            TreeNode Q;
            if (P.getLTag())
                Q = P.getLLink();
            else
            {
                Q = P;
                while (!Q.getRTag())
                    Q = Q.getRLink();
                Q = Q.getRLink();
            }
            return Q;
        }

        public TreeNode InOrderPredecessor(TreeNode P)
        {
            TreeNode Q = P.getLLink();
            if (P.getLTag())
                while (Q.getRTag())
                    Q = Q.getRLink();
            return Q;
        }

        public TreeNode InOrderSuccessor(TreeNode P)
        {
            TreeNode Q = P.getRLink();
            if (P.getRTag())
                while (Q.getLTag())
                    Q = Q.getLLink();
            return Q;
        }

        public TreeNode PostOrderPredecessor(TreeNode P)
        {
            TreeNode Q = P;
            if (P.getRTag())
                Q = P.getRLink();
            else
                while (!Q.getLTag())
                    Q = Q.getLLink();
            return Q;
        }

        public void ReverseInOrder(TreeNode searchPoint) {
            if (!searchPoint.isInfoNull()) {
                if (searchPoint.getRTag())
                    ReverseInOrder(searchPoint.getRLink());
                searchPoint.visit();
                if (searchPoint.getLTag())
                    ReverseInOrder(searchPoint.getLLink());
            }
        }

        public void PreOrder(TreeNode current)
        {
            if (current.isInfoNull())
                Console.WriteLine("Empty list.");
            else
            {
                do
                {
                    current.visit();
                    current = PreOrderSuccessor(current);
                } while (current != head);
            }
        }

        public void PostOrderIterative(TreeNode current)
        {
            Stack<TreeNode> st = new Stack<TreeNode>();
            st.Push(current);
            while (st.Count != 0)
            {
                current = st.Peek();
                if (current.getLTag() && !current.getLLink().getVisited())
                    st.Push(current.getLLink());
                else
                {
                    if (current.getRTag() && !current.getRLink().getVisited())
                        st.Push(current.getRLink());
                    else
                    {
                        current.visit();
                        current.setVisited(true);
                        st.Pop();
                    }
                }
            }
        }

        public void PostOrderRecursive(TreeNode searchPoint) {
            if (!searchPoint.isInfoNull())
            {
                if (searchPoint.getLTag())
                    PostOrderRecursive(searchPoint.getLLink());
                if (searchPoint.getRTag())
                    PostOrderRecursive(searchPoint.getRLink());
                searchPoint.visit();
            }
        }

        public void DeleteTreeNode(String name, int i)
        {

        }

        public TreeNode FindParent(TreeNode searchPoint, String name)
        {
            while (true)
            {
                int num = String.Compare(name, searchPoint.getName());
                if (num == 0)
                    return searchPoint;
                else if (num > 0)
                {
                    if (name.Equals(searchPoint.getRLink().getName()))
                        return searchPoint;
                    else if (searchPoint.getRTag())
                        searchPoint = searchPoint.getRLink();
                    else
                        return null;
                }
                else
                {
                    if (name == searchPoint.getLLink().getName())
                        return searchPoint;
                    else if (searchPoint.getLTag())
                        searchPoint = searchPoint.getLLink();
                    else
                        return null;
                }
            }
        }

        public void DeleteTreeNode(String name) {
            bool deleteRoot = false;
            int dir = 0; // direction: 0 is go left, 1 is go right
            TreeNode parent;
            TreeNode temp;
        
            parent = FindParent(root, name);
            if (parent == null)
                Console.WriteLine(name + " not found. No action taken.");
            else {
                if (name == this.getHead().getLLink().getName())
                {
                    Console.WriteLine("Delete - name match found. {0}.", name);
                    deleteRoot = true;
                }
                if (String.Compare(name, parent.getName()) > 0 && !deleteRoot)
                    dir = 1;
                if (!deleteRoot)
                {
                    if (dir == 1)
                        temp = parent.getRLink();
                    else
                        temp = parent.getLLink();
                }
                else
                    temp = parent;
                    
                if (!temp.getLTag() && !temp.getRTag())
                    DeleteNodeNoChildren(deleteRoot, parent, dir);
                else if (!temp.getLTag() && temp.getRTag())
                    DeleteNodeRightChild(deleteRoot, parent, dir);
                else if (temp.getLTag() && !temp.getRTag())
                    DeleteNodeLeftChild(deleteRoot, parent, dir);
                else if (temp.getLTag() && temp.getRTag())
                    DeleteNodeBothChildren(deleteRoot, parent, dir);
                Console.WriteLine(name + " deleted.");
            }
        }

        public void DeleteNodeNoChildren(bool deleteRoot, TreeNode parent, int direction) {
            if (!deleteRoot) {
                if (direction == 1) {
                    parent.setRTag(parent.getRLink().getRTag());
                    parent.setRLink(parent.getRLink().getRLink());
                } else {
                    parent.setLTag(parent.getLLink().getLTag());
                    parent.setLLink(parent.getLLink().getLLink());
                }
            } else {
                head.setLLink(head);
                root = null;
            }
        }

        public void DeleteNodeRightChild(bool deleteRoot, TreeNode parent, int direction) {
            if (!deleteRoot) {
                if (direction == 1) {
                    parent.setRLink(parent.getRLink().getRLink());
                    if (!parent.getRLink().getLTag())
                        parent.getRLink().setLLink(parent);
                    else {
                        parent.getLLink().getRLink().setLLink(parent.getLLink().getLLink());
                        parent.setLLink(parent.getLLink().getRLink());
                    }
                } else {
                    head.setLLink(parent.getRLink());
                    root = parent.getRLink();
                    root.setLLink(null);
                }
            }
        }

        public void DeleteNodeLeftChild(bool deleteRoot, TreeNode parent, int direction) {
            if (!deleteRoot) {
                if (direction == 1) {
                    parent.getRLink().getLLink().setRLink(parent.getRLink().getRLink());
                    parent.setRLink(parent.getRLink().getLLink());
                } else {
                    parent.getLLink().getLLink().setLLink(parent.getLLink().getLLink());
                    parent.setLLink(parent.getLLink().getLLink());
                }
            } else {
                head.setLLink(parent.getLLink());
                root = parent.getLLink();
                root.setRLink(null);
            }
        }

        public void DeleteNodeBothChildren(bool deleteRoot, TreeNode parent, int direction) {
            TreeNode nodeToBeDeleted, tempNode;
            if (!deleteRoot) {
                if (direction == 1)
                    nodeToBeDeleted = parent.getRLink();
                else
                    nodeToBeDeleted = parent.getLLink();
            } else
                nodeToBeDeleted = parent;
            tempNode = nodeToBeDeleted.getRLink();
            if (tempNode.getLLink() != null && tempNode.getLTag())
            {
                while (tempNode.getLTag() && tempNode.getLLink() != null)
                {
                    tempNode = tempNode.getLLink();
                }
                if (!deleteRoot) {
                    if (direction == 1)
                        parent.setRLink(tempNode);
                    else
                        parent.setLLink(tempNode);
                } else {
                    head.setLLink(tempNode);
                    root = tempNode;
                }
                // transfer attributes
                tempNode.setLLink(nodeToBeDeleted.getLLink());
                tempNode.setLTag(nodeToBeDeleted.getLTag());
                tempNode.setRTag(nodeToBeDeleted.getRTag());
                tempNode.setRLink(nodeToBeDeleted.getRLink());
                nodeToBeDeleted.getLLink().setRLink(tempNode);
            } else {
                if (!deleteRoot) {
                    if (direction == 1)
                        parent.setRLink(tempNode);
                    else
                        parent.setLLink(tempNode);
                } else 
                    root = tempNode;
                tempNode.setLLink(nodeToBeDeleted.getLLink());
                tempNode.setLTag(nodeToBeDeleted.getLTag());
            }
        }

    }
}

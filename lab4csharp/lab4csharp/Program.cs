// Franklin Leung
// Lab 4 - COSC 3319
// Fall 2012

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace lab4csharp
{
    class MainClass
    {
        static void Main(string[] args)
            {
            BinarySearchTree myTree = new BinarySearchTree();
            TreeNode Q;

            // C1
            Console.WriteLine("Inserting data...");
            myTree.InsertBinarySearchTree(new Customer("Smith", "295-1492"));
            myTree.InsertBinarySearchTree(new Customer("Lenug", "291-1864"));
            myTree.InsertBinarySearchTree(new Customer("Nelson", "295-1601"));
            myTree.InsertBinarySearchTree(new Customer("Ji", "293-6122"));
            myTree.InsertBinarySearchTree(new Customer("Pruett", "295-1882"));
            myTree.InsertBinarySearchTree(new Customer("Henderson", "291-7890"));
            myTree.InsertBinarySearchTree(new Customer("Smith", "294-8075"));
            myTree.InsertBinarySearchTree(new Customer("Thorell", "584-3622"));

            // C2
            Console.WriteLine();
            Console.WriteLine("C2 Finding Ebrahimi iteratively...");
            try
            {
                myTree.FindCustomerIterative("Ebrahimi").visit();
            }
            catch
            {
                Console.WriteLine("C2 Customer not found.");
            }

            // C3
            Console.WriteLine();
            Console.WriteLine("C3 Finding Ebrahimi recursively...");
            try
            {
                myTree.FindCustomerRecursive(myTree.getRoot(), "Ebrahimi").visit();
            }
            catch
            {
                Console.WriteLine("C3 Customer not found.");
            }

            // C4
            Console.WriteLine();
            Console.WriteLine("C4 Finding Pruett and printing phone number iteratively...");
            try
            {
                myTree.FindCustomerIterative("Pruett").visit();
            }
            catch
            {
                Console.WriteLine("C4 Customer not found.");
            }

            // C5
            Console.WriteLine();
            Console.WriteLine("C5 Finding Pruett and printing phone number recursively...");
            try
            {
                myTree.FindCustomerRecursive(myTree.getRoot(), "Pruett").visit();
            }
            catch
            {
                Console.WriteLine("C5 Customer not found.");
            }

            // C6
            Console.WriteLine();
            Console.WriteLine("C6 Traversing in-order from Nelson");
            Q = myTree.FindCustomerIterative("Nelson");
            TreeNode firstNode = Q;
            do
            {
                if (Q != myTree.getHead())
                {
                    Q.visit();
                }
                Q = myTree.InOrderSuccessor(Q);
            } while (Q != firstNode);

            // C7
            Console.WriteLine();
            Console.WriteLine("C7 Inserting additonal nodes...");
            myTree.InsertBinarySearchTree(new Customer("Wieser", "294-1568"));
            myTree.InsertBinarySearchTree(new Customer("Lewis", "294-1882"));
            myTree.InsertBinarySearchTree(new Customer("Xin", "295-6622"));

            // C8
            Console.WriteLine();
            Console.WriteLine("C8 Traversing in-order froom root...");
            Q = myTree.InOrderSuccessor(myTree.getHead());
            do
            {
                Q.visit();
                Q = myTree.InOrderSuccessor(Q);
            } while (Q != myTree.getHead());

            // B7
            Console.WriteLine();
            Console.WriteLine("B7 Deleting nodes...");

            // myTree.DeleteTreeNode("Smith"); 
            // deleting the head node causes an infinite loop (pointer errors, I'm sure)
            // rest of code seem to work otherwise
            myTree.DeleteTreeNode("Ji");
            myTree.DeleteTreeNode("Thorell");
            myTree.DeleteTreeNode("Wieser");
            myTree.DeleteTreeNode("Burris");
            // myTree.DeleteTreeNode("Smith");

            // B8
            Console.WriteLine();
            Console.WriteLine("B8 Inserting new nodes...");
            myTree.InsertBinarySearchTree(new Customer("Reioux", "294-1666"));
            myTree.InsertBinarySearchTree(new Customer("Sanchez", "295-1882"));

            // B9
            Console.WriteLine();
            Console.WriteLine("B9 Traversing in-order from root...");
            Q = myTree.InOrderSuccessor(myTree.getHead());
            do
            {
                Q.visit();
                Q = myTree.InOrderSuccessor(Q);
            } while (Q != myTree.getHead());

            // B10
            Console.WriteLine();
            Console.WriteLine("B10 Traversing in Reverse-In-Order from root...");
            myTree.ReverseInOrder(myTree.getHead().getLLink());

            // B11
            Console.WriteLine();
            Console.WriteLine("B11 Traversing in Pre-Order from root...");
            myTree.PreOrder(myTree.getHead().getLLink());

            // A12
            Console.WriteLine();
            Console.WriteLine("A12 Traversing Post-Order iteratively from root...");
            myTree.PostOrderIterative(myTree.getHead().getLLink());

            //// A13
            Console.WriteLine();
            Console.WriteLine("A13 Traversing Post-Order recursively from root...");
            myTree.PostOrderRecursive(myTree.getHead().getLLink());

            Console.ReadLine();
        }
    }
}

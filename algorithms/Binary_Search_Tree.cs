/* What is a BINARY TREE ?
	 In CS, a binary tree is a hierarchical structure of nodes, each node referencing at most to two child nodes. 
	 Every binary tree has a root from which the first two child nodes originate.

   What is BINARY SEARCH TREE
	 A particular kind of binary tree, called the binary search tree, is very useful for storing data for rapid access, storage, and deletion. 
	 Data in a binary search tree are stored in tree nodes, and must have associated with them an ordinal value or key.
	 These keys are used to structure the tree such that the value of a left child node is less than that of the parent node, and the value of a right child node is greater than that of the parent node
*/

/* C# Program to Implement BINARY SEARCH TREE using LinkedList */

using System;
using System.Collections.Generic;
using System.Text;
namespace TreeSort
{
    class Node
    {
        public int item;
        public Node leftc;
        public Node rightc;
        public void display()
        {
            Console.Write("[");
            Console.Write(item);
            Console.Write("]");
        }
    }
    class Tree
    {
        public Node root;
        public Tree()
        { 
            root = null; 
        }
        public Node ReturnRoot()
        {
            return root;
        }
        public void Insert(int id)
        {
            Node newNode = new Node();
            newNode.item = id;
            if (root == null)
                root = newNode;
            else
            {
                Node current = root;
                Node parent;
                while (true)
                {
                    parent = current;
                    if (id < current.item)
                    {
                        current = current.leftc;
                        if (current == null)
                        {
                            parent.leftc = newNode;
                            return;
                        }
                    }
                    else
                    {
                        current = current.rightc;
                        if (current == null)
                        {
                            parent.rightc = newNode;
                            return;
                        }
                    }
                }
            }
        }
        public void Preorder(Node Root)
        {
            if (Root != null)
            {
                Console.Write(Root.item + " ");
                Preorder(Root.leftc);
                Preorder(Root.rightc);
            }
        }
        public void Inorder(Node Root)
        {
            if (Root != null)
            {
                Inorder(Root.leftc);
                Console.Write(Root.item + " ");
                Inorder(Root.rightc);
            }
        }
        public void Postorder(Node Root)
        {
            if (Root != null)
            {
                Postorder(Root.leftc);
                Postorder(Root.rightc);
                Console.Write(Root.item + " ");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Tree theTree = new Tree();
            theTree.Insert(20);
            theTree.Insert(25);
            theTree.Insert(45);
            theTree.Insert(15);
            theTree.Insert(67);
            theTree.Insert(43);
            theTree.Insert(80);
            theTree.Insert(33);
            theTree.Insert(67);
            theTree.Insert(99);
            theTree.Insert(91);            
            Console.WriteLine("Inorder Traversal : ");
            theTree.Inorder(theTree.ReturnRoot());
            Console.WriteLine(" ");
            Console.WriteLine();
            Console.WriteLine("Preorder Traversal : ");
            theTree.Preorder(theTree.ReturnRoot());
            Console.WriteLine(" ");
            Console.WriteLine();
            Console.WriteLine("Postorder Traversal : ");
            theTree.Postorder(theTree.ReturnRoot());
            Console.WriteLine(" ");
            Console.ReadLine();
        }
    }
}


/*
Here is the output of the C# Program:

inorder traversal :
15  20  25  33  43  45  67  67  80  91  99
Preorder Traversal :
20  15  25  45  43  33  67  80  67  99  91
Postorder Traversal :
15  33  43  67  91  99  80  67  45  25  20
*/
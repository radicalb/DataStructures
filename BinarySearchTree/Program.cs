using System;
using RadicalB.DataStructures.BSTreeDictGeneric;

using System.Collections.Generic;
/// <summary>
/// My implementation of BST. It Has two classes. INode & BSTree.
/// </summary>
namespace RadicalB.DataStructures
{
    class Program
    {
        static void Main(string[] args)
        {

            BSTreeDict<string> bst = new BSTreeDict<string>(20, "twenty");
            bst.Insert(15, "fifteen");
            bst.Insert(25, "twentyfive");
            bst.Insert(18, "eighteen");
            bst.Insert(10, "ten");
            bst.Insert(19, "nineteen");
            bst.Insert(16, "sixteen");
            bst.Insert(17, "seventeen");

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


            Console.WriteLine(bst.PrintTraversedKeys(BSTreeDict<string>.TreeTraversalForm.BreathFirstSearch));

            /*
            bst.Delete(20);
            Console.WriteLine(bst.PrintTraversedKeys(BSTreeDict<string>.TreeTraversalForm.BreathFirstSearch));
            bst.Delete(15);
            Console.WriteLine(bst.PrintTraversedKeys(BSTreeDict<string>.TreeTraversalForm.BreathFirstSearch));
            bst.Delete(18);
            */

            bst.Delete(15);
            Console.WriteLine("Deleting completed. Node 15 removed.");
            /*
                 20
                /  \
               16  25
              /  \
             10  18
                /  \
               17  19

           */

            Console.WriteLine("Tree contains 18: "+bst.Contains(18));
            Console.WriteLine(bst.PrintTraversedKeys(BSTreeDict<string>.TreeTraversalForm.BreathFirstSearch));

            string[] aValues = bst.TraverseValues(BSTreeDict<string>.TreeTraversalForm.BreathFirstSearch);

            foreach (string val in aValues)
            {
                Console.Write($"{val} ");
            }

            Console.WriteLine();


            //Console.WriteLine(bst.GetValue(18));
            Console.WriteLine(bst.GetValue(25));

        }
    }

 

   
}

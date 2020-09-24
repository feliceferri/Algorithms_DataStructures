using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace Algorithms_DataStructures
{
    class Huffman
    {
        public class Node
        {
            public int? label { get; set; }
            public int weight { get; set; }
            public Node right { get; set; }
            public Node left { get; set; }
          
            public Node(int? label, int weight)
            {
                this.label = label;
                this.weight = weight;
            }
        }

        public class NodeExtended: Node
        {
            public NodeExtended(Node node, int Level, string Encoding):base(node.label, node.weight)
            {
                this.right = node.right;
                this.left = node.left;
                this.level = Level;
                this.encoding  = Encoding;
            }
            public int level { get; set; }
            public string encoding { get; set; }
        }

        //Rename to Main to use it
        static void Main_Renamed(string[] args)
        {
            var rootDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            
            //USE THIS FOR A LARGE DATASET
            string[] StringArray = File.ReadAllLines(rootDir + @"\Huffman\TestData_Stanford.txt");
            
            //USE THIS FOR A SMALL DATASET (First value = n)
            //string[] StringArray = new string[] {"8","1",  "10","100","1000","10000", "100000", "1000000", "10000000" };


            //O(n Log n) | O (n)
            SortedDictionary<int, Node> sortedDic_Weight_Node = new SortedDictionary<int, Node>();

            ////// LOAD DATA .txt into SortedDictionary
            int i = 0;
            foreach (string s in StringArray)
            {
                if (i > 0)
                {
                    sortedDic_Weight_Node.Add(Convert.ToInt32(s), new Node(i, Convert.ToInt32(s)));
                }
                i++;
            }


            ////// CREATE THE TREE   O(n Log n) => its a n loop, but it has the sorted dic inside
            while (sortedDic_Weight_Node.Count > 1)
            {
                MergeTheTwoWithMoreWeight(sortedDic_Weight_Node);
            }

            ///// PRINT BINARY TREE RESULTS WITH THE LEVELS  O(n) | O(n)
            BFSToDisplayNodesAndTreeLevels(sortedDic_Weight_Node.First().Value);
        }

        private static void MergeTheTwoWithMoreWeight(SortedDictionary<int, Node> sortedDic_Weight_Node)
        {
            KeyValuePair<int, Node> lastWeightNodePair = sortedDic_Weight_Node.First();
            sortedDic_Weight_Node.Remove(lastWeightNodePair.Key);
            KeyValuePair<int, Node> secondLastWeightNodePair = sortedDic_Weight_Node.First();
            sortedDic_Weight_Node.Remove(secondLastWeightNodePair.Key);

            Node n = new Node(null, lastWeightNodePair.Key + secondLastWeightNodePair.Key);
            if (lastWeightNodePair.Key > secondLastWeightNodePair.Key)
            {
                n.right = lastWeightNodePair.Value;
                n.left = secondLastWeightNodePair.Value;
            }
            else
            {
                n.right = secondLastWeightNodePair.Value;
                n.left = lastWeightNodePair.Value;
            }

            sortedDic_Weight_Node.Add(lastWeightNodePair.Key + secondLastWeightNodePair.Key, n);
        }

        ///Breadth First Search to display the results
        ///O(n) | O(n) * Last level in a balanced tree account almost for half of the n, and all those leaves will be stored in memory that's why BFS is O(n) in Space Complexity
        private static void BFSToDisplayNodesAndTreeLevels(Node root)
        {
            Queue<NodeExtended> queue = new Queue<NodeExtended>();
            queue.Enqueue(new NodeExtended(root, 0, ""));

            while (queue.Count > 0)
            {
                NodeExtended node = queue.Dequeue();
                if (node == null)
                    continue;
                Console.WriteLine("Level: " + node.level + " Weight: " + (node.label > 0 ? node.weight.ToString() + " Encoding: " + node.encoding : ""));

                if (node.right != null)
                    queue.Enqueue(new NodeExtended(node.right,node.level +1, node.encoding + "1"));
                
                if (node.left != null)
                    queue.Enqueue(new NodeExtended(node.left, node.level + 1, node.encoding + "0"));
                

            }
        }
    }
}

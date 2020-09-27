using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Algorithms_DataStructures.Bellman_Ford
{

    internal class Edge
    {
        public int Tail { get; set; }
        public int Head { get; set; }
        public int Weight { get; set; }

        public Edge(int tail, int head, int weigth)
        {
            this.Tail = tail;
            this.Head = head;
            this.Weight = weigth;
        }
    }

    //O(NM) | O (N) For Single Source
    //O(N^2xM) | O(N) For All Pairs Comparison (Will iterate through each source)
    //O(NxM) | O(N) For All Pairs Comparison with TRICK => Creating an External Vertice that will have an edge to all vertices, so there wont be unreachable vertices.
    class Bellman_Ford
    {
        static void MainRenamed(string[] args)
        {

            Dictionary<int, List<Edge>> dic_Tail_Edges = new Dictionary<int, List<Edge>>();
            

            ////////////////////////////////////////////////////////
            /// LOAD DATA FROM FILE ////////////////////////////////////
            var rootDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            string[] StringArray = File.ReadAllLines(rootDir + @"\Bellman-Ford\stanford_g3.txt");
            //string[] StringArray = File.ReadAllLines(rootDir + @"\Bellman_Ford\input_random_10_8.txt");
            
            dic_Tail_Edges.Add(0, new List<Edge>()); //This is a trick that creates a new "external/source" vertice, that will have edges to all existing vertices
                                                     //In this way there wont be "unreacheable" edges.

            int vertices = 0;
            int iRow = 0;
            foreach (string s in StringArray)
            {
                string[] splits = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (iRow == 0)
                {
                    vertices = Convert.ToInt32(splits[0]);
                }
                else
                {
                    int tail = Convert.ToInt32(splits[0]);
                    if (dic_Tail_Edges.ContainsKey(tail) == false)
                        dic_Tail_Edges.Add(tail, new List<Edge>());

                    dic_Tail_Edges[tail].Add(new Edge(tail, Convert.ToInt32(splits[1]), Convert.ToInt32(splits[2])));
                    dic_Tail_Edges[0].Add(new Edge(0, tail, 0)); //Part of the trick of the "external/source" vertice. Weight of the Edge is always 0
                }
                iRow++;
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////////


            /////TEST DATA WITHOUT NEGATIVE COST CYCLE
            //dic_Tail_Edges.Add(1, new List<Edge>());
            //dic_Tail_Edges[1].Add(new Edge(1, 2, 10));
            //dic_Tail_Edges[1].Add(new Edge(1, 6, 8));
            //dic_Tail_Edges.Add(2, new List<Edge>());
            //dic_Tail_Edges[2].Add(new Edge(2, 4, 2));
            //dic_Tail_Edges.Add(3, new List<Edge>());
            //dic_Tail_Edges[3].Add(new Edge(3, 2, 1));
            //dic_Tail_Edges.Add(4, new List<Edge>());
            //dic_Tail_Edges[4].Add(new Edge(4, 3, -2));
            //dic_Tail_Edges.Add(5, new List<Edge>());
            //dic_Tail_Edges[5].Add(new Edge(5, 2, -4));
            //dic_Tail_Edges[5].Add(new Edge(5, 4, -1));
            //dic_Tail_Edges.Add(6, new List<Edge>());
            //dic_Tail_Edges[6].Add(new Edge(6, 5, 1));

            ///TEST DATA WITH NEGATIVE COST CYCLE
            //dic_Tail_Edges.Add(1, new List<Edge>());
            //dic_Tail_Edges[1].Add(new Edge(1, 2, 10));
            //dic_Tail_Edges[1].Add(new Edge(1, 6, 8));
            //dic_Tail_Edges.Add(2, new List<Edge>());
            //dic_Tail_Edges[2].Add(new Edge(2, 4, 2));
            //dic_Tail_Edges[2].Add(new Edge(2, 5, -4));
            //dic_Tail_Edges.Add(3, new List<Edge>());
            //dic_Tail_Edges[3].Add(new Edge(3, 2, 1));
            //dic_Tail_Edges.Add(4, new List<Edge>());
            //dic_Tail_Edges[4].Add(new Edge(4, 3, -2));
            //dic_Tail_Edges.Add(5, new List<Edge>());
            //dic_Tail_Edges[5].Add(new Edge(5, 4, -1));
            //dic_Tail_Edges.Add(6, new List<Edge>());
            //dic_Tail_Edges[6].Add(new Edge(6, 5, 1));
                        

            
            int ShortestCostBetweenTwoVertices = int.MaxValue;

            //All Pairs: O(N^2 x M) | O(N)
            //To compute all Pairs we have to iterate through each Vertice to set it as the source (this will take care of unreacheable vertices)
            //for (int sourceEdge = 1; sourceEdge <= vertices; sourceEdge++)
            //{
                int[] currentMinCostFromSource = new int[vertices + 1];
                currentMinCostFromSource = Enumerable.Repeat(int.MaxValue, vertices + 1).ToArray(); //Index 0 doesnt count.
                //currentMinCostFromSource[sourceEdge] = 0; //Source Vertice
                currentMinCostFromSource[0] = 0; //this part of the Trick

                //O(N x M) | O(N)
                for (int i = 1; i <= vertices + 1; i++)
                {
                    if (i == vertices + 1)
                    {
                        Console.WriteLine("There is a negative cost cycle");
                        return;
                    }

                    bool CostMinimized = ComputeShortestDistancesToAllVertices(dic_Tail_Edges, i, currentMinCostFromSource, ref ShortestCostBetweenTwoVertices);
                    if (CostMinimized == false)
                    {
                        Console.WriteLine("No changes between the last two cycles");
                        break;
                    }

                }
            //}

            Console.WriteLine("End => Shortests Cost Path Between two Vertices : " + ShortestCostBetweenTwoVertices);
        }

        //O(M) **Inner loops but at the end of the day is analizying each Edge once.
        private static bool ComputeShortestDistancesToAllVertices(Dictionary<int, List<Edge>> dic_Tail_Edges, int MaxNumberOfEdgesAllowed, int[] shortesPathCost,
                                                                    ref int ShortestCostBetweenTwoVertices)
        {

            bool CostMinimized = false;

            foreach(KeyValuePair<int, List<Edge>> pair_Tail_Edges in dic_Tail_Edges) //Iterates through each Vertice
            {

                if (shortesPathCost[pair_Tail_Edges.Key] == int.MaxValue) //Value its Infinity as this Vertice has not been reached yet, so we skip it
                    continue;

                foreach (Edge edge in pair_Tail_Edges.Value) //Iterates throgh each Edge comming out from the current Vertice
                {
                    if (shortesPathCost[edge.Tail] + edge.Weight < shortesPathCost[edge.Head])
                    {
                        shortesPathCost[edge.Head] = shortesPathCost[edge.Tail] + edge.Weight;
                        ShortestCostBetweenTwoVertices = Math.Min(ShortestCostBetweenTwoVertices, shortesPathCost[edge.Tail] + edge.Weight);
                        ShortestCostBetweenTwoVertices = Math.Min(ShortestCostBetweenTwoVertices, edge.Weight);
                        CostMinimized = true;
                    }
                   
                }
            }

            return CostMinimized;
        }
    }
}

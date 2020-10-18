using Algorithms_DataStructures.Kosarajus__SCC_;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Algorithms_DataStructures._2_SAT_Problem
{
    class _2_SAT_Problem
    {

     
        static void Main(string[] args)
        {
            Dictionary<int, List<int>> dic_Vertice_Edges = new Dictionary<int, List<int>>();
            Dictionary<int, List<int>> Inverteddic_Vertice_Edges = new Dictionary<int, List<int>>();

            //Load Data from file
            var rootDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string[] StringArray = File.ReadAllLines(rootDir + @"\2-SAT Problem\stanford_2sat1.txt");

            //O(N) N being the number of Disjunctions
            //Create regular and inverted/transposed Graph
            int i = 0;
            foreach (string s in StringArray)
            {
                if (i == 0)
                {
                    i++;
                    continue;
                }

                string[] splits = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                int VerticeA = Convert.ToInt32(splits[0]);
                int VerticeB = Convert.ToInt32(splits[1]);
                int VerticeA_Abs = Math.Abs(VerticeA);
                int VerticeB_Abs = Math.Abs(VerticeB);

                if(VerticeA == VerticeB *-1)
                {
                    Console.WriteLine("Insatisfiable: " + VerticeA + " => " + VerticeB);
                    return;
                }

                if (VerticeA > 0 && VerticeB > 0)
                {
                    AddVerticeToBothGraphsRegularAndTransposed(dic_Vertice_Edges, Inverteddic_Vertice_Edges,
                                                                VerticeA_Abs * -1, VerticeB_Abs);
                    AddVerticeToBothGraphsRegularAndTransposed(dic_Vertice_Edges, Inverteddic_Vertice_Edges,
                                                                VerticeB_Abs * -1, VerticeA_Abs);
                }
                else if (VerticeA < 0 && VerticeB > 0)
                {
                    AddVerticeToBothGraphsRegularAndTransposed(dic_Vertice_Edges, Inverteddic_Vertice_Edges,
                                                                VerticeA_Abs, VerticeB_Abs);
                    AddVerticeToBothGraphsRegularAndTransposed(dic_Vertice_Edges, Inverteddic_Vertice_Edges,
                                                                VerticeB_Abs * -1, VerticeA_Abs * -1);
                }
                else if (VerticeA < 0 && VerticeB < 0)
                {
                    AddVerticeToBothGraphsRegularAndTransposed(dic_Vertice_Edges, Inverteddic_Vertice_Edges,
                                                                VerticeA_Abs, VerticeB_Abs * -1);
                    AddVerticeToBothGraphsRegularAndTransposed(dic_Vertice_Edges, Inverteddic_Vertice_Edges,
                                                                VerticeB_Abs, VerticeA_Abs * -1);
                }
                else
                {
                    //VerticeA > 0 && VerticeB < 0
                    AddVerticeToBothGraphsRegularAndTransposed(dic_Vertice_Edges, Inverteddic_Vertice_Edges,
                                                                VerticeA_Abs * - 1, VerticeB_Abs * -1);
                    AddVerticeToBothGraphsRegularAndTransposed(dic_Vertice_Edges, Inverteddic_Vertice_Edges,
                                                                VerticeB_Abs, VerticeA_Abs);
                }

                i++;
            }
            ////////////////////////////////

            //O(V+E) | O(N)   
            List<List<int>> SCCs = Kosarajus__SCC_.Kosarajus.GetSCC(dic_Vertice_Edges, Inverteddic_Vertice_Edges);

            //check if there is X & -X in the same Strongly Connected Component
            foreach(List<int> SCC in SCCs)
            {
                HashSet<int> hashVertices = new HashSet<int>(SCC.Count);
                foreach(int vertice in SCC)
                {
                    if (hashVertices.Contains(vertice * -1))
                    {
                        Console.WriteLine("Safistability: False");
                        return;
                    }

                    hashVertices.Add(vertice);
                }
                                
            }


            Console.WriteLine("Safistability: True");
        }

        private static void AddVerticeToBothGraphsRegularAndTransposed(Dictionary<int, List<int>> dic_Vertice_Edges,
                                                                        Dictionary<int, List<int>> Inverteddic_Vertice_Edges,
                                                                        int VerticeA, int VerticeB)
        {
            if (dic_Vertice_Edges.ContainsKey(VerticeA) == false)
                    dic_Vertice_Edges.Add(VerticeA, new List<int>());

                dic_Vertice_Edges[VerticeA].Add(Convert.ToInt32(VerticeB));

                ////1) Inverted/Transpose Grapth
                if (Inverteddic_Vertice_Edges.ContainsKey(VerticeB) == false)
                    Inverteddic_Vertice_Edges.Add(VerticeB, new List<int>());
                Inverteddic_Vertice_Edges[VerticeB].Add(Convert.ToInt32(VerticeA));

        }
    }
}

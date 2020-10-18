using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Algorithms_DataStructures.Kosarajus__SCC_
{
    internal class Kosarajus
    {
        static void MainRenamed(string[] args)
        {
            Dictionary<int, List<int>> dic_Vertice_Edges = new Dictionary<int, List<int>>();
            Dictionary<int, List<int>> Inverteddic_Vertice_Edges = new Dictionary<int, List<int>>();
                        

            //Load Data from file
            var rootDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string[] StringArray = File.ReadAllLines(rootDir + @"\Kosarajus (SCC)\stanford_SCC.txt");
            
            //Create regular and inverted/transposed Graph
            foreach (string s in StringArray)
            {
                string[] splits = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                int VerticeLabel = Convert.ToInt32(splits[0]);
                int VerticeEdge = Convert.ToInt32(splits[1]);

                if (dic_Vertice_Edges.ContainsKey(VerticeLabel) == false)
                    dic_Vertice_Edges.Add(VerticeLabel, new List<int>());

                dic_Vertice_Edges[VerticeLabel].Add(Convert.ToInt32(VerticeEdge));

                ////1) Inverted/Transpose Grapth
                if (Inverteddic_Vertice_Edges.ContainsKey(VerticeEdge) == false)
                    Inverteddic_Vertice_Edges.Add(VerticeEdge, new List<int>());
                Inverteddic_Vertice_Edges[VerticeEdge].Add(Convert.ToInt32(VerticeLabel));
            }
            ////////////////////////////////

            List<List<int>> SCCs = GetSCC(dic_Vertice_Edges, Inverteddic_Vertice_Edges);


            Console.WriteLine("Number of Strongly Connected Components: " + SCCs.Count());
        }

      
        ///Receives the Original and Transpose version of the Graph
        ///Just because in this case, it makes more sense to create the Original and Transpose one in tandem
        internal static List<List<int>> GetSCC(Dictionary<int, List<int>> dic_Vertice_Edges, Dictionary<int, List<int>> Inverteddic_Vertice_Edges)
        {
            Dictionary<int, int> dic_Vertice_Score = new Dictionary<int, int>();

            //3) Iterate through each of the nodes once, checking in a hastable if the node has not been visited previously
            int scoreCounter = 0;
            HashSet<int> ExploredVerticesFirstPass = new HashSet<int>();
            foreach (var item in Inverteddic_Vertice_Edges)
            {
                if (ExploredVerticesFirstPass.Contains(item.Key) == false)
                {
                    //4) If the node has not been visited, then Do a Depth First Search the last node found in the DFS will be scored at Score++
                    scoreCounter = DFS(Inverteddic_Vertice_Edges, item.Key, ExploredVerticesFirstPass, scoreCounter, dic_Vertice_Score);
                }
            }

            //7) Order the Vertices based on the Score from highest to lowest
            List<int> VerticesSortedByScoreDesc_ = dic_Vertice_Score.OrderByDescending(x => x.Value).Select(x => x.Key).ToList();

            List<List<int>> SCCs = new List<List<int>>();

            //8) Iterate through all the nodes another time based on the Scoring Order(7)
            HashSet<int> ExploredVerticesSecondPass = new HashSet<int>();
            foreach (int vertice in VerticesSortedByScoreDesc_)
            {
                if (ExploredVerticesSecondPass.Contains(vertice) == false)
                {
                    List<int> res = DFS_GetSCC(dic_Vertice_Edges, vertice, ExploredVerticesSecondPass);
                    SCCs.Add(res);
                }
            }

            return SCCs;
        }

       
        private static List<int> DFS_GetSCC(Dictionary<int, List<int>> dic_Vertice_Edges, int vertice, HashSet<int> ExploredVertices)
        {
            List<int> res = new List<int>();

            Stack<int> stack = new Stack<int>();
            stack.Push(vertice);
            while (stack.Count > 0)
            {
                vertice = stack.Pop();
                if (ExploredVertices.Contains(vertice))
                {
                    continue;
                }

                ExploredVertices.Add(vertice);
                res.Add(vertice);

                if (dic_Vertice_Edges.ContainsKey(vertice)) //Because is Inverted, sometime doesn have edges, because of the inversion
                {
                    List<int> edges = dic_Vertice_Edges[vertice];
                    foreach (int edge in edges)
                        stack.Push(edge);
                }
            }

            return res;
        }

        private static int DFS(Dictionary<int, List<int>> dic_Vertice_Edges, int vertice, HashSet<int> ExploredVertices, int ScoreCounter,
                                         Dictionary<int, int> dic_Vertice_Score)
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(vertice);

            while (stack.Count > 0)
            {
                vertice = stack.Peek();


                ExploredVertices.Add(vertice);

                bool Flag_AddedEdge = false;
                if (dic_Vertice_Edges.ContainsKey(vertice)) //Because is Inverted, sometime doesn have edges, because of the inversion
                {
                    List<int> edges = dic_Vertice_Edges[vertice];

                    foreach (int edge in edges)
                    {
                        if (ExploredVertices.Contains(edge) == false)
                        {
                            Flag_AddedEdge = true;
                            stack.Push(edge);
                        }

                    }
                }

                if (Flag_AddedEdge == false)
                {
                    //Close this level
                    //5) As DFS backtracks will be adding Score++ to all the parents. So the node that started the DFS will have the highest Score
                    vertice = stack.Pop();
                    ScoreCounter++;
                    if (dic_Vertice_Score.ContainsKey(vertice) == false)
                        dic_Vertice_Score.Add(vertice, ScoreCounter);
                }




            }

            return ScoreCounter;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Algorithms_DataStructures.Kosarajus__SCC_
{
    class Kosarajus
    {
        static void Main(string[] args)
        {
            //List<Vertice> vertices = new List<Vertice>();
            Dictionary<int, List<int>> dic_Vertice_Edges = new Dictionary<int, List<int>>();
            Dictionary<int, List<int>> Inverteddic_Vertice_Edges = new Dictionary<int, List<int>>();
            Dictionary<int, int> dic_Vertice_Score = new Dictionary<int, int>();
            HashSet<int> ExploredVerticesFirstPass = new HashSet<int>();
            HashSet<int> ExploredVerticesSecondPass = new HashSet<int>();

            //Load Data from file
            var rootDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string[] StringArray = File.ReadAllLines(rootDir + @"\Kosarajus (SCC)\stanford_SCC.txt");
            
            foreach (string s in StringArray)
            {
                string[] splits = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                int VerticeLabel = Convert.ToInt32(splits[0]);
                int VerticeEdge = Convert.ToInt32(splits[1]);

                if (dic_Vertice_Edges.ContainsKey(VerticeLabel) == false)
                    dic_Vertice_Edges.Add(VerticeLabel, new List<int>());

                dic_Vertice_Edges[VerticeLabel].Add(Convert.ToInt32(VerticeEdge));

                ////Inverted
                if (Inverteddic_Vertice_Edges.ContainsKey(VerticeEdge) == false)
                    Inverteddic_Vertice_Edges.Add(VerticeEdge, new List<int>());
                Inverteddic_Vertice_Edges[VerticeEdge].Add(Convert.ToInt32(VerticeLabel));
            }

            int scoreCounter = 0;
            foreach (var item in Inverteddic_Vertice_Edges)
            {
                if (ExploredVerticesFirstPass.Contains(item.Key) == false)
                {
                    scoreCounter = DFS(Inverteddic_Vertice_Edges, item.Key, ExploredVerticesFirstPass, scoreCounter, dic_Vertice_Score);
                }
            }

            List<int> VerticesSortedByScoreDesc_ = dic_Vertice_Score.OrderByDescending(x => x.Value).Select(x => x.Key).ToList();
            List<List<int>> SCCs = new List<List<int>>();
            foreach (int vertice in VerticesSortedByScoreDesc_)
            {
                if (ExploredVerticesSecondPass.Contains(vertice) == false)
                {
                    List<int> res = DFS_GetSCC(dic_Vertice_Edges, vertice, ExploredVerticesSecondPass);
                    SCCs.Add(res);
                }
            }

            var SCCs_ordered = SCCs.OrderByDescending(x => x.Count).ToList();

            Console.WriteLine("Number of Strongly Connected Components: " + SCCs_ordered.Count());
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
                    vertice = stack.Pop();
                    ScoreCounter++;
                    if (dic_Vertice_Score.ContainsKey(vertice) == true)
                    {
                        int z = 12;
                    }
                    else
                        dic_Vertice_Score.Add(vertice, ScoreCounter);
                }




            }

            return ScoreCounter;
        }
    }
}
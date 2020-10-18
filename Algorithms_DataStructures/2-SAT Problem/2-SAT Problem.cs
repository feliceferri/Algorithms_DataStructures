using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Algorithms_DataStructures._2_SAT_Problem
{
    class _2_SAT_Problem
    {

        private static int GetVertexId(int label)
        {
            if (label < 0)
                return 2 * Math.Abs(label);
            else
                return 2 * label - 1;
        }

        static void MainRenamed(string[] args)
        {
            //Load Data from file
            var rootDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string[] StringArray = File.ReadAllLines(rootDir + @"\K2-SAT Problem\n10Test.txt");

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

            Console.WriteLine("End");
        }
    }
}

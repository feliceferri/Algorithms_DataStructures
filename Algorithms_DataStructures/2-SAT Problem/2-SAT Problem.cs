using System;
using System.Collections.Generic;
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
            int[,] data = new int[4, 2] {   { 1, -2 }, 
                                            { -1, 2 }, 
                                            { -1, -2 }, 
                                            { 1, -3 } };

            List<int> g = new List<int>(data.Length *2 + 1);
            for(int i = 0; i< data.Length;i++)
            {
                int u = GetVertexId(data[i, 0]);
                int v = GetVertexId(data[i, 1]);
            }

            Console.WriteLine("End");
        }
    }
}

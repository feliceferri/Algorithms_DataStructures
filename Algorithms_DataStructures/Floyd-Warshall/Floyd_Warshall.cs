using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;


namespace Algorithms_DataStructures.Floyd_Warshall
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
    class Floyd_Warshall
    {
        static void MainRenamed(string[] args)
        {
            List<Edge> edges = new List<Edge>();
            int vertices = 0;

            //Small Test case
            //edges.Add(new Edge(1,3,-2));
            //edges.Add(new Edge(3, 4, 2));
            //edges.Add(new Edge(4, 2, -1));
            //edges.Add(new Edge(2, 1, 4));
            //edges.Add(new Edge(2, 3, 3));
            //vertices = 4;


            //Load Data from file
            var rootDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string[] StringArray = File.ReadAllLines(rootDir + @"\Floyd-Warshall\stanford_g3.txt");
            
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
                    edges.Add(new Edge(Convert.ToInt32(splits[0]), Convert.ToInt32(splits[1]), Convert.ToInt32(splits[2])));
                }
                iRow++;
            }
            /////////////////////////////////////////////////////
       
            

            //This matrix is a Vertice-Vertice matrix or Tail-Head matrix that will store the Cost/Weight of the edge.
            //O(N^2)
            int[,] matrix = new int[vertices + 1, vertices + 1];  //One Index Based
            FillArray(matrix, int.MaxValue);

            //Fill the matrix with the original weights of each edge
            //Create HashSet of vertices
            HashSet<int> hashVertices = new HashSet<int>(vertices);
            foreach (Edge edge in edges)
            {
                matrix[edge.Tail, edge.Head] = edge.Weight;
                hashVertices.Add(edge.Tail);
                hashVertices.Add(edge.Head); //Just in case its a vertice without outgoin edges
            }
            
            //Fill the matrix with 0 for the intersection of each vertice
            foreach(int vertice in hashVertices)
            {
                matrix[vertice, vertice] = 0;
            }

            int ShortestLenghtOfAllPairs = int.MaxValue;


            //O(N^3)
            for(int k = 1; k <= vertices; k++ )
                for (int i = 1; i <= vertices; i++)
                    for (int j = 1; j <= vertices; j++)
                    {
                        matrix[i, j] = Math.Min(matrix[i, j], SumAsIfWeWereSummingInfinity( matrix[i, k] , matrix[k, j]));
                        ShortestLenghtOfAllPairs = Math.Min(ShortestLenghtOfAllPairs, matrix[i, j]);
                    }

           Console.WriteLine("ShortestLenghtOfAllPairs: " + ShortestLenghtOfAllPairs);
        }

        //This is to avoid Overflow, which in C# will mean that after the sum (int.MaxValue + X), the result will be negative
        private static int SumAsIfWeWereSummingInfinity(int a, int b)
        {
            if (a == int.MaxValue || b == int.MaxValue)
                return int.MaxValue;

            return a + b;
        }

        //Used to fill the array with infinity values
        public static void FillArray(int[,] array, int value)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = value;
                }
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;



namespace Algorithms_DataStructures
{
    class MaxWeightIndependentSetOfGraph
    {
        //Rename to Main to use it
        static void Main_Renamed(string[] args)
        {
            var rootDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            //USE THIS FOR A LARGE DATASET
            //string[] StringArray = File.ReadAllLines(rootDir + @"\Max Weight Independent Set of a Graph\TestData_Stanford.txt");

            //USE THIS FOR A SMALL DATASET (First value = n)
            string[] StringArray = new string[] {"10","72", "48","89","21","34", "12", "18", "64","52","22" };

            ////// CONVERT STRING VALUES FROM FILE TO LIST OF INT
            //Index 1 based
            List<int> values = new List<int>();
            for (int i = 1; i < StringArray.Length; i++)
                values.Add(Convert.ToInt32(StringArray[i]));

            
            //EDGE CASES
            if (values.Count == 0)
                return;
            else if (values.Count == 1)
            {
                Console.WriteLine("Result  Index used: 0  Max Weight:" + values[0]);
                return;
            }
            else if (values.Count == 2)
            {
                if (values[0] > values[1])
                    Console.WriteLine("Result  Index used: 0  Max Weight:" + values[0]);
                else
                    Console.WriteLine("Result  Index used: 1  Max Weight:" + values[1]);
                return;
            }
            ///////////////////////


            //O(n^2) space
            List<int>[] indexPointers = new List<int>[values.Count];

         
            //O(1) space for the sum, instead of O(n)
            int Aggregated_n0 = values[0];
            int Aggregated_n1 = values[1];
            indexPointers[0] = new List<int>() { 0 };
            indexPointers[1] = new List<int>() { 0 };

            
            //O(n) time 
            for (int i = 2; i < values.Count; i++)
            {
                int currentSum = 0;
                
                if (values[i] + Aggregated_n0 > Aggregated_n1)
                {
                    currentSum = values[i] + Aggregated_n0;
                    indexPointers[i] = new List<int>(indexPointers[i - 2]);
                    indexPointers[i].Add(i);
                
                }
                else
                {
                    currentSum = Aggregated_n1;
                    indexPointers[i] = new List<int>(indexPointers[i - 1]);
                   
                }

                //Swap values to maintain n0 and n1
                Aggregated_n0 = Aggregated_n1;
                Aggregated_n1 = currentSum;
                
            }


            //Print Results
            //I'll create a HashSet to have O(1) when searching if the index is part of the Max Subset
            HashSet<int> MaxSubsetIndexes = new HashSet<int>(indexPointers[indexPointers.Length -1]);
            int total = 0;
            Console.WriteLine("Indexes used: " + total);
            Console.WriteLine("");

            for (int i = 0; i < values.Count;i++)
            {
                if (MaxSubsetIndexes.Contains(i))
                {
                    Console.WriteLine("Index: " + i + " => " + values[i]);
                    total += values[i];
                }
            }

            Console.WriteLine("");
            Console.WriteLine("TOTALS SUM: " + total);
            //////////////////////////////////////////////////////////
        }

     
    }
}

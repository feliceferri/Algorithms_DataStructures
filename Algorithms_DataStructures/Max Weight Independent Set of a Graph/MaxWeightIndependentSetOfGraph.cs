using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;



namespace Algorithms_DataStructures
{
    class MaxWeightIndependentSetOfGraph
    {
        static void Main(string[] args)
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


            //O(n^2) space
            List<int>[] indexPointers = new List<int>[values.Count];
            //Tried to do it with O(n) space but I couldn't
            //List<int> IndexPointers_n0 = new List<int>() { 0 };
            //List<int> IndexPointers_n1 = new List<int>() { 0 };

            int Aggregated_n0 = values[0];
            int Aggregated_n1 = values[1];
            indexPointers[0] = new List<int>() { 0 };
            indexPointers[1] = new List<int>() { 0 };

            

            for (int i = 2; i < values.Count; i++)
            {
                int currentSum = 0;
                //List<int> currentPointers = null;  //Tried to do it with O(n) space but I couldn't
                if (values[i] + Aggregated_n0 > Aggregated_n1)
                {
                    currentSum = values[i] + Aggregated_n0;
                    indexPointers[i] = new List<int>(indexPointers[i - 2]);
                    indexPointers[i].Add(i);
                    //Tried to do it with O(n) space but I couldn't
                    //currentPointers = IndexPointers_n0;
                    //currentPointers.Add(i);
                }
                else
                {
                    currentSum = Aggregated_n1;
                    indexPointers[i] = new List<int>(indexPointers[i - 1]);
                    //Tried to do it with O(n) space but I couldn't
                    //currentPointers = IndexPointers_n1;
                }

                //Swap values to maintain n0 and n1
                Aggregated_n0 = Aggregated_n1;
                Aggregated_n1 = currentSum;
                //Tried to do it with O(n) space but I couldn't
                //IndexPointers_n0 = IndexPointers_n1;
                //IndexPointers_n1 = currentPointers;
            }


            //Iterate through the IndexPointer array to reconstruct the Indexes Used
            List<int> IndexesUsed = new List<int>();
            int index = values.Count - 1;

           

            Console.WriteLine(string.Join(" ",IndexesUsed));
        }

     
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Algorithms_DataStructures.Knapsack_Problem
{
    class KnapsackProblem
    {
		//Rename to Main to use it
		static void Main_Renamed(string[] args)
		{
			var rootDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

			//string[] StringArray = File.ReadAllLines(rootDir + @"\Knapsack Problem\stanford_knapsack1.txt");
			//USE THIS FOR A LARGE DATASET
			string[] StringArray = File.ReadAllLines(rootDir + @"\Knapsack Problem\stanford_knapsack_big.txt");
			/*string[] StringArray  = new string[] { "10 4",
													"1 2",
													"4 3",
													"5 6",
													"6 7" };
			*/


			int capacity = 0;
			int[,] items = null;
			////// LOAD DATA .txt ///////////////
			int NumberOfItems = -1; //First row in the txt. is the header, not an item.
			foreach (string s in StringArray)
			{
				string[] splits = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				if (NumberOfItems == -1)
				{
					capacity = Convert.ToInt32(splits[0]);
					items = new int[Convert.ToInt32(splits[1]), Convert.ToInt32(splits[1])];
				}
				else
				{
					items[NumberOfItems, 0] = Convert.ToInt32(splits[0]);
					items[NumberOfItems, 1] = Convert.ToInt32(splits[1]);
				}

				NumberOfItems++;
			}
			////////////////////////////////////


            int[] matrixPrev = new int[capacity + 1];
            int[] matrixCurrent = new int[capacity + 1];


	    //LOADS 0s as it were rows[0] values for each capacity
            for (int i = 0; i < NumberOfItems + 1; i++)
                matrixPrev[i] = 0;

	    //O(nm) | O(1)
            for (int i = 0; i < NumberOfItems; i++)
            {
                int value = items[i, 0];
                int weight = items[i, 1];

                for (int cap = 0; cap < capacity + 1; cap++)
                {
                    int newValue = 0;
                    if (weight <= cap)
                    {
                        newValue = value;  //Only 1 is allowed
                        newValue = newValue + matrixPrev[cap - weight];
                    }
                    matrixCurrent[cap] = Math.Max(matrixPrev[cap], newValue);
                }

                //SWAP ROWS
                matrixPrev = matrixCurrent;
                matrixCurrent = new int[capacity + 1];
            }

			Console.WriteLine(matrixPrev[capacity]); //Not using matrixCurrent because that has been alrady swapped.

		}

    
    }
}

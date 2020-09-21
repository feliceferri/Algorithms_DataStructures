using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Algorithms_DataStructures.Knapsack_Problem
{
    class KnapsackProblem
    {
		//Rename to Main to use it
		static void Main(string[] args)
		{
			var rootDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

			string[] StringArray = File.ReadAllLines(rootDir + @"\Knapsack Problem\stanford_knapsack1.txt");
			//USE THIS FOR A LARGE DATASET
			//string[] StringArray = File.ReadAllLines(rootDir + @"\Knapsack Problem\stanford_knapsack_big.txt");

			
			int capacity = 0;
			int[,] items = null;
			////// LOAD DATA .txt into SortedDictionary
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

		

				int[,] matrix = new int[items.GetLength(0) + 1, capacity + 1]; //Adding Empty Row at the top
																			   //and 0 as a first column

			    
				for (int i = 0; i < matrix.GetLength(0); i++)
					matrix[0, i] = 0;

				for (int i = 0; i < items.GetLength(0); i++)
				{
					int value = items[i, 0];
					int weight = items[i, 1];
					int matrixRow = i + 1; //because I added an empty at the beggining.

					for (int cap = 0; cap < matrix.GetLength(1); cap++)
					{
						int newValue = 0;
						if (weight <= cap)
						{
							newValue = value;  //Only 1 is allowed
							newValue = newValue + matrix[matrixRow - 1, cap - weight];
						}
						matrix[matrixRow, cap] = Math.Max(matrix[matrixRow - 1, cap], newValue);

					}
				}

				var x = 12;
			}

		//answer question 1 : 2493893

	}
}

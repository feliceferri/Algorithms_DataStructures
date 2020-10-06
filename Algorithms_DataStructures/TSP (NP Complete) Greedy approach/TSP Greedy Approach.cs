using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Algorithms_DataStructures.TSP__NP_Complete__Greedy_approach
{
    class TSP_Greedy_Approach
    {
        public class Location
        {
            public int Index { get; set; }
            public double X { get; set; }
            public double Y { get; set; }

            public Location(int index,  double x, double y)
            {
                this.X = x;
                this.Y = y;
                this.Index = index;
            }
        }
        private static void Main(string[] args)
        {
            int vertices = 0;
            List<Location> LocationsToVisit = null;

            //Load Data from file
            var rootDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string[] StringArray = File.ReadAllLines(rootDir + @"\TSP (NP Complete) Greedy approach\stanford_TSP2.txt");

            int iRow = 0;
            foreach (string s in StringArray)
            {
                string[] splits = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (iRow == 0)
                {
                    vertices = Convert.ToInt32(splits[0]);
                    LocationsToVisit = new List<Location>(vertices);
                }
                else
                {
                    LocationsToVisit.Add(new Location(Convert.ToInt32(splits[0]), Convert.ToDouble(splits[1]), Convert.ToDouble(splits[2])));
                }
                iRow++;
            }
            /////////////////////////////////////////////////////

            Location Source = LocationsToVisit[0];
            Location currentLocation = Source;
            LocationsToVisit.Remove(currentLocation);

            Double totalCost = 0;

            while(LocationsToVisit.Count > 0)
            {
                Double Distance;
                (currentLocation, Distance) = GetClosestCity(currentLocation, LocationsToVisit);
                LocationsToVisit.Remove(currentLocation);
                totalCost += Distance;
            }


            //Adding Trip back to Source Vertice
            totalCost += Distance(currentLocation, Source);

            Console.WriteLine("Total Cost:" + totalCost);

        }

      
        
        /// Get the closest city by Euclidean Distance, if there is a match, the one with lowest index wins
        private static (Location, double) GetClosestCity(Location SourceCity, List<Location> LocationsToVisit)
        {
            Location res = null;
            double MinDistance = double.MaxValue;

            foreach(Location loc in LocationsToVisit)
            {

                double distance = Distance(SourceCity, loc);
                if (distance < MinDistance)
                {
                    MinDistance = distance;
                    res = loc;
                }
                else if (distance == MinDistance)
                {
                    if(loc.Index < res.Index)
                    {
                        res = loc;
                    }
                }
            }

            return (res, MinDistance);
        }

        private static double Distance(Location A, Location B)
        {
           return Math.Sqrt(Math.Pow(A.X - B.X, 2) + Math.Pow(A.Y - B.Y, 2));
        }
    }
}

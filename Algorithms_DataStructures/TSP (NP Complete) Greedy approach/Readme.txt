Travel Salesman Problem (Greedy Approach)

O(n^2) | O(n)   

* This was part of the 4rd Module of the Stanford Online Algorithms & Data Structures Specialization

** This code reflects a very palpable Times Vs Space dilemma.
   Current code is cuadratic in time, because at each iteration we are looking for the next closest city (Euclidean distance)
   Can't create a map of all the distances between cities which will end up being Cuadratic in Space, because there
   are 33708 cities in the stanford sample, did the math and at 8 bytes per doudble that would end up being about 8.5 GB

*** Its important to remember that the Travel Salesman Problem is an NP Complete problem, so this greedy approach its an approximation
    for result, there are different kind of approximations for the TSP.

STEPS
1)  Fill a LocationsToVisit array with all the cities
2)	Start the tour in the First City and remove it from the LocationsToVisit array.
3)	Calculate the Euclidean Distance between this and all the other LocationsToVisit
4)	Select the closest one
5)  Add that distance to the Total Cost of the tour
6)  Remove the city from the LocationsToVisit array
7)  Go to (3) using the city selected in (4)
8)  Repeat until LocationsToVisit is empty
9)  Add the distance between the last city visited and the First City





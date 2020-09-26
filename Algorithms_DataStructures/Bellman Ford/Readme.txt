Bellman Ford algorithm 

O(NM) | O (N) For Single Source

** Can be used for All Pais Comparison (as Floyd-Warshall Algorithm does)
O(N^2xM) | O(N) For All Pairs Comparison (Will iterate through each source)
O(NxM) | O(N) For All Pairs Comparison with a TRICK => Creating an External Vertice that will have an edge to all vertices, so there wont be unreachable vertices.

* Dynamic Programming
** This was part of the 4th Module of the Stanford Online Algorithms & Data Structures Specialization

- Graph Algorithm
- Allows cycles
- Allows negative cost edges
- Doesn't allow negative cost cycles, but it detects them.


Context
- It has to Iterate through each vertice at least once, and if there is no negative cost cycles there will be a moment where the paths costs are not
  minimized anymore, at that moment we can exit.
- In the initial for loop we added +1 cycle, so if it gets there it means there is a negative cost cycle, as it will never stop, because in each
  cycle the some of path costs are minimizing further and further due to the negative cost cycle.

STEPS - Single Source
1) Loop N (vertices) time + 1
2) Check if we are in the +1 Iteration it means there is a negative cost cycle.
3) Create an Array to store the shortest path from the Source Vertice to the Head Vertice
4) Initialize that array with Int.MaxValue or Infinity for all its items
5) Set the Source Vertice as value 0 in that array.
6) Loop through each Vertice 
7) If that Vertice already have a "base" value in the array, I mean if its other than MaxValue or Inifinity then
8) Loop through each Edge wich tail is that vertice 
9) If the "base" value of that vertice (which really related to the cost from the source to it) + cost of the edge < the "base" value of the Head of the edge then
   assign this new value: Head = Tail + Edge Cost, and mark a flag that states a Minimization has ocurred
10) outside the two loops check if no minimization ocurred you can exit and return the values


NOTE: for All Pairs
- A first Loop can be done that will iterate trough each vertice setting it as the Source vertice, that will be N^2 X M
- An improved trick is not to create a third loop, but to create a new "external vertice" which edges to all existing vertices, so no vertice is unreacheable.

- In the code you will find this Improved version as the default, and the N^2xM as Commented lines.



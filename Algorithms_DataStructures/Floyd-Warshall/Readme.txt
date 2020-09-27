Floyd Warshall algorithm 

O(N^3) | O (N^2) 

* Dynamic Programming
** This was part of the 4th Module of the Stanford Online Algorithms & Data Structures Specialization

- Graph Algorithm
- Allows cycles
- Allows negative cost edges
- Doesn't allow negative cost cycles, but it detects them, as the value from a node to itself should be 0, if is less than 0 then there is a negative cycle.


It stores the shortest distance between each vertice in a 2D Matrix.
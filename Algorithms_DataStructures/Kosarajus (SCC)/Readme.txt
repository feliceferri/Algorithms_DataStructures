Kosaraju's (Strongly Connected Components)

O(2N) | O(N)   

* This was part of the 2nd Module of the Stanford Online Algorithms & Data Structures Specialization


SCC: Group of nodes where from each of them all the other ones can be reached.
     (Only applies for Directed Graphs)
     If is a node with only input and no output is called "Sink", and its not part of the SCC


STEPS
1) Reverse all the directions of all the edges in the Graph (Transpose/Invert).
2) Set the Score at 0
3) Iterate through each of the nodes once, checking in a hastable if the node has not been visited previously
4) If the node has not been visited, then Do a Depth First Search the last node found in the DFS will be scored at Score++
5) As DFS backtracks will be adding Score++ to all the parents. So the node that started the DFS will have the highest Score
6) Continue looping until all nodes has been visited and have a Score assigned to them.
7) Order the Nodes based on the Score from highest to lowest
8) Iterate through all the nodes another time based on the Scoring Order (7)
9) Explored them using again DFS but this time in the Original Graph, not the Transpose/Inverted one
10) Everything that can be reached in the DFS is part of that specific Strongly Connected Component.


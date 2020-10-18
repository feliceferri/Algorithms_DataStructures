2-SAT Problem (using Kosaraju's Strongly Connected Components)

O(V+E) | O(N)  

* This is a beutiful problem to solve.
** In this example only "OR" Literals are used.
*** In order to create the Graph you need to know the following about OR Literals.
    For each Literal two edges have to be created in the Graph, depending on the Positive/Negative value the edges should be created in the followay manner:
    A OR B : 
         Edge 1: -A =>  B
         Edge 2: -B =>  A
    -A OR B:
         Edge 1:  A =>  B
         Edge 2: -B => -A
    -A OR -B:
         Edge 1:  A => -B
         Edge 2:  B => -A
    -A OR -B:
         Edge 1: -A => -B
         Edge 2:  B => A

**** The problem is not satisfiable if:
     1) We found a direct Edge from A => -A, or from -A => A
     2) If we found A and -A in the same strongly connected component.




Max Weight Independent Set of a Graph
(Max Subset of non adjacent numbers)

O(n) | O(n^2)  **The reason is cuadratic space is because to print the actual subset, we need to store the array of steps for each n
			   **I tried to do it saving the steps for the previous two positions but is not enough there are jumps that are over 2 spots
			   **and you dont know it until you have pass it.

* Dynamic Programming
** This was part of the 3rd Module of the Stanford Online Algorithms & Data Structures Specialization

1) Easier approach would be to create a 2nd array to store the Sums, I did it in O(1) by only saving the last values
2) Iterate through each of the indexes
3) At each position peek the max between the previous accumulated sum and the sum between current value (i) and i-2  (no adjacency)
4) The last aggregated value is the total sum of the no adjacency subset.

Now to display/reconstruct the subset is another game
1) Create an array of List<int>, because at each index we need to store all the steps took to get there.
   So basically if the Max is just the value of the previous cell (i-1), then we just copy the previous List of Steps to the current (i)
   If the max is i + (i-2), then we copy that List and we add the current index (i)



Huffman's algorithm 

O(n log n) | O(n)    * Needs to sort the frequencies

* Dynamic Programming
** This was part of the 3rd Module of the Stanford Online Algorithms & Data Structures Specialization

- Compression Algorithm
- Doesn’t loose of information
- Based on a binary tree.
- Variable length encoding

The trick here is precisely the variable length encoding.
- By having variable length encoding, we saved space.
- Because of variable length encoding we have to see the frequencies of each piece of data, and use the shorter encodings for the pieces that are repeated the most.

STEPS
1)	Get the frequency of each piece of data, in my code written as weight. 
2)	Sort the weights (I used a SortedDictionary, as we would need to add and remove items from it)
3)	Start the tree creation from the bottom-top, the leaves at the bottom should be the one with the lowest frequencies (less repeated through the dataset) as the bottom leaves will have the larger encoding
4)	Pick the two with the lowest frequencies, sum the frequencies and create a top node and that sum will be its weight, assign the two chosen as right and left child.
5)	Remove the two chosen from the SortedDictionary.
6)	Add the new node with it two children to the SortedDictionary<int weight, Node node>
7)	Repeat the same until having a single Node in the Sorted Dictionary. (At each iteration we are removing two and adding one, so we are decreasing by one the total count).

I created an Extender of the Node Data Structure, so it stores the Level in the Tree and the Encoding, both are filled by doing a Breadth First Search and printed in the Console.



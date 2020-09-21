Knapsack Problem

O(mn) | O(1)  * I could achieve O(1) because I'm not reconstructing the path took, meaning I'm not listing the items selected,
               if that would be the case O(n) space will be needed to store each calculated row, and then backtract from the result. 

* Dynamic Programming
** This was part of the 3rd Module of the Stanford Online Algorithms & Data Structures Specialization

Lets assume this is done in O(n) space with a matrix.
1) Create a 2D Matrix Items +1 x each discrete capacity value +1 (if capacity is 10, then 11 columns will be created )
2) Fill 1st row values with 0, as there is where no items
3) Iterate through each item touple (Value, Weight)
4) Inner loop wich iterates through each capacity value
5) Compare if current capacity is >= item weight if not write 0 in the matrix 
6) Else => Write the Max between the sum of CurrentValue + the Value whatever weight is left (using the previous row to calculate that value) and Previous Row value at the same capacity
7) The final value will be store at the last row, last column of the matrix

** O(1) space
As at any iteration what is needed is the "previous" row, this can be done in O(1) space just by storing the current row (being calculated/filled) and
the previous row (already calculated), at the end of each iteration, the rows are swapped, so the current row becomes the previous row and the new row
is blanked


Now to reconstruct the result, meaning display which items where choosen
1) The whole matrix has to be store, so space will be O(mn)
2) Start from the result (last row, las column)
3) If value == the value at the same capacity in the previous row (i-1), dont write anything move to the the previous row
4) Repeat the same until current row value != previous row value, in which case we write down that Item
5) Then we substract the weight of that item from the current Capacity, and that delta will be the new capacity to analize, and Go To step 3)


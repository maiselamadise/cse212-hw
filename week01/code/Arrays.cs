public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // PLAN:
        // 1. Create a new array of doubles with a size equal to 'length'.
        // 2. Loop through each index of the array, from 0 up to length - 1.
        // 3. For each index i, the value is 'number' multiplied by (i + 1).
        //    The "+ 1" is needed because the first element should be number * 1,
        //    not number * 0.
        // 4. Store that value in the array at index i.
        // 5. After the loop finishes, return the filled array.

        double[] result = new double[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1);
        }
        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // PLAN:
        // 1. Rotating right by 'amount' means the last 'amount' items move to
        //    the front of the list, and the remaining items follow them in order.
        // 2. Find the index where the tail section starts:
        //    splitIndex = data.Count - amount.
        // 3. Use GetRange to copy the tail section (the last 'amount' items)
        //    into a new temporary list.
        // 4. Use RemoveRange to delete that tail section from the end of the
        //    original list.
        // 5. Use InsertRange at index 0 to put the saved tail section at the
        //    front of the original list.
        // 6. The original list is now rotated, so nothing needs to be returned.
        //    (If amount equals data.Count, the tail is the whole list, so the
        //    result is unchanged, which is correct for a full rotation.)
        
        int splitIndex = data.Count - amount;
        List<int> tail = data.GetRange(splitIndex, amount);
        data.RemoveRange(splitIndex, amount);
        data.InsertRange(0, tail);
    }
}

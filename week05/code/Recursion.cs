using System.Collections;

public static class Recursion
{
    /// <summary>
    /// #############
    /// # Problem 1 #
    /// #############
    /// Using recursion, find the sum of 1^2 + 2^2 + 3^2 + ... + n^2
    /// and return it.  If n <= 0, just return 0.  No loops.
    /// </summary>
    public static int SumSquaresRecursive(int n)
    {
        // Base case
        if (n <= 0)
            return 0;

        // Recursive case: n^2 plus the sum for a smaller problem
        return (n * n) + SumSquaresRecursive(n - 1);
    }

    /// <summary>
    /// #############
    /// # Problem 2 #
    /// #############
    /// Using recursion, insert permutations of length 'size' from a list
    /// of 'letters' into the results list.  Each letter is unique.
    /// Example: letters = ABC, size = 2 -> AB, AC, BA, BC, CA, CB
    /// </summary>
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        // Base case: the word has reached the requested length
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        // Try every letter that has not been used yet
        for (int i = 0; i < letters.Length; i++)
        {
            if (!word.Contains(letters[i]))
                PermutationsChoose(results, letters, size, word + letters[i]);
        }
    }

    /// <summary>
    /// #############
    /// # Problem 3 #
    /// #############
    /// Count the ways to climb 's' stairs taking 1, 2, or 3 steps at a time.
    /// CountWaysToClimb(s) = CountWaysToClimb(s-1) + CountWaysToClimb(s-2) + CountWaysToClimb(s-3)
    /// Uses memoization via the 'remember' dictionary.
    /// </summary>
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        // Base Cases
        if (s == 0)
            return 0;
        if (s == 1)
            return 1;
        if (s == 2)
            return 2;
        if (s == 3)
            return 4;

        // Create the cache on the first call
        remember ??= new Dictionary<int, decimal>();

        // Return the cached answer if we already solved this size
        if (remember.ContainsKey(s))
            return remember[s];

        // Solve using recursion, passing the cache down
        decimal ways = CountWaysToClimb(s - 1, remember)
                     + CountWaysToClimb(s - 2, remember)
                     + CountWaysToClimb(s - 3, remember);

        // Save the answer for next time
        remember[s] = ways;
        return ways;
    }

    /// <summary>
    /// #############
    /// # Problem 4 #
    /// #############
    /// Using recursion, insert all possible binary strings for a pattern
    /// containing '*' wildcards into the results list.
    /// Example: 1**1 -> 1001, 1011, 1101, 1111
    /// </summary>
    public static void WildcardBinary(string pattern, List<string> results)
    {
        int index = pattern.IndexOf('*');

        // Base case: no wildcards left, so this is a finished binary string
        if (index == -1)
        {
            results.Add(pattern);
            return;
        }

        // Replace the first wildcard with 0, then with 1
        WildcardBinary(pattern[..index] + "0" + pattern[(index + 1)..], results);
        WildcardBinary(pattern[..index] + "1" + pattern[(index + 1)..], results);
    }

    /// <summary>
    /// Use recursion to insert all paths that start at (0,0) and end at the
    /// 'end' square into the results list.
    /// </summary>
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        // If this is the first time running the function, then we need
        // to initialize the currPath list.
        if (currPath == null)
        {
            currPath = new List<ValueTuple<int, int>>();
        }

        // Step onto the current square
        currPath.Add((x, y));

        if (maze.IsEnd(x, y))
        {
            // Base case: found a complete solution
            results.Add(currPath.AsString());
        }
        else
        {
            // Try right, down, left, up
            var moves = new (int dx, int dy)[] { (1, 0), (0, 1), (-1, 0), (0, -1) };

            foreach (var (dx, dy) in moves)
            {
                int nx = x + dx;
                int ny = y + dy;

                if (maze.IsValidMove(currPath, nx, ny))
                    SolveMaze(results, maze, nx, ny, currPath);
            }
        }

        // Backtrack: step off this square so other paths can use it
        currPath.RemoveAt(currPath.Count - 1);
    }
}
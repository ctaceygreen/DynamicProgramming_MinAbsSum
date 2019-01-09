using System;
using System.Linq;
// you can also use other imports, for example:
// using System.Collections.Generic;

// you can write to stdout for debugging purposes, e.g.
// Console.WriteLine("this is a debug message");

class Solution
{
    public static void Main(string[] args)
    {
        int[] intArgs = new int[args.Length];
        for(int i = 0; i < args.Length; i++)
        {
            intArgs[i] = int.Parse(args[i]);
        }
        solution(intArgs);
    }
    public static int solution(int[] A)
    {
        // write your code in C# 6.0 with .NET 4.5 (Mono)
        int N = A.Length;
        int max = 0;
        int sum = 0;
        //Make each element absolute value as we don't care about negatives or positives as we are allowed to use either in the result
        for(int i = 0; i < N; i++)
        {
            A[i] = Math.Abs(A[i]);
            max = Math.Max(max, A[i]); // Also get the max value to be used later
            sum += A[i];
        }

        //Get the count of each value in A, as the range is so low that repeated values are likely.
        int[] count = new int[max + 1];
        for(int i = 0; i < A.Length; i++)
        {
            count[A[i]]++;
        }

        //DP array holds whether each sum can be reached. If not, -1. If it can, the value
        int[] dp = new int[sum + 1];
        for(int i = 0; i < sum + 1; i++)
        {
            dp[i] = -1;
        }
        dp[0] = 0; // Important 
        for(int a = 1; a < max + 1; a++)
        {
            if(count[a] > 0)
            {
                for(int j = 0; j < sum; j++)
                {
                    if(dp[j] >= 0)
                    {
                        //Then sum has already been reached, so simply set the value to the count of our a
                        dp[j] = count[a];
                    }
                    else if(j >= a && dp[j - a] > 0)
                    {
                        //The sum has yet to be reached. Our element is less than the sum so can be used. The addition of our element to get here still has enough count left. 
                        dp[j] = dp[j - a] - 1; // Set this sum to be reachable, and that we now have 1 less than the count before to be used.
                    }
                }
            }
        }
        int result = sum;
        //Simply loop through dp to find the element within the array which is closest to S / 2. 
        //By being closest to S/2, our actual set would be positive this value, minus the rest of the elements, to get the minimum abs value. But that's not required to return.
        for(int i = 0; i < (sum / 2) + 1; i++)
        {
            if(dp[i] >= 0)
            {
                //Then we were able to make this sum
                result = Math.Min(result, sum - 2 * i); // The min difference between the two sets
            }
        }
        return result;

    }
}
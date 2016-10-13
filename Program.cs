using System;
using System.Collections.Generic;

namespace ConsoleApplication
{

    public class Program
    {
        public static void Main(string[] args)
        {   
            var arr = new int[] {4, 2, 2, 5, 1, 5, 8};
            Console.WriteLine (MinAvgTwoSlice (arr));
        }




        

        #region Lesson 16 Greedy algorithms

        static int MaxNonoverlappingSegments (int[] A, int[] B)
        {
            if (A.Length == 0)
                return 0;
                
            int res = 1;
            int lastEnd = B[0];

            for (int i = 1; i < B.Length; i++)
            {
                if (lastEnd < A[i])
                {
                    res++;
                    lastEnd = B[i];
                }
            }
            
            return res;
        }        

        static int TieRopes (int K, int[] A)
        {
            long sum = 0;
            int res = 0;

            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] >= K || 
                   sum + A[i] >= K
                   )
                {
                    res++;
                    sum = 0;
                }
                else
                {
                    sum += A[i];
                }
            }

            return res;
        }

        #endregion

        #region Lesson 15 Caterpillar method

        static int MinAbsSumOfTwo (int[] A)
        {
            if (A.Length == 1)
                return Math.Abs (A[0] + A[0]);

            int res = Int32.MaxValue;
            Array.Sort (A);

            int p1 = 0;
            int p2 = A.Length - 1;

            while (p1 < p2)
            {
                int current = Math.Abs(A[p1] + A[p2]);
                int left = Math.Abs(A[p1 + 1] + A[p2]);
                int right = Math.Abs(A[p1] + A[p2 - 1]);

                if (left < current)
                {
                    res = Math.Min (res, Math.Min (Math.Abs (A[p1] + A[p1]), left));
                    p1 ++;
                }
                else if (right < current)
                {
                    res = Math.Min (res, Math.Min (Math.Abs (A[p2] + A[p2]), right));                    
                    p2 --;
                }
                else
                {
                    res = Math.Min (res, current); 
                    p1 ++;
                    p2 --;
                }   
                             
            }

            return res;
        }

        static int CountDistinctSlices (int[] A)
        {
            HashSet<int> hash = new HashSet<int> ();
            long p1 = 0;
            long p2 = 0;
            long res = 0;

            while (p1 < A.Length && p2 < A.Length)
            {
                if (!hash.Contains (A[p1]))
                {
                    hash.Add (A[p1]);
                    res += 1 + (p1 - p2);
                    p1 ++;
                }
                else
                {
                    hash.Remove (A[p2]);
                    p2 ++;
                }
            }

            return Convert.ToInt32 (res);
        }

        static int CountTriangles (int[] A)
        {
            Array.Sort (A);
            int res = 0;

            // Fix first element.
            for (int i = 0; i < A.Length - 2; i++)
            {
                int k = i + 2;

                // Fix second element.
                for (int j = i + 1; j < A.Length; j++)
                {
                    while (k < A.Length && A[i] + A[j] > A[k])
                        k++;
                    res += k - j - 1;    
                }                
            }

            return res;
        }

        static int AbsDistinct (int[] A)
        {
            return 0;        
        }

        #endregion

        #region Lesson 14 Binary search algorithm

        static int MinMaxDivision (int K, int M, int[] A)
        {
            int[] p = new int[A.Length];
            int max = 0, sum = 0;

            for (int i = 0; i < A.Length; i++)
            {
                sum += A[i];
                max = Math.Max (max, A[i]);
            }

            int left = max;
            int right = sum;

            while (left <= right)
            {
                int mid = (left + right) >> 1;
                int intervals = CountIntervals(A, mid);
                if (intervals > K)
                    left = mid + 1;
                else
                    right = mid - 1;   
            }

            return left;
        }

        static int CountIntervals(int[] A, int target) 
        {
            int sum = 0, count = 0;
            for (int i = 0; i < A.Length; i++) 
            {
                sum += A[i];
                if (sum > target) {
                    count++;
                    sum = A[i];
                }
            }
            return count + (sum > 0 ? 1 : 0);
        }

        #endregion

        #region Lesson 10 Prime and composite numbers

        static int Flags (int[] A)
        {
            int maxNum = Convert.ToInt32 (Math.Sqrt (A.Length - 1));
            int lastDistance = -1;
            int result = 0;

            for (int i = 1; i < A.Length - 1; i++)
            {
                if (A[i] > A[i -1] && A[i] > A[i + 1])
                {
                    if (lastDistance == -1)
                        lastDistance = 0;
                    /// else 
                        //lastDistance = Min;  
                }
            }

            return 0;
        }

        static int CountFactors (int N)
        {
            if (N == 1)
                return 1;

            int result = 0;
            int i = 1;

            while (i * i < N)
            {
                if (N % i == 0)
                    result += 2;

                i++;

                if (i * i == N)
                    result += 1;
            }

            return result;            
        }

        static int MinPerimeterRectangle (int N)
        {
            if (N == 1)
            {
                return 2 * (1 + N/1);
            }

            int result = Int32.MaxValue;
            int i = 1;

            while (i * i < N)
            {
                if (N % i == 0)
                    result = Math.Min (result, 2 * (i + N/i));

                i++;

                if (i * i == N)
                    result = Math.Min (result, 4*i); // box case!
            }

            return result;
        }

        #endregion

        #region Lesson 9 Maximum slice problem

        static int MaxProfit (int[] A)
        {
            int maxEnding = 0;
            int maxSlice = 0;            
            int len = A.Length;
            int[] delta = new int[len];

            for (int i = 1; i < len; i++)
            {
                delta[i] = A[i] - A[i -1]; 
            }

            foreach (var item in delta)
            {
                maxEnding = Math.Max (0, maxEnding + item);
                maxSlice = Math.Max (maxEnding, maxSlice);
            }

            return maxSlice;
        }

        static int MaxDoubleSliceSum (int[] A)
        {
            int len = A.Length;
            int maxSlice = 0;
            int[] leftMaxSum = new int[len];
            int[] rightMaxSum = new int[len];

            for (int i = 1; i < len - 1; i++)
            {
                leftMaxSum[i] = Math.Max (0, leftMaxSum[i-1] + A[i]);     
            }

            for (int i = len - 2; i > 0; i--)
            {
                rightMaxSum[i] = Math.Max (0 , rightMaxSum[i+1] + A[i]); 
            }   

            for (int i = 1; i < len - 1; i++)
            {
                maxSlice = Math.Max (maxSlice, leftMaxSum [i - 1] + rightMaxSum[i + 1]);
            }

            return maxSlice;
        }        

        static int MaxSliceSum (int[] A)
        {
            int maxSlice = 0;
            int maxEnding = 0;
            int max = A[0];

            // get max int.
            for (int i = 1; i < A.Length; i++)
            {
                max = A[i] > max ? A[i] : max;
            }

            if (max < 0)
                return max;

            foreach (var item in A)
            {
                maxEnding = Math.Max (0, maxEnding + item);
                maxSlice = Math.Max (maxEnding, maxSlice);
            }

            return maxSlice;
        }

        #endregion

        #region Lesson 8 Leader 
        static int Dominator (int[] A)
        {
            int candidate = -1;
            Stack<int> stack = new Stack<int> ();

            // get leader or dominator
            foreach (var item in A)
            {
                if (stack.Count == 0)
                {
                    stack.Push (item);
                }
                else 
                {
                    if (stack.Peek () != item)
                        stack.Pop ();
                    else
                        stack.Push (item);    
                }                      
            }

            // if no leader exist, return -1
            if (stack.Count == 0)
                return -1;
            else
                candidate = stack.Pop ();


            int firstIndex = -1;
            int count = 0;

            // check candidate correctness and grab
            // first index.
            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] == candidate)
                {
                    count++;
                    if (firstIndex == -1)
                        firstIndex = i;
                }
            }

            if (count > A.Length / 2)
                return firstIndex;

            return -1;
        }

        static int EquiLeader (int[] A)
        {
            int result = 0;
            int candidate = -1;
            int total = 0;
            int len = A.Length;
            Stack<int> stack = new Stack<int> ();

            // get leader
            foreach (var item in A)
            {
                if (stack.Count == 0)
                {
                    stack.Push (item);
                }
                else 
                {
                    if (stack.Peek () != item)
                        stack.Pop ();
                    else
                        stack.Push (item);    
                }                      
            }

            // if no leader exist, no posibilities
            if (stack.Count == 0)
                return 0;
            else
                candidate = stack.Pop ();

            for (var i = 0; i < len; i++)
            {
                if (A[i] == candidate)
                    total++;
            }  

            if (total > len/2)
            {
                // leader exists!
                int count = 0;
                for (var i = 0; i < len; i++)
                {
                    if (A[i] == candidate)
                        count++;

                    if (count > (i + 1) / 2 && 
                       (total - count) > (len - (i + 1))/2)
                    {
                        result++;   
                    }    
                }     
            }
            else 
            {
                return 0;
            }

            return result;
        }

        #endregion

        #region Lesson 7 Stacks and Queues
        // Solution at: http://blog.codility.com/2012/06/sigma-2012-codility-programming.html
        static int StoneWall (int[] H)
        {
            int result = 0;
            Stack<int> stack = new Stack<int> ();

            foreach (var item in H)
            {
                while (stack.Count > 0 && item < stack.Peek()) 
                    stack.Pop();

                if(stack.Count == 0 || item > stack.Peek()) 
                {
                    stack.Push(item);
                    result++;
                }                 
            }

            return result;
        }

        static int Nesting (string S)
        {
            // same as brackets!
            Stack<Char> stack = new Stack<Char> ();
            Char c, p;

            for (int i = 0; i < S.Length; i++)
            {
                c = S[i];
                if (c == '(' )
                {
                    stack.Push (c);
                }
                else
                {
                    if (stack.Count == 0)
                        return 0;

                    p = stack.Pop ();
                    if (p == '(' && c != ')')
                        return 0;
                }    
            }

            return stack.Count == 0 ? 1 : 0;
        }

        static int Brackets (string S)
        {
            Stack<Char> stack = new Stack<Char> ();
            Char c, p;

            for (int i = 0; i < S.Length; i++)
            {
                c = S[i];
                if (c == '{' || 
                    c == '(' || 
                    c == '[')
                    {
                        stack.Push (c);
                    }
                    else
                    {
                       if (stack.Count == 0)
                          return 0;

                       p = stack.Pop ();
                       if ((p == '{' && c != '}') || 
                           (p == '[' && c != ']') ||
                           (p == '(' && c != ')'))
                            {
                                return 0;
                            } 
                    }    
            }

            return stack.Count == 0 ? 1 : 0;
        }

        static int Fish (int[] A, int[] B)
        {            
            int P, Q;
            int result = 0;
            Stack<int> stack = new Stack<int> ();

            for (int i = 0; i < A.Length - 1; i++)
            {
                P = i;
                Q = i + 1;

                // fight downstream!
                if (B[P] == 1 && B[Q] == 0)
                {
                    if (A[P] > A[Q])
                    {
                        B[Q] = B[P];
                        result++;
                        continue;
                    }
                    else
                    {
                        // fight upstream!  
                        while (stack.Count > 0 && stack.Peek () != 0)
                        {
                            int fish = stack.Pop ();
                        }        
                    }   
                }
            }

            return result;
        }

        #endregion

        #region Lesson 5 Sorting

        // TODO:
        // NumberOfDiscIntersections

        static int Triangle (int[] A)
        {
            if (A.Length < 3)
                return 0;

            long P, Q, R;
            Array.Sort (A);

            for (int i = 1; i < A.Length - 1; i++)
            {
                P = A[i -1];
                Q = A[i];
                R = A[i + 1];
                
                if ( P + Q > R &&
                     Q + R > P &&
                     R + P > Q)
                     return 1;
            }
            return 0;
        }

        static int MaxProductOfThree (int[] A)
        {
            int n = A.Length;
            Array.Sort (A);

            // corner case 1
            if (A[n-1] > 0 && (A[0] * A[1] > A[n-3] * A[n-2]))
                return A[0] * A[1] * A[n-1];    

            return A[n-3] * A[n-2] * A[n-1];                        
        }        

        static int Distinct (int[] A)
        {
            // corner case!
            if (A.Length == 0)
                return 0;

            int result = 1;
            Array.Sort (A);

            for (int i = 1; i < A.Length; i++)
            {
                if (A[i] != A[i -1])
                    result++;
            }

            return result;
        }

        #endregion
        
        #region Lesson 4 Prefix sums

        static int MinAvgTwoSlice (int[] A)
        {
            int res = 0;
            int min = Int32.MaxValue;

            // Empirically, is demostrable that the best combination
            // is the minor pair a + b / 2 . Find it and return index.
            for (int i = 1; i < A.Length; i++)
            {
                if ((A[i] + A[i - 1])/2 < min)
                {
                    min = (A[i] + A[i - 1])/2;
                    res = i - 1;
                }
            }

            return res;
        }

        static int[] GenomicRangeQuery (string S, int[] P, int[] Q)
        {
            int len = S.Length + 1;
            int[,] pref = new int[len,4];
            int[] result = new int[P.Length];

            // create multidim array to save
            // 4 counters.
            for(int i = 1; i < len; i++)
            {
                char c = S[i - 1];
                if(c == 'A') pref[i,0] = 1;
                if(c == 'C') pref[i,1] = 1;
                if(c == 'G') pref[i,2] = 1;
                if(c == 'T') pref[i,3] = 1;
            } 

            // compute prefix    
            for(int i = 1; i < len; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    pref[i,j] += pref[i-1,j];
                }
            }	

            for(int i = 0; i < P.Length; i++)
            {
                int x = P[i];
                int y = Q[i];

                for(int a = 0; a < 4; a++)
                {
                    if(pref[y + 1,a] - pref[x,a] > 0){
                        result[i] = a + 1;
                        break;
                    }
                }
            }   

            return result;
        }

        static int CountDiv (int A, int B, int K)
        {              
            var rest = (A % K == 0) ? 1 : 0; 
            return B/K - A/K + rest; 
        }

        static int PassingCars (int[] A)
        {
            int result = 0;

            // prefix sum calculation.
            int[] pref = new int[A.Length + 1];
            for (int i = 1; i < pref.Length; i++)
            {
                pref[i] = pref[i -1] + A[i - 1];
            }    

            for (int i = 0; i < A.Length; i++)
            {
                // A[i] == 0 slice starts.
                // A.Length slice ends.
                if (A[i] == 0)
                    result += pref[A.Length] - pref[i];
                    // pref[end + 1] - pref[start]

                // corner condition
                if (result > 1000000000)
                    return -1;    
            }
            
            return result;
        }

        static int PassingCars_nonStandardSolution (int[] A)
        {
            int ones = 0;
            int result = 0;

            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] == 1)
                    ones++;
            }

            // corner condition.
            if (ones == 0)
                return 0;

            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] == 0) 
                {
                    result += ones;        
                }
                else
                {
                     ones -= 1;   
                }

                // corner condition ?
                if (result > 1000000000)
                    return -1;
            }

            return result;
        }

        #endregion

        #region Lesson 4 Counting Elements

        // not ok. research!
        static int[] MaxCounters (int N, int[] A)
        {
            int[] counters = new int[N];                
            int[] diff = new int[N];   
            int baseValue = 0;
            int max = -1;

            for (int i = 0; i < A.Length; i++)
            {
                if (1 <= A[i] && A[i] <= N)
                {                     
                    int v = (counters[A[i] - 1] > baseValue) ? counters[A[i] - 1] : baseValue; 
                    counters[A[i] - 1] = v + 1;      
                    max = Math.Max (max, counters[A[i] - 1]);
                }
                else
                {
                    baseValue = max;
                }
            }

            for (int i = 0; i < counters.Length; i++)
            {
                if (counters[i] < baseValue)
                    counters[i] = baseValue;
            }

            return counters;
        }

        static int FrogRiverOne (int X, int[] A)
        {
            int counter = 0;
            HashSet<int> pool = new HashSet<int> ();

            for (int i = 0; i < A.Length; i++)
            {
                if (!pool.Contains (A[i]))
                {
                    pool.Add (A[i]);
                    counter++;
                }   

                if (counter == X)
                    return i;     
            }
            
            return -1;
        }

        static int MissingInteger (int[] A)
        {
            var lookUpArray = new bool[A.Length];

            for (int i = 0; i < A.Length; i++)
                if (A[i] > 0 && A[i] <= A.Length)
                    lookUpArray[A[i] - 1] = true;

            for (int i = 0; i < lookUpArray.Length; i++)
                if (!lookUpArray[i])
                    return i + 1;

            return A.Length + 1;
        }

        static bool PermCheck(int[] A)
        { 
            int[] B = new int[A.Length + 1];

            for (int i = 1; i < B.Length; i++)
            {
                if (A[i - 1] > B.Length - 1)
                    return false;

                if (B[A[i - 1]] != 0)
                    return false;        

                B[A[i - 1]] = A[i - 1];
            }

            return true;
        }

        #endregion

        #region Lesson 3 Time complexity

        static int PermMissingElem(int[] A)
        {
            var pool = new int[A.Length + 1];

            for (int i = 0; i < A.Length; i++)
            {
                pool[A[i] - 1] = 1;
            }

            return Array.FindIndex (pool, i => i == 0) + 1;            
        }

        static int FrogJmp(int X, int Y, int D)
        {
            return (int)decimal.Ceiling ((Y - X)/(D * 1.0M));        
        }

        static int TapeEquilibrium (int[] A)
        {
            long leftSide = A[0];
            long rightSide = 0;
            long result = long.MaxValue;

            for (int i = 1; i < A.Length; i++)
                rightSide += A[i]; 

            for (int i = 1; i < A.Length; i++)
            {    
                result = Math.Min (result, Math.Abs(leftSide - rightSide));  
                leftSide += A[i];
                rightSide -= A[i];                    
            }    
            return Convert.ToInt32 (result);
        }

        #endregion


        static int OddOccurrences (int[] A)
        {
            var max = A[0];
            for (int i = 1; i < A.Length; i++)
            {
                max = A[i] > max ? A[i] : max;
            }    

            int item;
            HashSet<int> pool = new HashSet<int>();

            for (int i = 0; i < A.Length; i++)
            {
                item = A[i];
                if (!pool.Contains (item))
                    pool.Add (item);
                else
                    pool.Remove (item);
            }
            var arr  = new int[1];
            pool.CopyTo (arr);

            return arr[0];
        }



    }
}

/*
Add as a sort snippet:
for (int i = 1; i < A.Length; i++)
{
    max = A[i] > max ? A[i] : max;
} 
*/

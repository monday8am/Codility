using System;
using System.Collections.Generic;

namespace ConsoleApplication
{

    public class Program
    {
        public static void Main(string[] args)
        {   
            /*
            var S = "CAGCCTA";
            var P = new int[] {2,5,0};
            var Q = new int[] {4,5,6};            
            var arr = GenomicRangeQuery (S, P, Q);
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine (arr[i]);
            }
            */
            var arr = new int[] {8, 8, 5, 7, 9, 8, 7, 4, 8};
            Console.WriteLine (StoneWall (arr));
        }

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
        
        // create multidim array and try again!
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

        // not ok. research!
        static int[] MaxCounters (int N, int[] A)
        {
            int[] result = new int[N];                
            int[] diff = new int[N];   
            int max = 0;

            for (int i = 0; i < A.Length; i++)
            {
                if (1 <= A[i] && A[i] <= N)
                {                        
                    result[A[i] - 1] ++;

                    if (result[A[i] - 1] > max)
                        max = result[A[i] - 1];        
                }
                else
                {
                    for (int j = 0; j < diff.Length; j++)
                    {
                        diff[j] = (max - result[j]);
                    }
                }
            }

            for (int i = 0; i < result.Length; i++)
            {
                result[i] += diff[i];
            }

            return result;
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

        static int TapeSolution (int[] A)
        {
            long leftSide = A[0];
            long rightSide = 0;
            long subValue = 0;
            long result = long.MaxValue;

            for (int i = 1; i < A.Length; i++)
                rightSide += A[i]; 

            for (int i = 1; i < A.Length; i++)
            {    
                subValue = Math.Abs(leftSide - rightSide);     
                if (subValue < result)
                    result = subValue;
                leftSide += A[i];
                rightSide -= A[i];                    
            }    
            return Convert.ToInt32 (result);
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

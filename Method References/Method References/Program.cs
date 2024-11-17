
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Method_References
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine(High("aa b"));
            Console.ReadLine();
        }


        /*Find the highest scoring word
        
        Console.WriteLine(High("Call me Ishmael"));*/
        public static string High(string s)
        {
            string sentence = s.ToUpper();
            string[] words = sentence.Split(' ');
            string result = "";
            int highestScore = 0;
            
            foreach (string word in words)
            {
                
                int wordScore = GetWordScore(word);
                
                if (wordScore > highestScore) 
                {
                    highestScore = wordScore;
                    result = word;
                }

               Console.WriteLine($"result: {result}, Score: {highestScore}");
            }
            return result;
        }


        private static int GetWordScore(string word)
        {
            
            char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            
            int score = 0;

            foreach (char c in word)
            {
                for (int i = 0; i < alphabet.Length; i++)
                {
                    int letterValue = i + 1;
                    if (alphabet[i] == c)
                    {
                        score += letterValue;
                        break;
                    }
                }
                
            }
            return score;
        }

     


        /*
        In an array of intergers find the ints that appear an odd number of times. 
        
        int[] oddArray = new int[] { 1, 1, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 4, };
        Console.WriteLine(findOddInt(oddArray));
        */
        public static int[] findOddInt(int[] seq)
        {

            /*simpler soultion 
            
             int found = 0;

            foreach (var num in seq)
            {
                found ^= num;
            }

            return found;
            */

            List<int> result = new List<int>();

            for (int i = 0; i < seq.Length; i++)
            {
                int count = 0;
                for (int j = 0; j < seq.Length; j++)
                {
                    if (seq[j] == seq[i])
                    count++;
                }
                int isEven = count % 2;
                if(isEven != 0 && !result.Contains(seq[i])) 
                {
                    result.Add(seq[i]);
                }
            }
            foreach (int i in result)
            {
                Console.WriteLine("Odd Numbers: " + i);
            }
            return result.ToArray();
        }


        /*
        returns the number of times a ball bounces in view of a hypothetical window
        Console.WriteLine(bouncingBall(10, 0.6, 10));
        */
        public static int bouncingBall(double h, double bounce, double window)
        {
            if (h <= 0 || bounce <= 0 || bounce >= 1 || window >= h) return -1;
            int result = 0;
            double newh = h;
            while (newh > window)
            {
                result++;
                newh *= bounce;
                if (newh > window) result++;
            }
            return result;
        }


        /*
         * returns a sorted array of numbers that fufil the following requirement: The sum of the individual digits to consecutive powers is equal to the number e.g 
         * 89= 8 * 1 + 9 * 9;
         * the input is a range
        */
        /*
         * Console.WriteLine(SumDigPow(0, 140506));
        */
        public static long[] SumDigPow(long a, long b)
        {
            List<long> result = new List<long>();

            for(long num = a; num <= b; num++) 
            {
                if(IsEurekaNumber(num)) 
                {
                    Console.WriteLine("Eureka Number: " + num);
                    result.Add(num);
                }
            }
            
            return result.ToArray();
        }

        private static bool IsEurekaNumber(long num)
        {
            string numStr = num.ToString();
            long sum = 0;

            for (int i = 0; i < numStr.Length; i++)
            {
                int digit = int.Parse(numStr[i].ToString());
                sum += (long)Math.Pow(digit, i + 1); 
            }

            return sum == num;
        }



        /*returns true if the numbe of x's and o's in a string are the same*/
        public static bool XO(string input)
        {
            //return new string(input.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
            //string result = new string(input.ToCharArray().Where())
            int oCount = 0;
            int xCount = 0;
            foreach (char c in input)
            {
                if (c == 'o' || c == 'O') { oCount++; }
                if (c == 'x' || c == 'X') { xCount++; }
            }

            if (oCount == xCount) return true;
            return false;
        }

        public static void ArrayHandling(string[] array)
        {
            /*Unsorted Order*/
            Console.WriteLine("Unsorted...");
            foreach (string ar in array)
            {
                Console.WriteLine($"-- {ar}");
            }
            Console.WriteLine("\n");

            /*Sorted Order*/
            SortArray(array);
            Console.WriteLine("\n");

            /*Reverse Order*/
            ReverseArray(array);
            Console.WriteLine("\n");

            /*Clear Array*/
            ClearArray(array);

        }
        public static string[] ClearArray(string[] array)
        {
            Console.WriteLine("Clear Array...");
            Array.Clear(array, 0, 4);
            foreach (string ar in array)
            {
                Console.WriteLine($"-- {ar}");
            }
            Console.WriteLine($"Array Length: {array.Length}");
            return array;
        }
        public static string[] SortArray(string[] array)
        {
            string[] SortedArray = new string[array.Length];
            Array.Sort(array);
            /*Move contents of array to SortedArray */
            Array.Copy(array, SortedArray, array.Length);

            Console.WriteLine("Sorted...");
            foreach (string ar in SortedArray)
            {
                Console.WriteLine($"-- {ar}");
            }
            return SortedArray;
        }
        public static string[] ReverseArray(string[] array)
        {
            Console.WriteLine("Reversed...");
            Array.Reverse(array);
            foreach (string ar in array)
            {
                Console.WriteLine($"-- {ar}");
            }
            return array;
        }

        public static string ToCharArray(string inputString)
        {
            Console.WriteLine("String Handling...");
            Console.WriteLine($"-- {inputString}");
            char[] stringToCharArray = inputString.ToCharArray();
            foreach (char c in stringToCharArray)
            {
                Console.WriteLine($"-- {c}");
            }
            Array.Reverse(stringToCharArray);
            string result = new string(stringToCharArray);
            Console.WriteLine($"-- {result}");
            return result;
        }

        public static void diceGame()
        {
            string playing = "No";
            Console.WriteLine("Weclome Player, would you like to start a game?");
            Console.WriteLine("Type \"Yes\" if you would like to play.");
            playing = Console.ReadLine();
            while (playing == "Yes")
            {
                int guess = 0;
                Random random = new Random();
                bool isValid = false;
                string input = "";

                while (!isValid)
                {
                    Console.WriteLine("Please enter a number between 1 & 6");
                    input = Console.ReadLine();

                    if (!int.TryParse(input, out guess))
                    {
                        Console.WriteLine("You did not enter a number");
                    }
                    else if (guess > 6)
                    {
                        Console.WriteLine("number you entered was to high");
                    }
                    else
                    {
                        isValid = true;
                    }
                }


                int.TryParse(input, out guess);



                Console.WriteLine("Starting Role");
                Loading();
                if (guess == random.Next(0, 7))
                {
                    Console.WriteLine($"Your number {guess} was correct. Well done!");
                    Console.WriteLine("If you want to play again type \"Yes\" otherwise type \"No\"");
                    playing = Console.ReadLine();
                    Loading();
                }
                else while (guess != random.Next(0, 7))
                    {
                        {
                            Console.WriteLine($"Your number {guess} was not a match. TryAgain");
                            input = Console.ReadLine();
                            int.TryParse(input, out guess);
                            Loading();
                        }

                    }
            }
        }

        public static void Loading()
        {

            string[] symbols = new string[] { "Rolling Dice", "Rolling Dice.", "Rolling Dice..", "Rolling Dice...", "Rolling Dice...." };
            int delay = 200;
            int animationduration = 5000;

            DateTime startTime = DateTime.Now;

            while ((DateTime.Now - startTime).TotalMilliseconds < animationduration)
            {

                foreach (string symbol in symbols)
                {

                    Console.Write("\r" + symbol);
                    Thread.Sleep(delay);
                }
            }
        }


        /*Multiplies an array of ints in order [5, 2, 6, 7] = 5 * 2 * 6 * 7 */
        public static int MultiplyInOrder(int[] x)
        {
            /*option 1 simple loop*/
            int result = 1;
            foreach (int num in x)
            {
                result *= num;
            }
            return result;

            /*option 2 use Linq method Aggregate */
            /*
             * 1 is the set initial value
             * acc: The accumulated result from the previous iterations.
             * val: The current value from the array during each iteration. 
             */
            // return x.Aggregate(1, (acc, val) => acc * val);
        }

        /*Prepends a number to each string in a list starting at 1*/
        public static List<string> PrependNumber(List<string> lines) // OPTION 1: => lines.Select((x, i) => $"{i + 1}: {x}").ToList();
        {

            /*Option 2
            Initialise a new results string
            - Loop through the List Array
            - For each string in list array Create a string variable and Insert the string index number + 1
            - Add new string to result List
            - return result
            */
            List<string> result = new List<string>();
            for (int i = 0; i < lines.Count; i++)
            {
                string ModifiedLine = lines[i].Insert(0, $"{i + 1}: ");
                result.Add(ModifiedLine);
            }
            return result;
        }

        public static List<int> RemoveSmallest(List<int> numbers)
        {
            /*Solution 1*/
            /*if (numbers.Count == 0) { return numbers; }
            int MinNumber = numbers.Min();
            numbers.Remove(MinNumber);
            return numbers;*/

            /*Solution 2*/
            if (numbers.Count == 0) { return numbers; }
            int smallestInt = numbers.First();

            foreach (int num in numbers)
            {
                if (smallestInt > num) { smallestInt = num; }
            }
            foreach (int numb in numbers)
            {
                if (numb == smallestInt)
                {
                    numbers.Remove(smallestInt);
                    break;
                }
            }

            return numbers;
        }


        /*Returns how many iterations before initial value surpasses a set goal*/
        public static int IterationTotal(int initialValue, double percent, int aug, int goalValue)
        {
            int iterations = 0;
            double currentValue = initialValue;

            while (currentValue < goalValue)
            {
                currentValue += currentValue * (percent / 100) + aug;
                iterations++;
            }
            return iterations;

        }


        public static int CharCount(string str, char character)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }

            int count = str.Count(c => c == character);

            return count;

        }

        /*To determine if a triangle can be formed with three given side lengths 𝑎a, 𝑏b, and 𝑐 c,
         * we can use the triangle inequality theorem. This theorem states that for three sides to 
         * form a triangle, the sum of the lengths of any two sides must be greater than the length of the remaining side.*/
        public static bool IsTriangle(int a, int b, int c)
        {
            return (a > 0 && b > 0 && c > 0 && a + b > c && a + c > b && c + b > a);
        }



        public static bool comp(int[] a, int[] b)
        {
            if (a == null || b == null || a.Length != b.Length) { return false; } // returns false if a or b are null arrays

            int[] isSquared = new int[a.Length]; // instantiates a new empty array the same length as array a

            for (int i = 0; i < a.Length; i++)
            {
                isSquared[i] = a[i] * a[i]; // adds the squared value of each element in a array to the corrosponding index in the isSquared array
            }

            Array.Sort(isSquared);
            Array.Sort(b);

            for (int i = 0; i < a.Length; i++)
            {

                if (isSquared[i] != b[i]) { return false; }
            }
            return true;
        }

        public static int[] CountBy(int x, int n)
        {
            int[] result = new int[n];

            for (int i = 0; i < n; i++)
            {
                result[i] = x * (i + 1);
            }
            return result;
        }

        public static int FindSmallestIntOfAnArray(int[] args)
        {

            // return args.Min(); is much quicker, its a linq feature

            /*
               int small = args[0];
                foreach(int num in args) {
                    if(small > num) {
                    small = num;
                    }
                }
                return small;
            */

            Array.Sort(args);
            int result = args.First();
            return result;
        }

        /*
                       1463
                       0123
                       calculate each char by its index in reference to string length
                       if totallength = 3 & index 0 then result = char[0] (1) + "0" *  Length 3 = "1000"
                       therfore Length from position of index
                       char[1] would be result = char[1] (4) + "0" *  Length 2 = "400"
                       */
        public static string ExpandedForm(long num)
        {
            string result = "";
            string number = num.ToString();
            for (int i = 0; i < number.Length; i++)
            {
                char currentDigit = number[i];

                if (currentDigit != '0')
                {
                    string forResult = currentDigit + new string('0', number.Length - i - 1);
                    result += (result.Length > 0 ? " + " : "") + forResult;
                }
            }

            return result;
        }

        public static IEnumerable<T> UniqueInOrder<T>(IEnumerable<T> iterable)
        {
            List<T> result = new List<T>();
            foreach (var item in iterable)
            {
                if (result.Count == 0 || !result[result.Count - 1].Equals(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }


        /****
        Return String with all Whitespace removed.
        ****/
        public static string NoSpace(string input)
        {
            return new string(input.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
        }

    }
}

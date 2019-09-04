using System;

namespace FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            FizzBuzz();
            Console.Read();
        }

        public static void FizzBuzz()
        {
            for (int i = 1; i < 101; i++ )
            {

                if (i % (3 * 5) == 0)
                {
                    Console.WriteLine("FizzBuzz");
                    continue;
                }

                if (i % 3 == 0)
                {
                    Console.WriteLine("Fizz");
                    continue;
                }

                if (i % 5 == 0)
                {
                    Console.WriteLine("Buzz");
                    continue;
                }

                Console.WriteLine(i);
            }          
        }
    }
}

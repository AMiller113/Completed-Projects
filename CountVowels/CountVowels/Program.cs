using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountVowels
{
    public class Program
    {
        public static int a, e, i, o, u;
        static string[] vowels = {"a","e","i","o","u"};

        static void Main(string[] args)
        {
            CountVowels("aaaaaaaa");
            Console.Read();
        }

        public static int CountVowels(string text)
        {
            int numberOfVowels = 0;

            if (text == null || text.Equals(""))
            {
                Console.WriteLine("There are no vowels in this string.(Or any letters for that matter.)");
                return 0;
            }
            foreach (char current in text)
            {
                foreach(string vowel in vowels)
                {
                    if (vowel[0] == current)
                    {
                        numberOfVowels++;
                        CheckWhichVowel(current);
                    }
                }
            }

            Console.WriteLine("The number of vowels in this string is {0} \n", numberOfVowels);
            Console.WriteLine("The number of each vowel is ");
            Console.Write("A = {0} \r\n" +
                "E = {1} \r\n" +
                "I = {2} \r\n" +
                "O = {3} \r\n" +
                "U = {4}"
                , a ,e, i, o, u);

            return numberOfVowels;
        }

        public static void CheckWhichVowel(char c)
        {
            switch (c)
            {
                case 'a':
                    a++;
                    break;
                case 'e':
                    e++;
                    break;
                case 'i':
                    i++;
                    break;
                case 'o':
                    o++;
                    break;
                case 'u':
                    u++;
                    break;
                default:
                    break;
            }
        }
    }
}

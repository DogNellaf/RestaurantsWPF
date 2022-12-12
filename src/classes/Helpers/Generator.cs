using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantsClasses.Helpers
{
    public static class Generator
    {
        //private static Char[] charArray = new char[char.MaxValue];
        private static int passwordLength = 18;
        private static Random random = new Random();

        //static Generator()
        //{
        //    for (char c = Char.MinValue; c < Char.MaxValue; ++c)
        //    {
        //        charArray[(int)c] = c;
        //    }
        //}   

        public static string GenerateNewPassword()
        {
            string result = "";
            for (int i = 0; i < passwordLength; ++i)
            {
                int element = random.Next('a', 'z');
                result += (char)element;
            }

            return result;
        }
    }
}

using System.Text.RegularExpressions;
using System;

namespace BLL
{
    public class GetInputService : IGetInputService
    {
        public static string GetVerifiedInput(string pattern)
        {
            string input = " ";
            bool isInputProper = false;
            for (int i = 0; i < 3; ++i)
            {
                if (isInputProper != true)
                {
                    input = Console.ReadLine();
                    isInputProper = Regex.IsMatch(input, pattern);
                }
            }
            return isInputProper ? input.ToLower() : throw new TooManyFalseAttemptsException();
        }
    }
}
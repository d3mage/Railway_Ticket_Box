using System.Text.RegularExpressions;
using System;

namespace BLL
{
    public class GetInputService : IGetInputService
    {
        public static string GetVerifiedInput(string pattern)
        {
            string input = Console.ReadLine();
            bool isInputProper = Regex.IsMatch(input, pattern);
            return isInputProper ? input.ToLower() : throw new InvalidInputException();
        }

        string IGetInputService.GetVerifiedInput(string pattern)
        {
            return GetVerifiedInput(pattern);
        }
    }
}
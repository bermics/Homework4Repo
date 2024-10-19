using System;
using System.Text.RegularExpressions;

namespace Homework4
{
    public delegate bool ValidationFunction(string input, out string errorMessage);

    public class InputValidation
    {
        public static bool ValidateName(string name, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                errorMessage = "Name cannot be empty.";
                return false;
            }
            errorMessage = null;
            return true;
        }

        public static bool ValidateEmail(string email, out string errorMessage)
        {
            if (!Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                errorMessage = "Email must be a valid format (e.g., example@example.com).";
                return false;
            }
            errorMessage = null;
            return true;
        }

        public static bool ValidatePhone(string phone, out string errorMessage)
        {
            if (!Regex.IsMatch(phone, @"^\d{10,}$"))
            {
                errorMessage = "Phone number must contain only digits and be at least 10 characters long.";
                return false;
            }
            errorMessage = null;
            return true;
        }

        public static bool ValidateAddress(string address, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                errorMessage = "Address cannot be empty.";
                return false;
            }
            errorMessage = null;
            return true;
        }

        public static string GetValidInput(string prompt, ValidationFunction validationFunc)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();
                if (validationFunc(input, out string errorMessage))
                {
                    return input;
                }
                Console.WriteLine($"Invalid input: {errorMessage}");
            }
        }
    }
}

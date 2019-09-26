using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class PointOfEntry
    {
        public static void Run()
        {
            List<string> options = new List<string>() { "Hello welcome to the Humane Society! Are you a(n):", "1. Employee", "2. Customer" };
            UserInterface.DisplayUserOptions(options);
            string userInput = UserInterface.GetUserInput();
            RunUserInput(userInput);
        }
        private static void RunUserInput(string input)
        {

            Customer customer;
            UserEmployee employee;
            Admin admin;
            Console.Clear();
            switch (input.ToLower().Trim())
            {
                case "customer":
                case"2":
                    customer = new Customer();
                    customer.LogIn();
                    break;
                case "employee":
                case "1":
                    employee = new UserEmployee();
                    employee.LogIn();
                    break;
                case "admin**":
                    admin = new Admin();
                    admin.LogIn();
                    break;
                default:
                    UserInterface.DisplayUserOptions("Input not recognized please try again.");
                    Run();
                    break;
            }

        }
    }
}

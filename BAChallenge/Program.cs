using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAChallenge
{
    class Program
    {
        private static readonly ScheduleGenerator generator = new ScheduleGenerator();

        static void Main(string[] args)
        {
            // Task #1
            DateTime loanStartDate = new DateTime(2017, 04, 15);
            int loanAmount = 5000;
            int anualInterest = 12;
            int periods = 24;
            string filename = "FirstSchedule.csv";

            Loan firstLoan = new Loan(loanStartDate, loanAmount, anualInterest, periods); 
            generator.GenerateSchedule(firstLoan, filename);

            //Task #2
            string changedInterestFilename = "ChangedInterestSchedule.csv";

            firstLoan.ChangeInterestRate(new DateTime(2017, 09, 02), 9);
            generator.GenerateSchedule(firstLoan, changedInterestFilename);

            //Task #3
            Console.Write("Do you want your own loan schedule(y/n): ");
            string decision = Console.ReadLine().ToUpper();

            if (decision.Equals("Y"))
            {
                filename = "CustomUserSchedule.csv";

                Console.Write("Enter the start date of loan (yyyy-mm-dd): ");

                // keep asking every question until they provide what we ask
                while (!(DateTime.TryParse(Console.ReadLine(), out loanStartDate)))
                {
                    Console.Write("Invalid date. Re-enter the start date of loan (yyyy-mm-dd): ");
                }

                Console.Write("Enter the amount of loan: ");
                while (!(int.TryParse(Console.ReadLine(), out loanAmount)) || loanAmount < 0)
                {
                    Console.Write("Invalid amount. Please re-enter amount: ");
                }

                Console.Write("Enter anual interest rate: ");
                while (!(int.TryParse(Console.ReadLine(), out anualInterest)) || anualInterest < 0)
                {
                    Console.Write("Invalid number. Please re-enter the anual interest rate: ");
                }

                Console.Write("Enter the amount of loan periods: ");
                while (!(int.TryParse(Console.ReadLine(), out periods)) || periods < 0)
                {
                    Console.Write("Invalid number. Please re-enter the amount of loan periods: ");
                }

                Loan userLoan = new Loan(loanStartDate, loanAmount, anualInterest, periods);
                generator.GenerateSchedule(userLoan, filename);
            }
        }
    }
}
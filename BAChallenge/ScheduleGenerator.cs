using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAChallenge
{
    class ScheduleGenerator
    {
        private readonly FileWriter writer;

        public ScheduleGenerator()
        {
            writer = new FileWriter();
        }

        public void GenerateSchedule(Loan loan, string filename)
        {
            int periods = loan.Periods;
            int interestRate = loan.AnualInterest;

            double monthlyRate = CalculateMonthlyRate(interestRate);
            double totalPayment = CalculateTotalPayment(monthlyRate, loan.LoanAmount, periods);

            double remaining = loan.LoanAmount;
            double principal, interest;

            DateTime paymentDate = loan.LoanStartDate;

            DateTime interestChangeTime = loan.InterestChangeDate ?? new DateTime(1555,12,12);
            int newInterestRate = loan.NewInterestRate ?? 0;

            List<string> paymentDetails = new List<string>
            {
                "Payment #, Payment date, Remaining amount, Principal payment," +
                " Interest payment, Total payment, Interest rate"
            };

            for (int i = 1; i <= periods; i++)
            {
                // re-calculate total payment if interest rate changes this month
                if(loan.InterestRateChange && (paymentDate > interestChangeTime))
                {
                    interestRate = newInterestRate;
                    monthlyRate = CalculateMonthlyRate(interestRate);
                    totalPayment = CalculateTotalPayment(monthlyRate, loan.LoanAmount, periods);

                    // assign arbitrary high value so that this IF will be executed only 1 time
                    interestChangeTime = new DateTime(2500, 12, 12);
                }

                interest = Math.Round((remaining * monthlyRate), 2);

                // last month principal must be equal to remaining amount
                // so that after last payment remaining would be 0
                if (i == periods)
                {
                    principal = remaining;
                    totalPayment = principal + interest;
                }
                else principal = Math.Round((totalPayment - interest), 2);

                paymentDetails.Add($"{i}, {paymentDate.ToShortDateString()}, {remaining}, {principal}, {interest}, {totalPayment}, {interestRate}");

                paymentDate = paymentDate.AddMonths(1);
                remaining -= principal;
            }

            writer.Write(paymentDetails, filename);
        }

        private double CalculateMonthlyRate(int interestRate) => (interestRate / 100.0) / 12;

        private double CalculateTotalPayment(double monthlyRate, int loanAmount, int periods)
        {
            return Math.Truncate(100 * ((monthlyRate * loanAmount) / (1 - Math.Pow((1 + monthlyRate), - periods)))) / 100;
        }
    }
}

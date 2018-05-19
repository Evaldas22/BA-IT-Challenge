using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAChallenge
{
    class Loan
    {
        public DateTime LoanStartDate { get; set; }
        public int LoanAmount { get; set; }
        public int AnualInterest { get; set; }
        public int Periods { get; set; }

        public bool InterestRateChange { get; set; } = false;
        public DateTime? InterestChangeDate { get; set; } = null;
        public int? NewInterestRate { get; set; } = null;

        public Loan(DateTime startDate, int amount, int anualInterest, int periods)
        {
            LoanStartDate = startDate;
            LoanAmount = amount;
            AnualInterest = anualInterest;
            Periods = periods;
        }

        public void ChangeInterestRate(DateTime changeDate, int newInterestRate)
        {
            InterestRateChange = true;
            InterestChangeDate = changeDate;
            NewInterestRate = newInterestRate;
        }
    }
}

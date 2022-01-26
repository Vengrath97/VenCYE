using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VenCYE
{
    class Day
    {
        int _dayOfMonth;
        int _monthOfYear;
        int _year;
        List<Expense> _expenses;

        public int DayOfMonth { get => _dayOfMonth; set => _dayOfMonth = value; }
        public int MonthOfYear { get => _monthOfYear; set => _monthOfYear = value; }
        public int Year { get => _year; set => _year = value; }
        internal List<Expense> Expenses { get => _expenses; set => _expenses = value; }
        public Day(int dayOfMonth, int monthOfYear, int year)
        {
            DayOfMonth = dayOfMonth;
            MonthOfYear = monthOfYear;
            Year = year;
            Expenses = new List<Expense>();
        }
        public void AddExpense(Expense expense)
        {
            this.Expenses.Add(expense);
        }
    }
}

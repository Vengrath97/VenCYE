using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VenCYE
{
    class Day
    {
        private int _dayOfMonth;
        private int _monthOfYear;
        private int _year;
        private List<Expense> _expenses;

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
        public void SaveToCSV()
        {
            List<string> fullLine = new List<string>();
            foreach (Expense expense in Expenses)
            {
                fullLine.Add(this.DayOfMonth + "," + this.MonthOfYear + "," + this.Year + "," + expense.getCSVFormattedLine());
            }
            foreach (string text in fullLine)
            {
                if (File.ReadAllText("savefile.csv") == "") { File.AppendAllText("savefile.csv", (text)); }
                else { File.AppendAllText("savefile.csv", ("\n" + text)); }
            }
        }
    }
}
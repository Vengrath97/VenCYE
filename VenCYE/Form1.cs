using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace VenCYE
{
    public partial class Form : System.Windows.Forms.Form
    {
        static List<Day> Days = new List<Day>();
        string csvFilePath = "savefile.csv";
    public Form()
        {
            if (!File.Exists(csvFilePath)) 
            { 
                File.WriteAllText(csvFilePath, "");
            }
            InitializeComponent();
        }
        private void LoadCSV()
        {
            string[] lines = File.ReadAllLines(csvFilePath);
            foreach (string item in lines)
            {
                string[] subs = item.Split(",");
                if (subs[6] == "") { subs[6] = "0"; }
                string valueString = subs[5] + "," + subs[6];
                Expense temp = new Expense(subs[3], subs[4], doubleValidation(valueString));
                AssignExpense(subs[0], subs[1], subs[2], temp);
            }
        }
        private void ClearDisplay()
        {
            listBoxYear1.Items.Clear();
            listBoxMonth1.Items.Clear();
            listBoxDay1.Items.Clear();
            listBoxExpense1.Items.Clear();
            listBoxCategory1.Items.Clear();
            listBoxValue1.Items.Clear();
        }
        public void InfoToDisplay()
        {
            foreach (Day day in Days)
            {
                foreach (Expense expense in day.Expenses)
                {
                    listBoxYear1.Items.Add(day.Year.ToString());
                    listBoxMonth1.Items.Add(day.MonthOfYear.ToString());
                    listBoxDay1.Items.Add(day.DayOfMonth.ToString());
                    listBoxExpense1.Items.Add(expense.Name);
                    listBoxCategory1.Items.Add(expense.Category);
                    listBoxValue1.Items.Add(expense.Value.ToString());
                }
            }
        }
        private int intValidation(string input, int minValue, int maxValue)
        {
            int parsedValue;
            if (int.TryParse(input, out parsedValue) && parsedValue >= minValue && parsedValue <= maxValue)
            {
                return parsedValue;
            }
            else
            {
                return -1;
            }
        }   //returns value between min and max, or -1 if outside of range, or invalid data
        private double doubleValidation(string input)
        {
            double parsedValue;
            if (Double.TryParse(input, out parsedValue))
            {
                return parsedValue;
            }
            else
            {
                return -1D;
            }
        }                         //returns value, or -1 (in double format) if invalid
        private int getDayIndex(int day, int month, int year)
        {
            int index = 0;
            foreach (Day dayObject in Days)
            {
                if (dayObject.DayOfMonth == day && dayObject.MonthOfYear == month && dayObject.Year == year)
                {
                    return index;
                }
                index += 1;
            }
            return -1;
        }                 //returns index in Days table, or -1 if day doesn't exist
        private void AssignExpense(string dayData, string monthData, string yearData, Expense expense)
        {
            int parsedDay = intValidation(dayData, 1, 31);
            int parsedMonth = intValidation(monthData, 1, 12);
            int parsedYear = intValidation(yearData, 1, 2099);

            bool correctValuesPassed = (!(parsedDay == -1 || parsedMonth == -1 || parsedMonth == -1));

            if (correctValuesPassed)
            {
                int index = getDayIndex(parsedDay, parsedMonth, parsedYear);

                if (index >= 0)
                {
                    Days[index].AddExpense(expense);
                }
                else
                {
                    Days.Add(new Day(parsedDay, parsedMonth, parsedYear));
                    Days[Days.Count - 1].AddExpense(expense);
                }
            }
            else 
            {
                MessageBox.Show("Values passed are incorrect!", "Error!");
            }

        }
        private void AssignExpense(int parsedDay, int parsedMonth, int parsedYear, Expense expense)
        {
            bool correctValuesPassed = (!(parsedDay == -1 || parsedMonth == -1 || parsedMonth == -1));

            if (correctValuesPassed)
            {
                int index = getDayIndex(parsedDay, parsedMonth, parsedYear);

                if (index >= 0)
                {
                    Days[index].AddExpense(expense);
                }
                else
                {
                    Days.Add(new Day(parsedDay, parsedMonth, parsedYear));
                    Days[Days.Count - 1].AddExpense(expense);
                }
            }
            else
            {
                MessageBox.Show("Values passed are incorrect!", "Error!");
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            Days.Clear();
            ClearDisplay();
            LoadCSV();
            InfoToDisplay();
        }               //Basically restarts the app
        private void ButtonAddExpense1_Click(object sender, EventArgs e)
        {
            Expense temp = new Expense(textBoxExpense1.Text, textBoxCategory1.Text, doubleValidation(textBoxValue1.Text));
            AssignExpense(textBoxDay1.Text, textBoxMonth1.Text, textBoxYear1.Text, temp);
            ClearDisplay();
            InfoToDisplay();
        }
        private void ButtonRemoveExpense1_Click(object sender, EventArgs e)
        {
            ClearDisplay();
            Days.Clear();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            File.WriteAllText(csvFilePath, "");
            foreach (Day day in Days)
            {
                day.SaveToCSV();
            }
        }
    }
}

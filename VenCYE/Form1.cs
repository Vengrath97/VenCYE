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



/// <summary>
/// coś się dzieje, kiedy zapisuję SCVkę; mianowicie: 
/// </summary>
namespace VenCYE
{
    public partial class Form : System.Windows.Forms.Form
    {
        static List<Day> Days = new List<Day>();
    public Form()
        {
            if (!File.Exists("savefile.csv")) { File.WriteAllText("savefile.csv", ""); }
            InitializeComponent();
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
        private void ClearDisplay()
        {
            listBoxYear1.Items.Clear();
            listBoxMonth1.Items.Clear();
            listBoxDay1.Items.Clear();
            listBoxExpense1.Items.Clear();
            listBoxCategory1.Items.Clear();
            listBoxValue1.Items.Clear();
        }
        private void ButtonAddExpense1_Click(object sender, EventArgs e)
        {
            AssignExpense(textBoxDay1.Text, textBoxMonth1.Text, textBoxYear1.Text, textBoxExpense1.Text, textBoxCategory1.Text, textBoxValue1.Text);
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

            File.WriteAllText("savefile.csv", "");
            foreach (Day day in Days)
            {
                day.SaveToCSV();
            }
        }
        private void LoadCSV()
        {
            string[] lines = File.ReadAllLines("savefile.csv");
            foreach (string item in lines)
            {
                string[] subs = item.Split(",");
                if (subs[6] == "") { subs[6] = "0"; }
                string valueString = subs[5] + "," + subs[6];
                AssignExpense(subs[0], subs[1], subs[2], subs[3], subs[4], valueString);
            }
        }
        private void AssignExpense(string daydata, string monthdata, string yeardata, string expensedata, string categorydata, string valuedata)
        {
            int parsedDay;
            bool dayValid = false;
            if (int.TryParse(daydata, out parsedDay) && parsedDay < 32) { dayValid = true; }

            int parsedMonth;
            bool monthValid = false;
            if (int.TryParse(monthdata, out parsedMonth) && parsedMonth < 13 && 0 < parsedMonth) { monthValid = true; }

            int parsedYear;
            bool yearValid = false;
            if (int.TryParse(yeardata, out parsedYear) && 0 < parsedDay) { yearValid = true; }

            double parsedValue;
            bool valueValid = false;
            if (Double.TryParse(valuedata, out parsedValue) && parsedValue > 0) { valueValid = true; }

            if (dayValid && monthValid && yearValid && valueValid)
            {
                bool dayAlreadyExists = false;
                foreach (Day day in Days)
                {
                    if (day.DayOfMonth == parsedDay && day.MonthOfYear == parsedMonth && day.Year == parsedYear)
                    {
                        dayAlreadyExists = true;
                        day.AddExpense(new Expense(expensedata, categorydata, parsedValue));
                        break;

                    }
                }
                if (!dayAlreadyExists)
                {
                    Days.Add(new Day(parsedDay, parsedMonth, parsedYear));
                    Days[Days.Count - 1].AddExpense(new Expense(expensedata, categorydata, parsedValue));
                }
            }
            else
            {
                MessageBox.Show("Not all required fields are properly filled!", "Error!", MessageBoxButtons.OK);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Days.Clear();
            ClearDisplay();
            LoadCSV();
            InfoToDisplay();
        }
    }
}

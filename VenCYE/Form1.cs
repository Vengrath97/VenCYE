using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VenCYE
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }


        private void AddExpenseToDisplay(Day day, int expenseID)
        {
            listBoxYear1.Items.Add(day.Year.ToString());
            listBoxMonth1.Items.Add(day.MonthOfYear.ToString());
            listBoxDay1.Items.Add(day.DayOfMonth.ToString());
            listBoxExpense1.Items.Add(day.Expenses[expenseID].Name);
            listBoxCategory1.Items.Add(day.Expenses[expenseID].Category);
            listBoxValue1.Items.Add(day.Expenses[expenseID].Value.ToString());
        }


        private void ButtonAddExpense1_Click(object sender, EventArgs e)
        {
            int parsedDay;
            bool dayValid = false;
            if (int.TryParse(textBoxDay1.Text, out parsedDay) && parsedDay < 32) { dayValid = true; }

            int parsedMonth;
            bool monthValid = false;
            if (int.TryParse(textBoxMonth1.Text, out parsedMonth) && parsedMonth < 13 && 0 < parsedMonth) { monthValid = true; }

            int parsedYear;
            bool yearValid = false;
            if (int.TryParse(textBoxYear1.Text, out parsedYear) && 0 < parsedDay) { yearValid = true; }

            double parsedValue;
            bool valueValid = false;
            if (Double.TryParse(textBoxValue1.Text, out parsedValue) && parsedValue > 0) { valueValid = true; }

            if (dayValid && monthValid && yearValid && valueValid)
            {
                Expense tempExpense = new Expense(textBoxExpense1.Text, textBoxCategory1.Text, parsedValue);
                Day tempDay = new Day(parsedDay, parsedMonth, parsedYear);                                  //
                tempDay.AddExpense(tempExpense);                                                            
                AddExpenseToDisplay(tempDay, 0);
            }
            else 
            {
                MessageBox.Show("Not all required fields are properly filled!\nPlease, fill them, or provide the creator of this program with your debit card details, to proceed.", "Error!", MessageBoxButtons.OK);
            }
        }





    }
}

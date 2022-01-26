using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VenCYE
{
    class Expense
    {
        private string _name;
        private string _category;
        private double _value;

        public string Name { get => _name; set => _name = value; }
        public string Category { get => _category; set => _category = value; }
        public double Value { get => _value; set => _value = value; }
        public Expense(string name, string category, double value)
        {
            Name = name;
            Category = category;
            Value = value;
        }
    }
}

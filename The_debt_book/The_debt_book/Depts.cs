using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_debt_book
{
    public class Depts 
    {
        string _name;
        int _value;
        string _combined;

        public Depts()
        { }
        public Depts(string Name, int Value)
        {
            _name = Name;
            _value = Value;
            _combined = Name + "                " + Value;
        }

        public string Combined
        {
            get
            {
                return _combined;
            }
        }
        public string Name 
        {
            get
            { 
                return _name;
            }
            set
            { 
                _name = Name;
            }
        }

        public int Value
        {
            get
            { 
                return _value;
            }
            set
            {
                _value = Value;
            }
        }
    }
}

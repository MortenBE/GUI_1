using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Commands;

namespace The_Debt_Book
{
    public class Debtors : BindableBase
    {
        private string name;
        private int totaldebt;
        public Debtors()
        {
        }
        public Debtors(string dname)
        {
            name = dname;

            totaldebt = 0;
        }
        public string Debtorname
        {
            get => name;
            set => SetProperty(ref name, value);
        }


        public int InitDebt
        {
            get => totaldebt;
            set => SetProperty(ref totaldebt, value);
        }

    }
}

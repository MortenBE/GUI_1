using System;
using Prism.Mvvm;

namespace TheDebtBook.Models
{
    public class AddDeptsModel : BindableBase
    {
        private DateTime _depttime;
        private int _debtAmount;

        public AddDeptsModel(DateTime entryDate, int amount)
        {
            Depttime = entryDate;
            DeptAmount = amount;
        }

        public DateTime Depttime
        {
            get => _depttime;
            set => SetProperty(ref _depttime, value);
        }

        public int DeptAmount
        {
            get => _debtAmount;
            set => SetProperty(ref _debtAmount, value);
        }
    }
}
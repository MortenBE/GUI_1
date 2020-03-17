using Prism.Mvvm;

namespace TheDebtBook.Models
{
    public class Depts : BindableBase
    {
        private string _name;
        private double _debt;

        public Depts()
        {
        }

        public Depts(string name, int amount)
        {
            Name = name;
            Debt = amount;
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public double Debt
        {
            get => _debt;
            set => SetProperty(ref _debt, value);
        }
    }
}
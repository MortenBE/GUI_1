using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace TheDebtBook.Models
{
    public class Depts : BindableBase
    {
        private string _name;
        private double _dept;
        private ObservableCollection<AddDeptsModel> _deptsCollect = new ObservableCollection<AddDeptsModel>();
        public ObservableCollection<AddDeptsModel> DeptsCollect
        {
            get => _deptsCollect;
            set => SetProperty(ref _deptsCollect, value);
        }

        public Depts Clone()
        {
            return this.MemberwiseClone() as Depts;
        }
        public Depts()
        {
        }

        public Depts(string name, AddDeptsModel dept)
        {
            Name = name;
            Dept = dept.DeptAmount;
            DeptsCollect.Add(dept);
        }


        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public double Dept
        {
            get => _dept;
            set => SetProperty(ref _dept, value);
        }
    }
}
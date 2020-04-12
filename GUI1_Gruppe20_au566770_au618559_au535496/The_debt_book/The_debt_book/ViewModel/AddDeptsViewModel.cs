using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using TheDebtBook.Models;

namespace TheDebtBook.ViewModels
{
    public class AddDeptsViewModel : BindableBase
    {
        private Depts _currentDeptor;
        private int _valueField;

        public AddDeptsViewModel(Depts debtor)
        {
            CurrentDeptor = debtor;
        }

        public Depts CurrentDeptor
        {
            get => _currentDeptor;
            set => SetProperty(ref _currentDeptor, value);
        }

        public int DeptInput
        {
            get => _valueField;
            set => SetProperty(ref _valueField, value);
        }

        private ICommand _addValueButtonCommand;

        public ICommand AddValueButtonCommand
        {
            get => _addValueButtonCommand ?? (_addValueButtonCommand =
                       new DelegateCommand(AddValueButtonCommandExecute)
                           .ObservesProperty(() => CurrentDeptor.Name).ObservesProperty(() => CurrentDeptor.Dept));
        }

        private void AddValueButtonCommandExecute()
        {
            if (DeptInput != 0)
            {
                CurrentDeptor.DeptsCollect.Add(new AddDeptsModel(DateTime.Now, DeptInput));
                CurrentDeptor.Dept += DeptInput;
            }

        }
    }
}
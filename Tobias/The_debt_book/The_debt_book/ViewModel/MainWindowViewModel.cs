using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using TheDebtBook.Models;
using TheDebtBook.ViewModels;
using The_debt_book.Views;

namespace TheDebtBook
{
    public class MainWindowViewModel : BindableBase
    {
        private Depts _currentDebtor;
        private ObservableCollection<Depts> _debtors;
        private int _currentIndex;

        public MainWindowViewModel()
        {
            Debtors = new ObservableCollection<Depts>()
            {
                new Depts("Gustav", 100),
                new Depts("Morten", 150),
            };
        }

        public Depts CurrentDebtor
        {
            get => _currentDebtor;
            set => SetProperty(ref _currentDebtor, value);
        }

        public ObservableCollection<Depts> Debtors
        {
            get => _debtors;
            set => SetProperty(ref _debtors, value);
        }

        public int CurrentIndex
        {
            get => _currentIndex;
            set => SetProperty(ref _currentIndex, value);
        }

        private ICommand _addDebtorCommand;
        
        public ICommand AddDebtorCommand
        {
            get => _addDebtorCommand ?? (_addDebtorCommand = new DelegateCommand(() =>
            {
                var newDebtor = new Depts();
                var vm = new AddDebtorViewModel(newDebtor);
                var dlg = new AddDeptorView()
                {
                    DataContext = vm
                };
                if (dlg.ShowDialog() == true)
                {
                    Debtors.Add(newDebtor);
                    CurrentDebtor = newDebtor;
                    CurrentIndex = (Debtors.Count - 1);
                }
            }));
        }
    }
}
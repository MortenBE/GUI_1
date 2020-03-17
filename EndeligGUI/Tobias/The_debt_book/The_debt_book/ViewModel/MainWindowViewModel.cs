using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using TheDebtBook.Models;
using The_debt_book.Views;

namespace TheDebtBook
{
    public class MainWindowViewModel : BindableBase
    {
        private int _index;
        private Depts _currentDeptor;
        private ObservableCollection<Depts> _deptors;


        public MainWindowViewModel()
        {
            Deptors = new ObservableCollection<Depts>()
            {
                new Depts("Gustav", 100),
                new Depts("Morten", 150),
            };
        }

        public ObservableCollection<Depts> Deptors
        {
            get => _deptors;
            set => SetProperty(ref _deptors, value);
        }

        public int Index
        {
            get => _index;
            set => SetProperty(ref _index, value);
        }

        public Depts CurrentDeptor
        {
            get => _currentDeptor;
            set => SetProperty(ref _currentDeptor, value);
        }

        private ICommand _addDeptor;

        public ICommand AddDeptor
        {
            get => _addDeptor ?? (_addDeptor = new DelegateCommand(() =>
            {
                var newDeptor = new Depts();
                var vm = new AddDebtorViewModel(newDeptor);
                var dlg = new AddDeptorView()
                {
                    DataContext = vm
                };
                if (dlg.ShowDialog() == true)
                {
                    Deptors.Add(newDeptor);
                    CurrentDeptor = newDeptor;
                    Index = (Deptors.Count - 1);
                }
            }));
        }
        private ICommand _deleteDeptor;

        public ICommand DeleteDeptor
        {
            get => _deleteDeptor ??
                   (_deleteDeptor = new DelegateCommand(DeleteDeptorExecuter, DeleteDeptorCanExecute).ObservesProperty(() => Index));
        }

        private void DeleteDeptorExecuter()
        {
            Deptors.Remove(CurrentDeptor);
            if (Deptors.Count > 0)
            {
                CurrentDeptor = Deptors[(Deptors.Count - 1)];
                Index = (Deptors.Count - 1);
            }
        }

        private bool DeleteDeptorCanExecute()
        {
            if (Deptors.Count > 0 && Index >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using The_debt_book.Views;

namespace The_debt_book
{
    public class MainWindowViewModel : BindableBase
    {
        ObservableCollection<Depts> deptslist;

        public MainWindowViewModel()
        {
            deptslist = new ObservableCollection<Depts>
            {
                new Depts("Gustav", 100),
                new Depts("Morten", 200)
            };
        }

        public ObservableCollection<Depts> Deptslist
        {
            get { return deptslist; }
            set { SetProperty(ref deptslist, value); }
        }
        ICommand _newCommand;
        public ICommand AddNewDeptCommand
        {
            get
            {
                return _newCommand ?? (_newCommand = new DelegateCommand(() =>
                {
                    Deptslist.Add(new Depts());
                }));
            }
        }

        private ICommand _addClickCommand;

        public ICommand ButtonAddClickCommand
        {
            get
            {
                return _addClickCommand ?? (_addClickCommand = new DelegateCommand(() =>
                {
                    Views.AddDeptorView newDept = new Views.AddDeptorView();
                    newDept.Show();
                }));
            }

        }



        public ICommand ButtonCloseCommand
        {
            get
            {
                return _addClickCommand ?? (_addClickCommand = new DelegateCommand(() =>
                {

                }));
            }
        }
    }
}
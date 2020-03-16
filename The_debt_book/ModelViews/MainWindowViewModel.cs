using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

//using the_debt_book.Views;
using Prism.Commands;
using Prism.Mvvm;

namespace the_debt_book.ModelViews
{
    class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            
        }

        #region Commands
        private ICommand _buttonAddClickCommand;

        public ICommand ButtonAddClickCommand
        {
            get
            {
                return _buttonAddClickCommand ?? (_buttonAddClickCommand = new DelegateCommand(() =>
                {
                    Views.Add_Debtors newDept = new Views.Add_Debtors();
                    newDept.Show();
                }));
            }

        }
        #endregion
    }
}

using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using The_debt_book.Views;
using System.Windows.Interactivity;
using Microsoft.Expression;
using System;

namespace The_debt_book.AddDeptorView
{
    class AddDeptorViewModel : BindableBase
    {
        MainWindowViewModel test;


        public Action CloseAction { get; set; }
        private ICommand _closeCommand;
        public ICommand ButtonCloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new DelegateCommand((
                    ) =>
                {
                    CloseAction();
                }));
            }
        }
        ICommand _NewDeptcommand;
        public ICommand AddNewDeptCommand
        {
            get
            {
                return _NewDeptcommand ?? (_NewDeptcommand = new DelegateCommand(() =>
                {
                    test.Deptslist.Add(new Depts());
                }));
            }
        }
    }
}

        


//private ICommand _addClickCommand;

//public ICommand ButtonAddClickCommand
//{
//    get
//    {
//        return _addClickCommand ?? (_addClickCommand = new DelegateCommand(() =>
//        {
//            AddDeptorView newDept = new AddDeptorView();
//            newDept.Show();
//        }));
//    }

//}

//private ICommand _closeCommand;

//public ICommand ButtonCloseCommand
//{
//    get
//    {
//        return _addClickCommand ?? (_addClickCommand = new DelegateCommand(() =>
//        {

//        }));
//    }
//}
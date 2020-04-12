using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using TheDebtBook.Models;
using The_debt_book.Views;
using The_debt_book;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace TheDebtBook
{
    public class MainWindowViewModel : BindableBase
    {
        private int _index;
        private Depts _currentDeptor;
        private ObservableCollection<Depts> _deptors;
        private string filePath = "";


        public MainWindowViewModel()
        {
            Deptors = new ObservableCollection<Depts>()
            {
                new Depts("Gustav", new AddDeptsModel(DateTime.Today,100)),
                new Depts("Morten", new AddDeptsModel(DateTime.Today,250))
            };
        }

        #region Properties
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

        private string filename = "";
        public string Filename
        {
            get { return filename; }
            set
            {
                SetProperty(ref filename, value);
                RaisePropertyChanged("Title");
            }
        }

        private bool dirty = false;
        public bool Dirty
        {
            get { return dirty; }
            set
            {
                SetProperty(ref dirty, value);
                RaisePropertyChanged("Title");
            }
        }
        #endregion



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

        private ICommand _listViewItemDoubleClickCommand;
        public ICommand ListViewItemDoubleClickCommand
        {
            get => _listViewItemDoubleClickCommand ??
                   (_listViewItemDoubleClickCommand = new DelegateCommand(() =>
                   {
                       var temp = CurrentDeptor.Clone();
                       var vm = new AddDebtorViewModel(temp);

                       var dlg = new AddDeptsView()
                       {
                           DataContext = vm,
                           Owner = App.Current.MainWindow
                       };
                       if (dlg.ShowDialog() == true)
                       {
                           CurrentDeptor.Dept = temp.Dept;
                           CurrentDeptor.DeptsCollect = temp.DeptsCollect;
                           RaisePropertyChanged("TotalDebt");
                       }
                   }, () =>
                   { return Index >= 0; }
                       ).ObservesProperty(() => Index));
        }

        #region editDebt
        //public ICommand EditAgentCommand
        //{
        //    get
        //    {
        //        return _editCommand ?? (_editCommand = new DelegateCommand(() =>
        //        {
        //            var tempAgent = CurrentAgent.Clone();
        //            var vm = new AgentViewModel("Edit agent", tempAgent)
        //            {
        //                Specialities = specialities
        //            };
        //            var dlg = new AgentWiew
        //            {
        //                DataContext = vm,
        //                Owner = App.Current.MainWindow
        //            };
        //            if (dlg.ShowDialog() == true)
        //            {
        //                // Copy values back
        //                CurrentAgent.ID = tempAgent.ID;
        //                CurrentAgent.CodeName = tempAgent.CodeName;
        //                CurrentAgent.Speciality = tempAgent.Speciality;
        //                CurrentAgent.Assignment = tempAgent.Assignment;
        //                Dirty = true;
        //            }
        //        },
        //        () => {
        //            return CurrentIndex >= 0;
        //        }
        //        ).ObservesProperty(() => CurrentIndex));
        //    }
        //}

        #endregion

        #region DataSaveCommands
        ICommand _SaveAsCommand;
        public ICommand SaveAsCommand
        {
            get { return _SaveAsCommand ?? (_SaveAsCommand = new DelegateCommand<string>(SaveAsCommand_Execute)); }
        }

        private void SaveAsCommand_Execute(string argFilename)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Debtor documents|*.agn|All Files|*.*",
                DefaultExt = "agn"
            };
            if (filePath == "")
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            else
                dialog.InitialDirectory = Path.GetDirectoryName(filePath);

            if (dialog.ShowDialog(App.Current.MainWindow) == true)
            {
                filePath = dialog.FileName;
                Filename = Path.GetFileName(filePath);
                SaveFile();
            }
        }

        //ICommand _SaveCommand;
        //public ICommand SaveCommand
        //{
        //    get
        //    {
        //        return _SaveCommand ?? (_SaveCommand = new DelegateCommand(SaveFileCommand_Execute, SaveFileCommand_CanExecute)
        //          .ObservesProperty(() => Agents.Count));
        //    }
        //}

        //private void SaveFileCommand_Execute()
        //{
        //    SaveFile();
        //}

        //private bool SaveFileCommand_CanExecute()
        //{
        //    return (filename != "") && (Agents.Count > 0);
        //}

        private void SaveFile()
        {
            try
            {
                Repository.SaveFile(filePath, Deptors);
                Dirty = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unable to save file", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region NewFile

        ICommand _NewFileCommand;
        public ICommand NewFileCommand
        {
            get { return _NewFileCommand ?? (_NewFileCommand = new DelegateCommand(NewFileCommand_Execute)); }
        }

        private void NewFileCommand_Execute()
        {
            MessageBoxResult res = MessageBox.Show("Any unsaved data will be lost. Are you sure you want to initiate a new file?", "Warning",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (res == MessageBoxResult.Yes)
            {
                Deptors.Clear();
                Filename = "";
                Dirty = false;
            }
        }

        ICommand _OpenFileCommand;
        public ICommand OpenFileCommand
        {
            get { return _OpenFileCommand ?? (_OpenFileCommand = new DelegateCommand<string>(OpenFileCommand_Execute)); }
        }

        private void OpenFileCommand_Execute(string argFilename)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Deptor documents|*.agn|All Files|*.*",
                DefaultExt = "agn"
            };
            if (filePath == "")
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            else
                dialog.InitialDirectory = Path.GetDirectoryName(filePath);

            if (dialog.ShowDialog(App.Current.MainWindow) == true)
            {
                filePath = dialog.FileName;
                Filename = Path.GetFileName(filePath);
                try
                {
                    Repository.ReadFile(filePath, out ObservableCollection<Depts> tempDepts);
                    Deptors = tempDepts;
                    Dirty = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Unable to open file", MessageBoxButton.OK, MessageBoxImage.Error);
                }                                             
            }
        }

        ICommand _closingCommand;
        public ICommand ClosingCommand
        {
            get
            {
                return _closingCommand ?? (_closingCommand = new
                  DelegateCommand<CancelEventArgs>(ClosingCommand_Execute));
            }
        }

        private void ClosingCommand_Execute(CancelEventArgs arg)
        {
            if (Dirty)
                arg.Cancel = UserRegrets();
        }

        private bool UserRegrets()
        {
            var regret = false;
            MessageBoxResult res = MessageBox.Show("You have unsaved data. Are you sure you want to close the application without saving data first?",
                            "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (res == MessageBoxResult.No)
            {
                regret = true;
            }
            return regret;
        }
        #endregion



    }
}
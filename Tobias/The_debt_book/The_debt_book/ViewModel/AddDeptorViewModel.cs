using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using TheDebtBook.Models;

namespace TheDebtBook
{
    public class AddDebtorViewModel : BindableBase
    {
        private Depts _deptor;

        public AddDebtorViewModel(Depts deptor)
        {
            Deptor = deptor;
        }

        public Depts Deptor
        {
            get => _deptor;
            set => SetProperty(ref _deptor, value);
        }

        private ICommand _okButtonCommand;

        public ICommand OkButtonCommand
        {
            get => _okButtonCommand ?? (_okButtonCommand =
                       new DelegateCommand(OkButtonCommandExecute, OkButtonCommandCanExecute)
                           .ObservesProperty(() => Deptor.Name).ObservesProperty(() => Deptor.Debt));
        }

        private void OkButtonCommandExecute()
        { }

        private bool OkButtonCommandCanExecute()
        {
            return CheckEmpty;
        }

        public bool CheckEmpty
        {
            get
            {
                bool isValid = true;
                if (string.IsNullOrWhiteSpace(Deptor.Name))
                    isValid = false;
                if (string.IsNullOrWhiteSpace(Deptor.Debt.ToString()))
                    isValid = false;
                return isValid;
            }
        }
    }
}
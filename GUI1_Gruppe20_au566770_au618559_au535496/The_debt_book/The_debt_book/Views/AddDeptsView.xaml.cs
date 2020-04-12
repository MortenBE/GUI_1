using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace The_debt_book.Views
{
    /// <summary>
    /// Interaction logic for AddDeptsView.xaml
    /// </summary>
    public partial class AddDeptsView : Window
    {
        public AddDeptsView()
        {
            InitializeComponent();
        }
        private void AddValueButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void AddDeptsViewChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

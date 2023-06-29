using GDash.MVVM.Model;
using GDash.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using GDash.DB;

namespace GDash.MVVM.View
{
    /// <summary>
    /// Interaction logic for EssayForm.xaml
    /// </summary>
    public partial class EssayForm : Window
    {
        public EssayForm(IConnection db, bool isCreate)
        {
            InitializeComponent();
            UserConn conn = new UserConn(db);
            List<User> users = conn.GetAllDB().Cast<User>().ToList();
            UserIdComboBox.ItemsSource = users.Select(u => u.Id);
            UserIdComboBox.IsEnabled = isCreate;

        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            OKButton.IsEnabled = (EssayText.Text != string.Empty &&
                           EssayTitle.Text != string.Empty &&
                           UserIdComboBox.Text != string.Empty);
        }
    }
}

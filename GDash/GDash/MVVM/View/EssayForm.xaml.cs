using GDash.MVVM.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
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

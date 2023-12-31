﻿using GDash.MVVM.Model;
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
        public EssayForm()
        {
            InitializeComponent();
            UserConn conn = new UserConn();
            ObservableCollection<User> users = new ObservableCollection<User>(conn.GetUsersDB());

            UserIdComboBox.ItemsSource = users.Select(u => u.Id);

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

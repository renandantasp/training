using GDash.Core;
using GDash.DB;
using GDash.MVVM.Model;
using GDash.MVVM.View;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GDash.MVVM.ViewModel
{
    public class UserVM : ObservableObject
    {
        public ObservableCollection<User> Users { get; set; }
        private UserConn conn;

        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
            }
        }

        public ICommand CreateCMD => new RelayCommand(_ =>
        {
            User user = new User();
            user.Id = Guid.NewGuid().ToString();

            UserForm userForm = new UserForm();

            userForm.Title = "New User";
            userForm.DataContext = user;
            userForm.ShowDialog();

            if (userForm.DialogResult.HasValue && userForm.DialogResult.Value && userForm.TagValue.Text != string.Empty)
            {
                conn.InsertDB(user);
                SelectedUser = user;
            }
        }, canExecute => true);

        public ICommand UpdateCMD => new RelayCommand(exec => {
           
            User editedUser = SelectedUser.Clone();
            UserForm userForm = new UserForm();

            userForm.Title = "Edit User";
            userForm.DataContext = editedUser;
            userForm.ShowDialog();
            
            if (userForm.DialogResult.HasValue && userForm.DialogResult.Value)
            {
                conn.UpdateDB(editedUser);

            }

        }, canExecute => Users.Any() && SelectedUser != null);
        public ICommand DeleteCMD => new RelayCommand(_ => {

            conn.DeleteDB(SelectedUser.Id);

            SelectedUser = Users.FirstOrDefault();
        }, canExecute => Users.Any());
        
        ObservableCollection<User> GetUsers()
        {

            return new ObservableCollection<User>(conn.GetUsers());
        }
        
        public UserVM()
        {
            IConnection postgres = new Postgres();
            conn = new UserConn(postgres);
            // conn = new UserConn(new Postgres());
            Users = GetUsers();

            for (int i=0; i<Users.Count; i++)
            {
                Users[i].EssayStr = conn.GetEssaysFromId(Users[i].Id);
            }
        }

        

    }
}

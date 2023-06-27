using GDash.Core;
using GDash.MVVM.Model;
using GDash.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GDash.MVVM.ViewModel
{
    public class UserVM : ObservableObject
    {
        public ObservableCollection<User> Users { get; set; }
        

        private User _selectedUser;
        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
            }
        }

        public ICommand CreateCMD => new RelayCommand( _ =>
        {
            User user = new User();
            user.Id = Guid.NewGuid().ToString();

            UserForm userForm = new UserForm();
            userForm.Title = "New User";
            userForm.DataContext = user;
            userForm.ShowDialog();
            if (userForm.DialogResult.HasValue && userForm.DialogResult.Value && userForm.TagValue.Text != string.Empty)
            {
                UserManager.CreateUser(user);
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
                UserManager.EditUser(SelectedUser, editedUser);

            }

        }, canExecute => Users.Any() && SelectedUser != null);
        public ICommand DeleteCMD => new RelayCommand(_ => {

            EssayManager.DeleteAllEssays(SelectedUser.Essays);
            UserManager.DeleteUser(SelectedUser);
            
            SelectedUser = Users.FirstOrDefault();
        }, canExecute => Users.Any());
        public UserVM()
        {
            Users = UserManager.GetUsers();
        }

        
    }
}

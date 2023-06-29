using GDash.Core;
using GDash.DB;
using GDash.MVVM.Model;
using GDash.MVVM.View;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace GDash.MVVM.ViewModel
{
    public class UserVM : ObservableObject
    {
        private UserConn conn;
        
        public ObservableCollection<User> Users { get; set; }
        public User SelectedUser { get; set; }

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
                UpdateUsers();
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
                UpdateUsers();

            }

        }, canExecute => Users.Any() && SelectedUser != null);
        public ICommand DeleteCMD => new RelayCommand(_ => {

            conn.DeleteDB(SelectedUser);

            UpdateUsers();

            SelectedUser = Users.FirstOrDefault();
        }, canExecute => Users.Any() && SelectedUser != null);
        
        ObservableCollection<User> GetUsers()
        {

            return new ObservableCollection<User>(conn.GetUsers());
        }
        
        public void UpdateUsers()
        {
            Users = GetUsers();

            for (int i = 0; i < Users.Count; i++)
            {
                Users[i].EssayStr = conn.GetEssaysFromId(Users[i].Id);
            }
            RaisePropertyChanged(nameof(Users));
        }
        
        public UserVM(IConnection db)
        {
            conn = new UserConn(db);
            UpdateUsers();
        }

        

    }
}

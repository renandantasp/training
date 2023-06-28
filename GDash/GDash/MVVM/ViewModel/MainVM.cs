using GDash.Core;
using GDash.MVVM.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GDash.MVVM.ViewModel
{
    public class MainVM : ObservableObject
    {
        public ObservableObject CurrentVM { get; set; }
        public string NameVM { get; set; }
        public ICommand ToUser => new RelayCommand(_ =>
        {
            CurrentVM = new UserVM();
            NameVM = nameof(User);
            RaisePropertyChanged(nameof(CurrentVM));
        }, canExecute => NameVM != nameof(User));

        public ICommand ToEssay => new RelayCommand(_ =>
        {
            CurrentVM = new EssayVM();
            NameVM = nameof(Essay);
            RaisePropertyChanged(nameof(CurrentVM));
        }, canExecute => NameVM != nameof(Essay));

        public MainVM()
        {
            CurrentVM = new UserVM();
            NameVM = nameof(User);
        }
    }
}

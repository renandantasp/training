﻿using GDash.Core;
using GDash.DB;
using GDash.MVVM.Model;
using System.Windows.Input;

namespace GDash.MVVM.ViewModel
{
    public class MainVM : ObservableObject
    {
        private IConnection conn;

        public ObservableObject CurrentVM { get; set; }
        public string NameVM { get; set; }
        public ICommand ToUser => new RelayCommand(_ =>
        {
            CurrentVM = new UserVM(conn);
            NameVM = nameof(User);
            RaisePropertyChanged(nameof(CurrentVM));
        }, canExecute => NameVM != nameof(User));

        public ICommand ToEssay => new RelayCommand(_ =>
        {
            CurrentVM = new EssayVM(conn);
            NameVM = nameof(Essay);
            RaisePropertyChanged(nameof(CurrentVM));
        }, canExecute => NameVM != nameof(Essay));

        public MainVM()
        {
            conn = new Postgres();
            CurrentVM = new UserVM(conn);
            NameVM = nameof(User);
        }
    }
}

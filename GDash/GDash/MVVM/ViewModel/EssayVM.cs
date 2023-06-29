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
    public class EssayVM : ObservableObject
    {
        private EssayConn conn;
        private IConnection db;

        public ObservableCollection<Essay> Essays { get; set; }
        public Essay SelectedEssay { get; set; }
        public bool IsCreate { get; set; }
        
        public ICommand CreateCMD => new RelayCommand(_ =>
        {
            IsCreate = true;
            Essay newEssay = new Essay();
            newEssay.Id = Guid.NewGuid().ToString();
            EssayForm essayForm = new EssayForm(db, IsCreate);

            essayForm.Title = "New Essay";
            essayForm.DataContext = newEssay;
            essayForm.ShowDialog();

            if (essayForm.DialogResult.HasValue && essayForm.DialogResult.Value && essayForm.UserIdComboBox.Text != string.Empty)
            {
                conn.InsertDB(newEssay);
                SelectedEssay = newEssay;
                Essays = GetEssays();
                RaisePropertyChanged(nameof(Essays));
            }
        }, canExecute => conn.HasUser());
        public ICommand UpdateCMD => new RelayCommand(exec => {

            IsCreate = false;
            Essay editedEssay = SelectedEssay.Clone();
            EssayForm essayForm = new EssayForm(db, IsCreate);

            essayForm.Title = "Essay Edit";
            essayForm.DataContext = editedEssay;
            essayForm.ShowDialog();

            if (essayForm.DialogResult.HasValue && essayForm.DialogResult.Value)
            {
                conn.UpdateDB(editedEssay);
                Essays = GetEssays();
                RaisePropertyChanged(nameof(Essays));
            }

        }, canExecute => Essays.Any() && SelectedEssay != null);
        public ICommand DeleteCMD => new RelayCommand(_ => {

            string userId = SelectedEssay.UserId;
            conn.DeleteDB(SelectedEssay);

            Essays = GetEssays();
            RaisePropertyChanged(nameof(Essays));

            SelectedEssay = Essays.FirstOrDefault();
        }, canExecute => Essays.Any());
        
        ObservableCollection<Essay> GetEssays()
        {
            return new ObservableCollection<Essay>(conn.GetEssays());
        }

        public EssayVM(IConnection db)
        {
            this.db = db;
            conn = new EssayConn(this.db);
            Essays = GetEssays();

        }
    }
}

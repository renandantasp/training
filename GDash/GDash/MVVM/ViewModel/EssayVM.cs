using GDash.Core;
using GDash.DB;
using GDash.MVVM.Model;
using GDash.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GDash.MVVM.ViewModel
{
    public class EssayVM : ObservableObject
    {
        public ObservableCollection<Essay> Essays { get; set; }
        private EssayConn conn;
        IConnection db;

        private Essay _selectedEssasy;
        public Essay SelectedEssay
        {
            get { return _selectedEssasy; }
            set
            {
                _selectedEssasy = value;
            }
        }

        public ICommand CreateCMD => new RelayCommand(_ =>
        {
            Essay newEssay = new Essay();
            newEssay.Id = Guid.NewGuid().ToString();

            EssayForm essayForm = new EssayForm(db);

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
            Essay editedEssay = SelectedEssay.Clone();
            EssayForm essayForm = new EssayForm(db);

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

        public EssayVM()
        {
            //conn = new EssayConn(new Postgres());
            //postgres_conn = new EssayConn(new Postgres());
            conn = new EssayConn(new Maria());
            Essays = GetEssays();
        }

        public EssayVM(IConnection db)
        {
            this.db = db;
            conn = new EssayConn(this.db);
            Essays = GetEssays();

        }
    }
}

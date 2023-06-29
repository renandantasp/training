using GDash.MVVM.Model;
using System.Collections.Generic;

namespace GDash.DB
{
    public interface ICRUD
    {
        List<IModel> GetAllDB();
        void InsertDB(IModel element);
        void UpdateDB(IModel element);
        void DeleteDB(IModel element);
    }
}

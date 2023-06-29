using GDash.Core;
using GDash.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

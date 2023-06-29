using System.Data;

namespace GDash.DB
{
    public interface IConnection
    {
        IDbConnection GetConnection();
        IDbCommand GetCommand();

    }


}

using GDash.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;

namespace GDash.DB
{
    internal class EssayConn : ICRUD
    {

        private IConnection connectionHandler;
        private IDbConnection _conn;

        public EssayConn(IConnection _connection)
        {
            connectionHandler = _connection;
            _conn = connectionHandler.GetConnection();
        }
       
        public void ExecuteCMD(string sql)
        {
            try
            {
                _conn = connectionHandler.GetConnection();
                _conn.Open();

                using (IDbCommand _command = connectionHandler.GetCommand())
                {

                    _command.Connection = _conn;
                    _command.CommandText = sql;
                    _command.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { _conn.Close(); }
        }
        
        public List<IModel> ReadCMD(string sql) 
        {
            List<IModel> essayList = new List<IModel>();

            try
            {
                _conn.Open();

                using (IDbCommand _command = connectionHandler.GetCommand())
                {
                    _command.Connection = _conn;
                    _command.CommandText = sql;

                    using (IDataReader reader = _command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            essayList.Add(new Essay(
                                reader.GetString(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4)));

                        }

                        return essayList;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return essayList;
            }
            finally { _conn.Close(); }

        }
        
        public List<IModel> GetAllDB()
        {
            string commandString = "select * from essay";
            return ReadCMD(commandString);
        }

        public void InsertDB(IModel element)
        {
            Essay essay = element.GetObject() as Essay;
            string commandStr = "INSERT INTO essay (id, userid, image, " +
                                "essaytitle, essaytext)" + 
                                $"VALUES('{essay.Id}', '{essay.UserId}', '{essay.Image}'," + 
                                $"'{essay.EssayTitle}', '{essay.EssayText}');";

            ExecuteCMD(commandStr);

        }

        public void UpdateDB(IModel element)
        {
            Essay essay = element.GetObject() as Essay;
            string commandStr = $"UPDATE essay SET image='{essay.Image}', essaytitle='{essay.EssayTitle}', " +
                                $"essaytext='{essay.EssayTitle}' WHERE id='{essay.Id}';";
            
            ExecuteCMD(commandStr);

        }

        public void DeleteDB(IModel element)
        {
            Essay essay = element.GetObject() as Essay;
            string commandStr = $"DELETE FROM essay WHERE id='{essay.Id}';";
            
            ExecuteCMD(commandStr);
        }


        public bool HasUser()
        {
            try
            {
                _conn.Open();
                string commandString = "select * from users";

                using (IDbCommand _command = connectionHandler.GetCommand())
                {
                    _command.Connection = _conn;
                    _command.CommandText = commandString;

                    using (IDataReader reader = _command.ExecuteReader())
                    {
                        return reader.Read();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                _conn.Close();
            }

        }

        public List<Essay> GetEssays()
        {
            List<Essay> essays = GetAllDB().Cast<Essay>().ToList();
            return essays;
        }


    }
}

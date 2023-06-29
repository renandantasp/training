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

                    using (IDataReader reader = _command.ExecuteReader()) { 
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

        public List<IModel> GetAllDB()
        {
            List<IModel> essayList = new List<IModel>();

            try
            {
                _conn.Open();
                string commandString = "select * from essay";


                using (IDbCommand _command = connectionHandler.GetCommand())
                {
                    _command.Connection = _conn;
                    _command.CommandText = commandString;

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
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _conn.Close();
            }

            return essayList;
        }

        public List<Essay> GetEssays()
        {
            List<Essay> essays = GetAllDB().Cast<Essay>().ToList();
            return essays;
        }
        public void InsertDB(IModel element)
        {
            Essay essay = element.GetObject() as Essay;
            try
            {
                _conn.Open();
                string commandStr = "INSERT INTO essay (id, userid, image, " +
                                    "essaytitle, essaytext)" + 
                                    $"VALUES('{essay.Id}', '{essay.UserId}', '{essay.Image}'," + 
                                    $"'{essay.EssayTitle}', '{essay.EssayText}');";

                using (IDbCommand _command = connectionHandler.GetCommand())
                {
                    _command.Connection = _conn;
                    _command.CommandText = commandStr;

                    _command.ExecuteNonQuery();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _conn.Close();
            }
        }

        public void UpdateDB(IModel element)
        {
            Essay essay = element.GetObject() as Essay;

            try
            {
                string commandStr = $"UPDATE essay SET image='{essay.Image}', " +
                                    $"essaytitle='{essay.EssayTitle}', essaytext='{essay.EssayTitle}' WHERE id='{essay.Id}';";
                _conn.Open();

                using (IDbCommand _command = connectionHandler.GetCommand())
                {

                    _command.Connection = _conn;
                    _command.CommandText = commandStr;
                    _command.ExecuteNonQuery();

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _conn.Close();
            }
        }

        public void DeleteDB(IModel element)
        {
            try
            {
                Essay essay = element.GetObject() as Essay;
                string commandStr = $"DELETE FROM essay WHERE id='{essay.Id}';";
                _conn.Open();

                using (IDbCommand _command = connectionHandler.GetCommand())
                {

                    _command.Connection = _conn;
                    _command.CommandText= commandStr;
                    _command.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _conn.Close();
            }
        }
    }
}

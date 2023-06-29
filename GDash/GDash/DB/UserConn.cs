using GDash.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;

namespace GDash.DB
{
    public class UserConn : ICRUD
    {
        public UserConn(IConnection _connection)
        {
            connectionHandler = _connection;
            _conn = connectionHandler.GetConnection();
        }

        private IConnection connectionHandler;
        private IDbConnection _conn;

        public List<IModel> GetAllDB()
        {
            List<IModel> userList = new List<IModel>();

            try
            {
                string commandString = "select * from users";

                _conn.Open();
                using (IDbCommand _command = connectionHandler.GetCommand())
                {
                    _command.Connection = _conn;
                    _command.CommandText = commandString;

                    using (IDataReader reader = _command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userList.Add(new User(
                                reader.GetString(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetString(5),
                                reader.GetString(6)));
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\nGetAllDB.");
            }
            finally
            {
                _conn.Close();
            }
            return userList;
        }

        public void InsertDB(IModel element)
        {
            User user = element.GetObject() as User;

            try
            {
                
                string commandString = "INSERT INTO users (id, name, tag, email, password, profileimage, bio)"
                                        + $"VALUES('{user.Id}', '{user.Name}', '{user.Tag}', '{user.Email}', '{user.Password}', '{user.ProfileImage}', '{user.Bio}')";
                _conn = connectionHandler.GetConnection();
                _conn.Open();

                using (IDbCommand _command = connectionHandler.GetCommand())
                {

                    _command.Connection = _conn;
                    _command.CommandText = commandString;
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
            User user = (User)element.GetObject();
            try
            {
                string commandStr = $"UPDATE users SET name='{user.Name}', tag='{user.Tag}', email='{user.Email}', " + 
                                    $"password='{user.Password}', profileimage='{user.ProfileImage}', " +
                                    $"bio='{user.Bio}' WHERE id='{user.Id}';";

                _conn = connectionHandler.GetConnection();
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
            User user = element.GetObject() as User;
            try
            {
                string commandStr = $"DELETE FROM users WHERE id='{user.Id}';";

                _conn = connectionHandler.GetConnection();
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

        public List<User> GetUsers()
        {
            List<User> users = GetAllDB().Cast<User>().ToList();
            return users;
        }

        public string GetEssaysById(string id)
        {
            List<string> essayList = new List<string>();

            string commandStr = $"SELECT essaytitle FROM users LEFT JOIN essay ON users.id = essay.userid WHERE users.id = '{id}';";
            try
            {
                
                _conn.Open();


                using (IDbCommand _command = connectionHandler.GetCommand())
                {
                    _command.Connection = _conn;
                    _command.CommandText = commandStr;

                    using (IDataReader reader = _command.ExecuteReader())
                    {
                            while (reader.Read())
                            {
                                if (!reader.IsDBNull(0))
                                {
                                    essayList.Add($"\"{reader.GetString(0)}\"");
                                }

                            }
                            return string.Join(", ", essayList);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message} GetEssaysFromId");
                return string.Empty;
            }
            finally
            {
                _conn.Close();
            }

        }

    }

}

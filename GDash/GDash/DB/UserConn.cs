using GDash.MVVM.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;

namespace GDash.DB
{
    public class UserConn : ICRUD
    {

        private IConnection connectionHandler;
        private IDbConnection _conn;
        
        public UserConn(IConnection _connection)
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
            catch (Exception ex){ MessageBox.Show(ex.Message); }
            finally { _conn.Close(); }
        }
        
        public List<IModel> ReadCMD(string sql)
        {
            List<IModel> userList = new List<IModel>();
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
                            userList.Add(new User(
                                reader.GetString(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetString(5),
                                reader.GetString(6)));
                        }
                        return userList;
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
        
        public List<IModel> GetAllDB()
        {
            string commandString = "select * from users";
            return ReadCMD(commandString);
            
        }
            
        public void InsertDB(IModel element)
        {
            User user = element.GetObject() as User;
            string commandStr = "INSERT INTO users (id, name, tag, email, password, profileimage, bio)"
                                        + $"VALUES('{user.Id}', '{user.Name}', '{user.Tag}', '{user.Email}', '{user.Password}', '{user.ProfileImage}', '{user.Bio}')";

            ExecuteCMD(commandStr);
        }

        public void UpdateDB(IModel element)
        {
            User user = (User)element.GetObject();
            string commandStr = $"UPDATE users SET name='{user.Name}', tag='{user.Tag}', email='{user.Email}', " + 
                                $"password='{user.Password}', profileimage='{user.ProfileImage}', " +
                                $"bio='{user.Bio}' WHERE id='{user.Id}';";
            
            ExecuteCMD(commandStr);

        }
       
        public void DeleteDB(IModel element)
        {
            User user = element.GetObject() as User;
            string commandStr = $"DELETE FROM users WHERE id='{user.Id}';";

            ExecuteCMD(commandStr);
            
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

        public static implicit operator UserConn(Mock<ICRUD> v)
        {
            throw new NotImplementedException();
        }
    }

}

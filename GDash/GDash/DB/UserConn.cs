using GDash.Core;
using GDash.MVVM.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GDash.DB
{
    public class UserConn : ICRUD
    {
        private Connection conn = new Connection();
        NpgsqlCommand command;

        // private IConnection conn;
        public UserConn()
        {

        }

        public UserConn(IConnection connection)
        {
            // conn = SetConnection(infostr);
            // command = SetCommand();
        }

        public List<IModel> GetAllDB()
        {
            List<IModel> userList = new List<IModel>();

            try
            {
                conn.connection.Open();
                string commandString = "select * from users";

                using (command = new NpgsqlCommand())
                {
                    command.Connection = conn.connection;
                    command.CommandText = commandString;

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.connection.Close();
            }
            return userList;
        }

        public List<User> GetUsers()
        {
            List<User> users = GetAllDB().Cast<User>().ToList();
            return users;
        }
        public void InsertDB(IModel element)
        {

            User user = element.GetObject() as User;


            try
            {
                string commandString = "INSERT INTO users (id, name, tag, email, password, profileimage, bio)"
                                        + "VALUES(@p1, @p2, @p3, @p4, @p5, @p6, @p7)";

                conn.connection.Open();

                using (command = new NpgsqlCommand(commandString, conn.connection))
                {

                    command.Parameters.AddWithValue("p1", user.Id);
                    command.Parameters.AddWithValue("p2", user.Name);
                    command.Parameters.AddWithValue("p3", user.Tag);
                    command.Parameters.AddWithValue("p4", user.Email);
                    command.Parameters.AddWithValue("p5", user.Password);
                    command.Parameters.AddWithValue("p6", user.ProfileImage);
                    command.Parameters.AddWithValue("p7", user.Bio);


                    command.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.connection.Close();
            }
        }

        public void UpdateDB(IModel element)
        {
            User user = (User)element.GetObject();
            try
            {
                string commandStr = "UPDATE users SET name=@p2, tag=@p3, email=@p4, password=@p5, " +
                                    " profileimage=@p6, bio=@p7 WHERE id=@p1";

                conn.connection.Open();

                using (command = new NpgsqlCommand(commandStr, conn.connection))
                {
                    command.Parameters.AddWithValue("p1", user.Id);
                    command.Parameters.AddWithValue("p2", user.Name);
                    command.Parameters.AddWithValue("p3", user.Tag);
                    command.Parameters.AddWithValue("p4", user.Email);
                    command.Parameters.AddWithValue("p5", user.Password);
                    command.Parameters.AddWithValue("p6", user.ProfileImage);
                    command.Parameters.AddWithValue("p7", user.Bio);

                    command.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.connection.Close();
            }
        }
       
        public void DeleteBD(string id)
        {
            try
            {
                conn.connection.Open();

                using (command = new NpgsqlCommand("DELETE FROM users WHERE id=@p1 ", conn.connection))
                {
                    command.Parameters.AddWithValue("p1", id);
                    command.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.connection.Close();
            }
        }

        public string GetEssaysFromId(string id)
        {
            List<string> essayList = new List<string>();

            try
            {
                conn.connection.Open();
                string commandStr = "SELECT essaytitle FROM users LEFT JOIN essay ON " +
                                    "users.id = essay.userid WHERE users.id = @p1;";

                using (command = new NpgsqlCommand(commandStr, conn.connection))
                {
                    command.Parameters.AddWithValue("p1", id);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
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
                        return string.Empty;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return string.Empty;
            }
            finally
            {
                conn.connection.Close();
            }

        }


    }
}

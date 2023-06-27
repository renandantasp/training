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
    internal class UserConn
    {
        Connection conn = new Connection();
    
        public List<User> GetUsersDB()
        {

            List<User> userList = new List<User>();

            try
            {
                conn.connection.Open();
                string commandString = "select * from users";
                NpgsqlCommand command = new NpgsqlCommand();

                command.Connection = conn.connection;
                command.CommandText = commandString;

                NpgsqlDataReader reader = command.ExecuteReader();


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


        public void InsertUserDB(User user)
        {
            try
            {
                conn.connection.Open();
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO users (id, name, tag, email, password, profileimage, bio) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7) ", conn.connection)
                {
                    Parameters =
                    {
                        new NpgsqlParameter("p1", user.Id),
                        new NpgsqlParameter("p2", user.Name),
                        new NpgsqlParameter("p3", user.Tag),
                        new NpgsqlParameter("p4", user.Email),
                        new NpgsqlParameter("p5", user.Password),
                        new NpgsqlParameter("p6", user.ProfileImage),
                        new NpgsqlParameter("p7", user.Bio),
                    }
                };

                command.ExecuteNonQuery();
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

        public void AlterUserDB(User user)
        {
            try
            {
                conn.connection.Open();
                NpgsqlCommand command = new NpgsqlCommand("UPDATE users SET name=@p2, tag=@p3, email=@p4, password=@p5, profileimage=@p6, bio=@p7 WHERE id=@p1 ", conn.connection)
                {
                    Parameters =
                    {
                        new NpgsqlParameter("p1", user.Id),
                        new NpgsqlParameter("p2", user.Name),
                        new NpgsqlParameter("p3", user.Tag),
                        new NpgsqlParameter("p4", user.Email),
                        new NpgsqlParameter("p5", user.Password),
                        new NpgsqlParameter("p6", user.ProfileImage),
                        new NpgsqlParameter("p7", user.Bio),
                    }
                };

                command.ExecuteNonQuery();
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

        public void DeleteUserDB(string id)
        {
            try
            {
                conn.connection.Open();
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM users WHERE id=@p1 ", conn.connection)
                {
                    Parameters =
                    {
                        new NpgsqlParameter("p1", id),
                    }
                };

                command.ExecuteNonQuery();
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
                NpgsqlCommand command = new NpgsqlCommand("SELECT essaytitle FROM users LEFT JOIN essay ON users.id = essay.userid WHERE users.id = @p1;", conn.connection)
                {
                    Parameters =
                    {
                        new NpgsqlParameter("p1", id),
                    }
                };

                NpgsqlDataReader reader = command.ExecuteReader();


                
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

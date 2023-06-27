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
    internal class EssayConn
    {

        Connection conn = new Connection();

        public bool HasUser()
        {
            try
            {
                conn.connection.Open();
                string commandString = "select * from users";
                NpgsqlCommand command = new NpgsqlCommand();

                command.Connection = conn.connection;
                command.CommandText = commandString;

                NpgsqlDataReader reader = command.ExecuteReader();

                return reader.HasRows;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                conn.connection.Close();
            }

        }

        public List<Essay> GetEssaysDB()
        {

            List<Essay> essayList = new List<Essay>();

            try
            {
                conn.connection.Open();
                string commandString = "select * from essay";
                NpgsqlCommand command = new NpgsqlCommand();

                command.Connection = conn.connection;
                command.CommandText = commandString;

                NpgsqlDataReader reader = command.ExecuteReader();


                if (reader.HasRows)
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.connection.Close();
            }

            return essayList;

        }

        public void InsertEssayDB(Essay essay)
        {
            try
            {
                conn.connection.Open();
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO essay (id, userid, image, essaytitle, essaytext) VALUES (@p1, @p2, @p3, @p4, @p5) ", conn.connection)
                {
                    Parameters =
                    {
                        new NpgsqlParameter("p1", essay.Id),
                        new NpgsqlParameter("p2", essay.UserId),
                        new NpgsqlParameter("p3", essay.Image),
                        new NpgsqlParameter("p4", essay.EssayTitle),
                        new NpgsqlParameter("p5", essay.EssayText),
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

        public void AlterEssayDB(Essay essay)
        {
            try
            {
                conn.connection.Open();
                NpgsqlCommand command = new NpgsqlCommand("UPDATE essay SET image=@p2, essaytitle=@p3, essaytext=@p4 WHERE id=@p1 ", conn.connection)
                {
                    Parameters =
                    {
                        new NpgsqlParameter("p1", essay.Id),
                        new NpgsqlParameter("p2", essay.Image),
                        new NpgsqlParameter("p3", essay.EssayTitle),
                        new NpgsqlParameter("p4", essay.EssayText),
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

        public void DeleteEssayDB(string id)
        {
            try
            {
                conn.connection.Open();
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM essay WHERE id=@p1 ", conn.connection)
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
    }
}

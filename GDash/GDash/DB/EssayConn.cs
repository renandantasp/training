using GDash.MVVM.Model;
using GDash.MVVM.ViewModel;
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
        NpgsqlCommand command;

        public bool HasUser()
        {
            try
            {
                conn.connection.Open();
                string commandString = "select * from users";

                using (command = new NpgsqlCommand())
                {
                    command.Connection = conn.connection;
                    command.CommandText = commandString;

                    using (NpgsqlDataReader reader = command.ExecuteReader()) { 
                        return reader.HasRows;
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
                string commandStr = "INSERT INTO essay (id, userid, image, " +
                                    "essaytitle, essaytext) VALUES(@p1, @p2, @p3, @p4, @p5);";

                using (command = new NpgsqlCommand(commandStr, conn.connection))
                {
                    command.Parameters.AddWithValue("p1", essay.Id);
                    command.Parameters.AddWithValue("p2", essay.UserId);
                    command.Parameters.AddWithValue("p3", essay.Image);
                    command.Parameters.AddWithValue("p4", essay.EssayTitle);
                    command.Parameters.AddWithValue("p5", essay.EssayText);
                    
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

        public void AlterEssayDB(Essay essay)
        {
            try
            {
                string commandStr = "UPDATE essay SET image=@p2, " +
                                    "essaytitle=@p3, essaytext=@p4 WHERE id=@p1;";
                conn.connection.Open();

                using (command = new NpgsqlCommand(commandStr, conn.connection))
                {

                    command.Parameters.AddWithValue("p1", essay.Id);
                    command.Parameters.AddWithValue("p2", essay.Image);
                    command.Parameters.AddWithValue("p3", essay.EssayTitle);
                    command.Parameters.AddWithValue("p4", essay.EssayText);

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

        public void DeleteEssayDB(string id)
        {
            try
            {

                string commandStr = "DELETE FROM essay WHERE id=@p1;";
                conn.connection.Open();

                using (command = new NpgsqlCommand(commandStr, conn.connection))
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
    }
}

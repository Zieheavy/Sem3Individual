using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class GameDAL
    {
        public void CreateGame(string gameName)
        {
            // create game in database
            string query = string.Empty;

            using (MySqlConnection connection = new MySqlConnection(DalConnection.Conn))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                try
                {
                }
                catch
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void UpdateScore(string gameName, int p1Score, int p2Score)
        {
            string query = string.Empty;

            using (MySqlConnection connection = new MySqlConnection(DalConnection.Conn))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                //command.Parameters.Add(new MySqlParameter("@gameId", gameName));
                //command.Parameters.Add(new MySqlParameter("@creationDate", DateTime.Now));
                MySqlDataReader reader = command.ExecuteReader();
                try
                {
                }
                catch
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}

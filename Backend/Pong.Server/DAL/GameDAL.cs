using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class GameDal
    {
        protected GameDal()
        {
        }

        public static void CreateGame(string gameName)
        {
            using (MySqlConnection connection = new MySqlConnection(DalConnection.Conn))
            {
                connection.Open();
                connection.Close();
            }
        }

        public static void UpdateScore(string gameName, int p1Score, int p2Score)
        {
            using (MySqlConnection connection = new MySqlConnection(DalConnection.Conn))
            {
                connection.Open();
                connection.Close();
            }
        }
    }
}

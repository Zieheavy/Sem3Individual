using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class GameDAL
    {
        public List<PongGame> GetGames()
        {
            //get game info from database
            List<PongGame> pongGames = new List<PongGame>();

            string query = "SELECT * FROM `game` ";

            using (MySqlConnection connection = new MySqlConnection(DalConnection.Conn))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();
                try
                {
                    //while (reader.Read())
                    //{
                    //    PongGame machinedata = new PongGame()
                    //    {
                    //        Id = reader.GetInt32("id"),
                    //        TimeStamp = reader.GetDateTime("timestamp"),
                    //        ShortTime = reader.GetDouble("shot_time")
                    //    };
                    //    pongGames.Add(machinedata);
                    //}

                    //return monitoringsdata;
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
            return new List<PongGame>();
        }

        public List<PongGame> CreateGame(string gameName)
        {
            //create new game in database

            string query = "INSERT INTO `game`( `gameId`, `creationDate`) " +
                            "VALUES (@gameId, @creationDate)";

            using (MySqlConnection connection = new MySqlConnection(DalConnection.Conn))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Add(new MySqlParameter("@value", gameName));
                command.Parameters.Add(new MySqlParameter("@total", DateTime.Now));
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

            return GetGames();
        }

        public void AddUserToGame(string user, string game, int playerType)
        {
            string query = "";
            //add a user to the game
            if(playerType == 1)
            {
                query = "UPDATE `game` SET `player1Id`='@userId' WHERE `gameId` = @gameId";
            }
            else
            {
                query = "UPDATE `game` SET `player2Id`='@userId' WHERE `gameId` = @gameId";
            }

            using (MySqlConnection connection = new MySqlConnection(DalConnection.Conn))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Add(new MySqlParameter("@userId", user));
                command.Parameters.Add(new MySqlParameter("@gameId", game));
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
        public void RemoveUserFromGame(string user, string game, int playerType)
        {
            //remvoe a user from the game
            string query = "";
            if (playerType == 1)
            {
                query = "UPDATE `game` SET `player1Id`= NULL  WHERE `gameId` = @gameId AND `player1Id` = @userId";
            }
            else
            {
                query = "UPDATE `game` SET `player2Id`= NULL  WHERE `gameId` = @gameId AND `player2Id` = @userId";
            }

            using (MySqlConnection connection = new MySqlConnection(DalConnection.Conn))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Add(new MySqlParameter("@userId", user));
                command.Parameters.Add(new MySqlParameter("@gameId", game));
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

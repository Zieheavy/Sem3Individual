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
        public List<PongGameDB> GetGames()
        {
            //get game info from database
            List<PongGameDB> pongGames = new List<PongGameDB>();

            string query = "SELECT * FROM `game` ";

            using (MySqlConnection connection = new MySqlConnection(DalConnection.Conn))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        PongGameDB pongGame = new PongGameDB()
                        {
                            Id = reader.GetInt32("id"),
                            GameId = reader.GetString("gameId"),
                            CreationDate = reader.GetDateTime("creationDate"),
                            Player1Position = reader.GetInt32("player1Position"),
                            Player2Position = reader.GetInt32("player2Position"),
                            BalDirection = reader.GetInt32("balDirection"),
                            BalX = reader.GetInt32("balX"),
                            BalY = reader.GetInt32("balY"),
                        };

                        if (!reader.IsDBNull(reader.GetOrdinal("lastInput")))
                        {
                            pongGame.LastUpdate = reader.GetDateTime("lastInput");
                        }
                        else
                        {
                            pongGame.LastUpdate = new DateTime();
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("player1Id")))
                        {
                            pongGame.Player1Id = reader.GetString("player1Id");
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("player2Id")))
                        {
                            pongGame.Player2Id = reader.GetString("player2Id");
                        }

                        pongGames.Add(pongGame);
                    }

                    return pongGames;
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
            return new List<PongGameDB>();
        }
        public PongGameDB GetGame(PongGame pg)
        {
            string query = "SELECT * FROM `game` WHERE `gameId` = @gameId";

            using (MySqlConnection connection = new MySqlConnection(DalConnection.Conn))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                //command.Parameters.Add(new MySqlParameter("@gameId", pg.GroupName));

                MySqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        PongGameDB pongGame = new PongGameDB()
                        {
                            Id = reader.GetInt32("id"),
                            GameId = reader.GetString("gameId"),
                            CreationDate = reader.GetDateTime("creationDate"),
                            Player1Position = reader.GetInt32("player1Position"),
                            Player2Position = reader.GetInt32("player2Position"),
                            BalDirection = reader.GetInt32("balDirection"),
                            BalX = reader.GetInt32("balX"),
                            BalY = reader.GetInt32("balY")
                        };

                        if (!reader.IsDBNull(reader.GetOrdinal("lastInput")))
                        {
                            pongGame.LastUpdate = reader.GetDateTime("lastInput");
                        }
                        else
                        {
                            pongGame.LastUpdate = new DateTime();
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("player1Id")))
                        {
                            pongGame.Player1Id = reader.GetString("player1Id");
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("player2Id")))
                        {
                            pongGame.Player2Id = reader.GetString("player2Id");
                        }

                        return pongGame;
                    }
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
            return new PongGameDB();
        }

        public List<PongGameDB> CreateGame(string gameName)
        {
            //create new game in database

            string query = "INSERT INTO `game`( `gameId`, `creationDate`) " +
                            "VALUES (@gameId, @creationDate)";

            using (MySqlConnection connection = new MySqlConnection(DalConnection.Conn))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Add(new MySqlParameter("@gameId", gameName));
                command.Parameters.Add(new MySqlParameter("@creationDate", DateTime.Now));
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
            if (playerType == 1)
            {
                query = "UPDATE `game` SET `player1Id`=@userId WHERE `gameId` = @gameId";
            }
            else
            {
                query = "UPDATE `game` SET `player2Id`=@userId WHERE `gameId` = @gameId";
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

        public void UpdatePlayerPosition(PongGame pongGame)
        {
            //remvoe a user from the game
            string query = "UPDATE `game` SET `player1Position`= @player1Pos, `player2Position` = @player2Pos  WHERE `gameId` = @gameId";


            using (MySqlConnection connection = new MySqlConnection(DalConnection.Conn))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Add(new MySqlParameter("@player1Pos", pongGame.P1Pos));
                command.Parameters.Add(new MySqlParameter("@player2Pos", pongGame.P2Pos));
                //command.Parameters.Add(new MySqlParameter("@gameId", pongGame.GroupName));
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

        public void UpdateBal(PongGameDB pongGame)
        {
            //remvoe a user from the game
            string query = "UPDATE `game` SET `balDirection`= @balDir, `balX` = @balX, `balY` = @balY, `lastInput` = @lastInput  WHERE `gameId` = @gameId";


            using (MySqlConnection connection = new MySqlConnection(DalConnection.Conn))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Add(new MySqlParameter("@balDir", pongGame.BalDirection));
                command.Parameters.Add(new MySqlParameter("@balX", pongGame.BalX));
                command.Parameters.Add(new MySqlParameter("@balY", pongGame.BalY));
                command.Parameters.Add(new MySqlParameter("@gameId", pongGame.GameId));
                command.Parameters.Add(new MySqlParameter("@lastInput", DateTime.Now));
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

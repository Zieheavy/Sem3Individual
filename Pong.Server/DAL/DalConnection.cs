using System;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class DalConnection
    {
#pragma warning disable SA1401 // Fields should be private
        //public static string Conn = "Server=192.168.15.54;Uid=dbi419727;Database=dbi419727;Pwd=test;SslMode=none;";
        public static string Conn = "server=localhost;user id=root;database=pong;Pwd=;port=3307;SslMode=none;";
#pragma warning restore SA1401 // Fields should be private
    }
}

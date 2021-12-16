using System;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class DalConnection
    {
#pragma warning disable SA1401 // Fields should be private
        public const string Conn = "datasource=127.0.0.1;port=3306;username=root;password=;database=pong;";
#pragma warning restore SA1401 // Fields should be private
    }
}

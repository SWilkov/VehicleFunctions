using NPoco;
using System;
using VF.DataLayer.MySQL.Interfaces;

namespace VF.DataLayer.MySQL.Factories
{
  public class DbFactory : IDbFactory
  {
    public IDatabase Setup()
    {
      var connectionString = Environment.GetEnvironmentVariable("MYSQL_CONN_STR");
      return new Database(connectionString, DatabaseType.MySQL, MySql.Data.MySqlClient.MySqlClientFactory.Instance);
    }
  }
}

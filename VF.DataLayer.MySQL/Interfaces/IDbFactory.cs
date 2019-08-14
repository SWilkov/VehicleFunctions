using NPoco;

namespace VF.DataLayer.MySQL.Interfaces
{
  public interface IDbFactory
  {
    IDatabase Setup();
  }
}
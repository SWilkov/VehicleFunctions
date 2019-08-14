using Microsoft.Extensions.DependencyInjection;
using VF.DataLayer.Interfaces;
using VF.DataLayer.MySQL.Factories;
using VF.DataLayer.MySQL.Interfaces;
using VF.Framework.Models;

namespace VF.DataLayer.MySQL.Extensions
{
  public static class ServiceCollectionExtensions
  {
    public static void AddMySQLDataLayer(this IServiceCollection services)
    {
      #region Factories
      services.AddScoped<IDbFactory, DbFactory>();
      #endregion

      #region Repositories
      services.AddScoped<IReadRepository<VehicleQuery>, Repositories.VehicleQueryRepository>();
      #endregion      
    }
  }
}

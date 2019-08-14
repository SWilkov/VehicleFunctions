using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VF.DataLayer.Interfaces;
using VF.DataLayer.MySQL.DataModels;
using VF.DataLayer.MySQL.Interfaces;
using VF.Framework.Models;

namespace VF.DataLayer.MySQL.Repositories
{
  public class VehicleQueryRepository : IReadRepository<VehicleQuery>
  {
    private readonly IDbFactory _dbFactory;
    public VehicleQueryRepository(IDbFactory dbFactory)
    {
      _dbFactory = dbFactory;
    }

    public async Task<VehicleQuery> Get(int id)
    {
      using (var db = _dbFactory.Setup())
      {
        var dm = await db.SingleOrDefaultAsync<VehicleQueryDataModel>("WHERE [id] = @0", id);
        if (dm == null)
          return null;

        return new VehicleQuery(dm.Id, dm.CreatedAt, dm.VehicleId);
      }
    }

    public async Task<ICollection<VehicleQuery>> GetByDate(string startDate, string endDate)
    {
      using (var db = _dbFactory.Setup())
      {
        var dms = await db.FetchAsync<VehicleQueryDataModel>("WHERE [created_at] BETWEEN @0 AND @1",
          startDate, endDate);

        if (dms == null)
          return null;

        return dms.Select(x => new VehicleQuery(x.Id, x.CreatedAt, x.VehicleId)).ToList();
      }
    }


    public async Task<int> CountByDate(string startDate, string endDate)
    {
      using (var db = _dbFactory.Setup())
      {
        var count = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) " +
                                                     "FROM vehicledata.vdVehicleQueries " +
                                                     "WHERE (created_at BETWEEN @0 AND @1)",
          startDate, endDate);

        return count;
      }
    }
  }
}

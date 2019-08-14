using System;
using System.Threading.Tasks;
using VehicleFunctions.Models;

namespace VehicleFunctions.Interfaces
{
  public interface IVehicleQueryService
  {
    Task<VehicleQueryResult> GetByDate(DateTime startDate, DateTime endDate);
  }
}
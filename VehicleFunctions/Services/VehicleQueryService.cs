using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleFunctions.Interfaces;
using VehicleFunctions.Models;
using VF.DataLayer.Interfaces;
using VF.Framework.Models;
using VF.Utils.Interfaces;

namespace VehicleFunctions.Services
{
  public class VehicleQueryService : 
    IVehicleQueryService
  {
    private readonly IReadRepository<VehicleQuery> _vehicleQueryRepository;
    private readonly IDateTimeService _dateTimeService;
    public VehicleQueryService(IReadRepository<VehicleQuery> vehicleQueryRepository,
      IDateTimeService dateTimeService)
    {
      _vehicleQueryRepository = vehicleQueryRepository;
      _dateTimeService = dateTimeService;
    }

    public async Task<VehicleQueryResult> GetByDate(DateTime startDate, DateTime endDate)
    {
      if (startDate > endDate)
        throw new ArgumentException("Start Date is ahead of end date");

      var formattedStartDate = _dateTimeService.ConvertToString(startDate);
      var formattedEndDate = _dateTimeService.ConvertToString(endDate);

      var count = await _vehicleQueryRepository.CountByDate(formattedStartDate, formattedEndDate);

      return new VehicleQueryResult(_dateTimeService.ConvertToString(startDate, "dd MMMM yyyy HH:mm"), 
        _dateTimeService.ConvertToString(endDate, "dd MMMM yyyy HH:mm"), count);
    }
  }
}

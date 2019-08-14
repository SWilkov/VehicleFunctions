using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using VF.DataLayer.Interfaces;
using VF.Framework.Models;
using VF.Utils.Interfaces;
using VehicleFunctions.Interfaces;
using VehicleFunctions.Services;
using VehicleFunctions.Models;

namespace VehicleQueryAzureFunctionsTests
{
  public class VehicleQueryServiceTests
  {
    private Mock<IReadRepository<VehicleQuery>> _mVehicleQueryRepository;
    private Mock<IDateTimeService> _mDateTimeService;
    private IVehicleQueryService _service;
    public VehicleQueryServiceTests()
    {
      _mDateTimeService = new Mock<IDateTimeService>();
      _mVehicleQueryRepository = new Mock<IReadRepository<VehicleQuery>>();
      _service = new VehicleQueryService(_mVehicleQueryRepository.Object,
        _mDateTimeService.Object);
    }

    [Fact]
    public async Task start_date_is_ahead_of_end_date_throw_exception()
    {
      var startDate = new DateTime(2019, 8, 10, 9, 0, 0); //hour ahead of end date
      var endDate = new DateTime(2019, 8, 10, 8, 0, 0);

      await Assert.ThrowsAsync<ArgumentException>(async () =>
        await _service.GetByDate(startDate, endDate));
    }

    [Fact]
    public async Task return_vehicle_query_result()
    {
      var startDate = new DateTime(2019, 8, 10, 9, 0, 0); 
      var endDate = new DateTime(2019, 8, 10, 17, 0, 0);

      _mDateTimeService.SetupSequence(x => x.ConvertToString(It.IsAny<DateTime>(), It.IsAny<string>()))
        .Returns("2019 - 08 - 10 09:00:00")
        .Returns("2019 - 08 - 10 17:00:00")
        .Returns("10 August 2019 09:00")
        .Returns("10 August 2019 17:00");

      _mVehicleQueryRepository.Setup(x => x.CountByDate(It.IsAny<string>(), It.IsAny<string>()))
        .ReturnsAsync(5);

      var result = await _service.GetByDate(startDate, endDate);

      Assert.IsType<VehicleQueryResult>(result);
      Assert.Equal(5, result.NumberOfQueries);
    }
  }
}

using System;

namespace VehicleFunctions.Models
{
  public class VehicleQueryResult
  {
    public string StartDate { get; private set; }
    public string EndDate { get; private set; }
    public int NumberOfQueries { get; private set; } = 0;
    public VehicleQueryResult(string startDate, string endDate, int numberOfQueries)
    {
      this.StartDate = startDate;
      this.EndDate = endDate;
      this.NumberOfQueries = numberOfQueries;
    }
  }
}

using System;

namespace VF.Framework.Models
{
  public class VehicleQuery
  {
    public int Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public int VehicleId { get; private set; }
    public VehicleQuery(int id, DateTime createdAt, int vehicleId)
    {
      this.Id = id;
      this.CreatedAt = createdAt;
      this.VehicleId = vehicleId;
    }
  }
}

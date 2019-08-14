using NPoco;

namespace VF.DataLayer.MySQL.DataModels
{
  [TableName("vdVehicleQueries"), PrimaryKey("id")]
  public class VehicleQueryDataModel : BaseDataModel
  {
    [Column("vehicle_id")]
    public int VehicleId { get; set; }
  }
}

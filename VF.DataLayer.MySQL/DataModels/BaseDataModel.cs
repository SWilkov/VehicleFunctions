using NPoco;
using System;

namespace VF.DataLayer.MySQL.DataModels
{
  public class BaseDataModel
  {
    [Column("id")]
    public int Id { get; set; }
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
  }
}

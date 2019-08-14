using System;

namespace VF.Utils.Interfaces
{
  public interface IDateTimeService
  {
    string ConvertToString(DateTime dateTime, string format = "yyyy-MM-dd HH:mm:ss");
  }
}
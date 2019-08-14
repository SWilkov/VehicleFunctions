using System;
using VF.Utils.Interfaces;

namespace VF.Utils.Services
{
  public class DateTimeService : IDateTimeService
  {
    public string ConvertToString(DateTime dateTime, string format = "yyyy-MM-dd HH:mm:ss")
    {
      if (string.IsNullOrEmpty(format))
        throw new ArgumentNullException(nameof(format));

      return dateTime.ToString(format);
    }
  }
}

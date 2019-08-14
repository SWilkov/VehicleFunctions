using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VF.Utils.Interfaces
{
  public interface IReaderService<T>
    where T : class
  {
    Task<string> ReadToEndAsync(T item);
  }
}

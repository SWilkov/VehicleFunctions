using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VF.DataLayer.Interfaces
{
  public interface IReadRepository<T>
    where T: class
  {
    Task<T> Get(int id);
    Task<ICollection<T>> GetByDate(string startDate, string endDate);
    Task<int> CountByDate(string startDate, string endDate);
  }
}

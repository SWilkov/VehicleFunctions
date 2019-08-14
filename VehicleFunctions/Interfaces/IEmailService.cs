using System.Threading.Tasks;
using AW.Notifications.Email.Models;
using VehicleFunctions.Models;

namespace VehicleFunctions.Interfaces
{
  public interface IEmailService
  {
    Message Create(VehicleQueryResult result);
    Task<OutputResponse> Send(Message message);
  }
}
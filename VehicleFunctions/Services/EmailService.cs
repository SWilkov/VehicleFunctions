using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AW.Notifications.Email.Interfaces;
using AW.Notifications.Email.Models;
using AW.Utils.Interfaces;
using VehicleFunctions.Interfaces;
using VehicleFunctions.Models;

namespace VehicleFunctions.Services
{
  public class EmailService : IEmailService
  {
    private readonly IOutputService _outputService;
    private readonly IConfigService _configService;
    public EmailService(IOutputService outputService, IConfigService configService)
    {
      _outputService = outputService;
      _configService = configService;
    }

    public Message Create(VehicleQueryResult result)
    {
      if (result == null)
        throw new ArgumentNullException(nameof(result));

      var emailAddress = _configService.Get("SENDGRID_FROM_EMAIL");

      var message = new Message
      {
        Content = $"Number of Queries ({result.StartDate} - {result.EndDate}) = {result.NumberOfQueries}",
        Recipients = new List<EmailAddress>
        {
          new EmailAddress
          {
            Address = emailAddress,
            Name = "MOTLookup"
          }
        },
        From = new EmailAddress
        {
          Address = emailAddress,
          Name = "MOTLookup"
        },
        Subject = _configService.Get("SUBJECT")
      };

      return message;
    }

    public async Task<OutputResponse> Send(Message message)
    {
      if (message == null)
        throw new ArgumentNullException(nameof(message));

      var response = await _outputService.Send(message);

      if (!response.Success)
      {
        //TODO log
      }

      return response;
    }
  }
}

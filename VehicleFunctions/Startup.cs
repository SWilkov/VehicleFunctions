using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleQueryAzureFunctions;
using VF.Utils.Extensions;
using VF.DataLayer.MySQL.Extensions;
using AW.SendGrid.Extensions;
using VehicleFunctions.Interfaces;
using VehicleFunctions.Services;
using AW.Utils.Serialization.JsonNet.Extensions;

[assembly: FunctionsStartup(typeof(Startup))]
namespace VehicleQueryAzureFunctions
{
  public class Startup : FunctionsStartup
  {
    public override void Configure(IFunctionsHostBuilder builder)
    {
      //Utils
      builder.Services.AddVFUtils();

      //MySQL
      builder.Services.AddMySQLDataLayer();

      //SendGrid
      var sendGridApiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
      builder.Services.AddSendGrid(sendGridApiKey);

      //JsonNet
      builder.Services.AddJsonNetSerialization();

      //Services
      builder.Services.AddScoped<IVehicleQueryService, VehicleQueryService>();
      builder.Services.AddScoped<IEmailService, EmailService>();
    }
  }
}

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using VehicleFunctions.Interfaces;
using VF.Utils.Interfaces;
using AW.Utils.Serialization.Interfaces;
using VehicleFunctions.Models;

namespace VehicleFunctions
{  
  public class QueryCountFunction
  {
    private readonly IVehicleQueryService _vehicleQueryService;
    private readonly IEmailService _emailService;
    private readonly IReaderService<Stream> _readerService;
    private readonly IJsonConverter _jsonConverter;
    public QueryCountFunction(IVehicleQueryService vehicleQueryService,
      IEmailService emailService,
      IReaderService<Stream> readerService,
      IJsonConverter jsonConverter)
    {
      _vehicleQueryService = vehicleQueryService;
      _emailService = emailService;
      _readerService = readerService;
      _jsonConverter = jsonConverter;
    }

    [FunctionName("GetQueries")]
    public async Task<IActionResult> Post(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "queries")] HttpRequest req,
        ILogger log)
    {
      log.LogInformation("C# HTTP trigger function processed a request.");
      if (req == null)
        return new NotFoundObjectResult("Null HttpRequest");

      string requestBody = await _readerService.ReadToEndAsync(req.Body);
      if (string.IsNullOrEmpty(requestBody))
        return new BadRequestObjectResult("Could not find Start Date or End date");

      var data = _jsonConverter.DeserializeObject<VehicleQueryRequest>(requestBody);
      if (data == null)
        return new BadRequestObjectResult("VehicleQueryrequest is null after deserialization");

      try
      {
        var result = await _vehicleQueryService.GetByDate(data.StartDate, data.EndDate);
        //Send Email
        var msg = _emailService.Create(result);
        if (msg == null)
          log.LogWarning("Error creating Email");
        var response = await _emailService.Send(msg);

        log.LogInformation($"Output Response: {response.Success} - {response.Message}. Number of Queries: {result.NumberOfQueries}");

        return new OkObjectResult(result);
      }
      catch(Exception e)
      {
        log.LogInformation($"Exception occured. {e.Message}");
        throw e;
      }
    }
  }
}

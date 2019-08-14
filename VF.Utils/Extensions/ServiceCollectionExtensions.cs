using Microsoft.Extensions.DependencyInjection;
using System.IO;
using VF.Utils.Interfaces;
using VF.Utils.Services;

namespace VF.Utils.Extensions
{
  public static class ServiceCollectionExtensions
  {
    public static void AddVFUtils(this IServiceCollection services)
    {
      services.AddScoped<IDateTimeService, DateTimeService>();
      services.AddScoped<IReaderService<Stream>, StreamReaderService>();
    }
  }
}

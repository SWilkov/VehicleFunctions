using System.IO;
using System.Threading.Tasks;

namespace VF.Utils.Services
{
  public class StreamReaderService : 
    Interfaces.IReaderService<Stream>
  {
    public async Task<string> ReadToEndAsync(Stream stream)
    {
      if (stream == null) //TODO log
        return string.Empty;

      return await new StreamReader(stream).ReadToEndAsync();
    }
  }
}

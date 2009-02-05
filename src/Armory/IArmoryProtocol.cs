using System.IO;

namespace Armory
{
    public interface IArmoryProtocol
    {
        /// <summary>
        /// Gets the response stream.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        Stream GetResponse(string uri);
    }
}
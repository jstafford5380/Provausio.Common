using System.Threading.Tasks;

namespace Provausio.Common
{
    public interface IRateLimiter
    {
        /// <summary>
        /// Waits for the next available slot to run.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task Wait(string key);
    }
}
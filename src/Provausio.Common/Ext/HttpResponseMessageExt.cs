using System.Net.Http;
using Newtonsoft.Json;

namespace Provausio.Common.Ext
{
    public static class HttpWebResponseExt
    {
        public static T Deserialize<T>(this HttpResponseMessage response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
        }
    }
}

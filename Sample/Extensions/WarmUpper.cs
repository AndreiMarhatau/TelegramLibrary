using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sample.Extensions
{
    public static class WarmUpper
    {
        public static async void WarmUpApp()
        {
            await new HttpClient().SendAsync(new HttpRequestMessage()
            {
                RequestUri = new Uri("http://localhost:50077/telegram/warmup"),
                Method = HttpMethod.Get
            });
        }
    }
}

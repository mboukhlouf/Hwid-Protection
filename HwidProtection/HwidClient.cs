using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HwidProtection
{
    public class HwidClient
    {
        public string Host { get; set; }

        public HwidClient(string host)
        {
            host = host.TrimEnd('/');
            Host = host;
        }
        
        public async Task<bool> VerifyAsync()
        {
            using var client = new HttpClient();

            string hwid = HwidHelper.GetHwid();
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("hwid", hwid)
            });
            var response = await client.PostAsync(Host + "/Hwid/Verify", content);
            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }
    }
}

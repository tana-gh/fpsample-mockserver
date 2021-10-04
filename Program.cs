using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace fpsample_mockserver
{
    class Program
    {
        static readonly string url     = "http://localhost:8080/";
        static readonly string content = "{\"name\":\"John Smith\",\"age\":20}";

        static async Task Main(string[] args)
        {
            using var listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();

            Console.WriteLine($"Listening on {url}");

            for (;;)
            {
                var context = await listener.GetContextAsync();
                var req = context.Request;
                var res = context.Response;
                
                Console.WriteLine($"Requested from {req.UserHostAddress} to {req.Url.AbsolutePath} {req.HttpMethod}");

                if (req.Url.AbsolutePath == "/" && req.HttpMethod == "GET")
                {
                    var bytes = Encoding.UTF8.GetBytes(content);

                    res.StatusCode      = (int)HttpStatusCode.OK;
                    res.ContentType     = "application/json";
                    res.ContentEncoding = Encoding.UTF8;
                    res.ContentLength64 = bytes.LongLength;

                    await res.OutputStream.WriteAsync(bytes, 0, bytes.Length);
                }
                else
                {
                    res.StatusCode = (int)HttpStatusCode.NotFound;
                }

                res.Close();
            }
        }
    }
}

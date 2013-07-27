using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HarHar;

namespace Runner
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var uri = new Uri("http://www.google.com", UriKind.Absolute);
            //var request = WebRequest.CreateHttp(uri);

            //var handler = new HttpClientHandler();
            //var client = new HttpClient(handler);
            var cookies = new CookieContainer();
            //handler.CookieContainer = cookies;

            //var log = new Log(client);
            var log = new Log();

            MakeCalls(log, uri).ConfigureAwait(false);
            while (!_done)
            {
                Thread.Sleep(1);
            }

            var logJson = log.ToJson();
            Clipboard.SetText(logJson);
            Console.WriteLine(logJson);
            Console.ReadKey();
        }

        private static bool _done;

        async private static Task MakeCalls(Log log, Uri uri)
        {
            try
            {
                //log.StartPage("test.xaml");
                var cookies = new CookieContainer();
                var request = WebRequest.CreateHttp(uri);
                request.CookieContainer = cookies;
                await request.SendAndLogAsync(log);

                request = WebRequest.CreateHttp(uri);
                request.CookieContainer = cookies;
                request.Method = "POST";
                request.ContentType = "application/json";
                var json = "{\"html\":{\"head\":{},\"body\":{\"h1\":\"This is a test!\"}}}";
                var bytes = Encoding.UTF8.GetBytes(json);
                var stream = new MemoryStream(bytes);
                request.ContentLength = bytes.Length;

                await request.SendAndLogAsync(log, null, stream);
            }
            catch (Exception e)
            {
                
            }
            finally
            {
                _done = true;
            }
        }

        async private static Task MakeCalls(HttpClient client, HttpClientHandler handler, Log log, Uri uri)
        {
            try
            {
                var message = new HttpRequestMessage(HttpMethod.Get, uri);
                await client.SendAndLogAsync(message, log);
                //var requestCookie = string.Join(";", message.Headers.GetValues("Cookie"));
                var cookies = handler.CookieContainer.GetCookies(uri).Cast<Cookie>();
                message = new HttpRequestMessage(HttpMethod.Get, uri);
                await client.SendAndLogAsync(message, log);
                var headerString = message.Headers.ToString();

                var requestCookie = string.Join(";", message.Headers.GetValues("Cookie"));
                cookies = handler.CookieContainer.GetCookies(uri).Cast<Cookie>();
            }
            catch (Exception e)
            {
                var i = 0;
            }
            finally
            {
                _done = true;
            }
        }
    }
}

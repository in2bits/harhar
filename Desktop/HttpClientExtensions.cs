using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HarHar
{
    public static class HttpClientExtensions
    {
        async public static Task<HttpResponseMessage> SendAndLogAsync(this HttpClient client, HttpRequestMessage message, Log log, string page = null)
        {
            if (log.Entries == null)
                log.Entries = new List<Entry>();

            var entry = new Entry();
            entry.PageRef = page;
            log.Entries.Add(entry);
            entry.Request = await message.GetRequestInfo();
            if (message.Content != null)
                message.Content = new HarHttpContent(entry, message.Content);
            var start = DateTime.Now;
            entry.StartedDateTime = start;
            //TODO: Handle WebException (try posting to www.yahoo.com)
            var response = await client.SendAsync(message);
            entry.Timings.Receive = (int)DateTime.Now.Subtract(entry.StartedDateTime).Subtract(TimeSpan.FromMilliseconds(entry.Timings.Send)).TotalMilliseconds;
            entry.Time = entry.Timings.GetTotal();
            entry.Response = await response.GetResponseInfo();
            return response;
        }

        async public static Task<RequestInfo> GetRequestInfo(this HttpRequestMessage message)
        {
            var info = new RequestInfo();
            info.Method = message.Method.Method;
            info.Url = message.RequestUri.OriginalString;
            info.HttpVersion = message.Version.ToString();

            //TODO: SetCookies(message);
            //TODO: SetHeaders(message);
            //TODO: SetPostData(message);
            //HeadersSize
            //BodySize
            info.Comment = "HttpRequestMessage";
            return info;
        }

        async public static Task<ResponseInfo> GetResponseInfo(this HttpResponseMessage message)
        {
            var info = new ResponseInfo();
            info.Status = (int)message.StatusCode;
            info.StatusText = message.ToString();
            info.HttpVersion = message.Version.ToString();
            //TODO: SetCookies(message);
            //TODO: SetHeaders(message);
            //TODO: SetContent(message);
            if (message.Headers.Location != null)
                info.RedirectUrl = message.Headers.Location.OriginalString;
            //TODO: HeadersSize
            //TODO: BodySize
            info.Comment = "HttpResponseMessage";
            return info;
        }
    }

    public class HarHttpContent : HttpContent
    {
        private readonly Entry _entry;
        private readonly HttpContent _innerContent;

        public HarHttpContent(Entry entry, HttpContent innerContent)
        {
            var stringContent = innerContent as StringContent;
            _entry = entry;
            _innerContent = innerContent;
        }

        async protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            if (_innerContent == null)
            {
                _entry.Timings.Send = 0;
                return;
            }

            await _innerContent.CopyToAsync(stream);
            _entry.Timings.Send = (int)DateTime.Now.Subtract(_entry.StartedDateTime).TotalMilliseconds;
        }

        protected override bool TryComputeLength(out long length)
        {
            length = 0;
            return true;
        }
    }

    public class HarHttpRequestMessage : HttpRequestMessage
    {

    }
}

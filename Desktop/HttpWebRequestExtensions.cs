using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace HarHar
{
    public static class HttpWebRequestExtensions
    {
        async public static Task<HttpWebResponse> SendAndLogAsync(this HttpWebRequest request, Log log, string page = null, Stream sendContent = null)
        {
            if (log.Entries == null)
                log.Entries = new List<Entry>();

            var entry = new Entry();
            entry.PageRef = page;
            log.Entries.Add(entry);
            entry.Request = await request.GetRequestInfo();
            if (sendContent != null)
            {
                var postData = await sendContent.ToPostData(request.ContentType);
                entry.Request.PostData = postData;
                using (var requestStream = await Task.Factory.FromAsync<Stream>(request.BeginGetRequestStream, request.EndGetRequestStream, null))
                    await sendContent.CopyToAsync(requestStream);
            }

            var start = DateTime.Now;
            entry.StartedDateTime = start;
            HttpWebResponse response;
            try
            {
                response =
                    await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null) as
                    HttpWebResponse;
            }
            catch (WebException wex)
            {
                response = wex.Response as HttpWebResponse;
            }
            catch (Exception ex)
            {
                response = null;
                var i = 0;
            }
            entry.Timings.Receive = (int)DateTime.Now.Subtract(entry.StartedDateTime).Subtract(TimeSpan.FromMilliseconds(entry.Timings.Send)).TotalMilliseconds;
            entry.Time = entry.Timings.GetTotal();
            entry.Response = await response.GetResponseInfo();
            return response;
        }

        async public static Task<RequestInfo> GetRequestInfo(this HttpWebRequest request)
        {
            var info = new RequestInfo();
            info.Method = request.Method;
            info.Url = request.RequestUri.OriginalString;
            info.HttpVersion = request.ProtocolVersion.ToString();

            //SetCookies(request.CookieContainer);
            info.Headers = await request.GetHeaderInfo();
            //TODO: SetPostData(message);
            //HeadersSize
            //BodySize
            info.Comment = "HttpWebRequest";
            return info;
        }

        async public static Task<ResponseInfo> GetResponseInfo(this HttpWebResponse response)
        {
            var info = new ResponseInfo();
            info.Status = (int) response.StatusCode;
            info.StatusText = response.StatusDescription;
            info.HttpVersion = response.ProtocolVersion.ToString();

            //info.Cookies = await response.Cookies.GetCookieInfo();
            info.Headers = await response.GetHeaderInfo();
            info.Content = await response.GetContentInfo();
            if (response.ResponseUri != null)
                info.RedirectUrl = response.ResponseUri.OriginalString;
            //TODO: HeadersSize
            //TODO: BodySize
            info.Comment = "HttpResponseMessage";
            return info;
        }

        async public static Task<IList<CookieInfo>> GetCookieInfo(this CookieCollection cookieCollection)
        {
            throw new NotImplementedException();
        } 

        async public static Task<IList<CookieInfo>> GetCookieInfo(this CookieContainer cookieContainer)
        {
            throw new NotImplementedException();
        }

        async public static Task<IList<NameValuePairInfo>> GetHeaderInfo(this HttpWebRequest request)
        {
            var headers = new List<NameValuePairInfo>();
            headers.Add(new NameValuePairInfo { Name = "Host", Value = request.Host });
            headers.Add(new NameValuePairInfo { Name = "Connection", Value = request.KeepAlive ? "Keep-Alive" : request.Connection });
            var webHeaders = request.Headers;
            foreach (string name in webHeaders.Keys)
            {
                var values = webHeaders.GetValues(name);
                string valueList = null;
                if (values != null)
                    valueList = string.Join(",", values);
                headers.Add(new NameValuePairInfo { Name = name, Value = valueList });
            }
            return headers;
        }

        async public static Task<IList<NameValuePairInfo>> GetHeaderInfo(this HttpWebResponse response)
        {
            var headers = new List<NameValuePairInfo>();
            //responseInfo.Headers.Add(new NameValuePairInfo { Name = "Host", Value = request.Host });
            //responseInfo.Headers.Add(new NameValuePairInfo { Name = "Connection", Value = request.KeepAlive ? "Keep-Alive" : request.Connection });
            var webHeaders = response.Headers;
            foreach (string name in webHeaders.Keys)
            {
                var values = webHeaders.GetValues(name);
                string valueList = null;
                if (values != null)
                    valueList = string.Join(",", values);
                headers.Add(new NameValuePairInfo { Name = name, Value = valueList });
            }
            return headers;
        }

        async public static Task<ContentInfo> GetContentInfo(this HttpWebResponse response)
        {
            var stream = response.GetResponseStream();
            if (stream == null)
                return null;
            var content = new ContentInfo();
            var position = 0L;
            if (stream.CanSeek)
                position = stream.Position;
            var reader = new StreamReader(stream, Encoding.UTF8, false, 1024, true);
            content.Text = await reader.ReadToEndAsync();
            if (stream.CanSeek)
                stream.Position = position;
            content.MimeType = response.ContentType;
            return content;
        }
    }
}

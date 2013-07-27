using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarHar
{
    public static class StreamExtensions
    {
        async public static Task<PostData> ToPostData(this Stream stream, string mimeType = null)
        {
            var post = new PostData();
            var position = stream.Position;
            var reader = new StreamReader(stream, Encoding.UTF8, false, 1024, true);
            post.Text = await reader.ReadToEndAsync();
            if (stream.CanSeek)
                stream.Position = position;
            post.MimeType = mimeType;
            return post;
        }
    }
}

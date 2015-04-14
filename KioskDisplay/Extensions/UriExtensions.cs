using System;
using System.IO;
using System.Linq;

namespace KioskDisplay.Extensions
{
    internal static class UriExtensions
    {
        public static bool IsVideo(this Uri uri)
        {
            var ext = Path.GetExtension(uri.Segments.Last());
            var videoTypes = new[] { ".mp4", ".avi", ".wmv", ".mov" };
            return videoTypes.Contains(ext);
        }
    }
}

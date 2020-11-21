using System;
using System.IO;

namespace CleanMyDesktop.Core.Extensions
{
    public static class Extensions
    {
        public static T To<T>(this object obj) where T : struct
        {
            if (obj is T t)
                return t;

            var tbase = Nullable.GetUnderlyingType(typeof(T));
            if (tbase != null)
            {
                if (typeof(T).IsEnum)
                    return (T)Enum.Parse(typeof(T), obj.ToString());

                if (obj == null)
                    return default;
                return (T)Convert.ChangeType(obj, tbase);
            }
            else
            {
                return (T)Convert.ChangeType(obj, typeof(T));
            }
        }

        public static T To<T>(this object obj, T defvalue) where T : struct
        {
            if (obj is T t)
                return t;

            var tbase = Nullable.GetUnderlyingType(typeof(T));
            if (tbase != null)
            {
                if (obj == null)
                    return defvalue;
                return (T)Convert.ChangeType(obj, tbase);
            }
            else
            {
                return (T)Convert.ChangeType(obj, typeof(T));
            }
        }

        public static string FileNameWithoutExtension(this FileInfo fileInfo)
        {
            return Path.GetFileNameWithoutExtension(fileInfo.Name);
        }

        public static string FileExtension(this FileInfo fileInfo)
        {
            return Path.GetExtension(fileInfo.Name);
        }

        public static T ToEnum<T>(this string value) where T : struct
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static string ToFileSize(this long filesize)
        {
            var index = 0;
            for (; filesize > 1024; index++)
                filesize /= 1024;
            return filesize.ToString("0 " + new[] { "B", "KB", "MB", "GB", "TB" }[index]);
        }

        public static string SurroundWith(this string value, string start, string end)
        {
            return string.Concat(start, value, end);
        }
    }
}

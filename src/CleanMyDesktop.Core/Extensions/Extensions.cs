using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CleanMyDesktop.Core.Extensions
{
    public static class Extensions
	{
		public static string GetValue(this NameValueCollection collection, string configKey, string defaultValue = "")
		{
			return collection.AllKeys.Contains(configKey) ? collection[configKey] : defaultValue;
		}

		public static T To<T>(this object obj) where T : struct
		{
			if (obj is T)
				return (T)obj;

			var tbase = Nullable.GetUnderlyingType(typeof(T));
			if (tbase != null)
			{
				if (typeof(T).IsEnum)
					return (T)Enum.Parse(typeof(T), obj.ToString());

				if (obj == null)
					return default(T);
				return (T)Convert.ChangeType(obj, tbase);
			}
			else
			{
				return (T)Convert.ChangeType(obj, typeof(T));
			}
		}

		public static List<T> ToList<T>(this object obj, string seperator)
		{
			return obj == null ? new List<T>() : obj.ToString().Split(seperator.ToCharArray()).Select(x => (T)Convert.ChangeType(x, typeof(T))).ToList();
		}

		public static T To<T>(this object obj, T defvalue) where T : struct
		{
			if (obj is T)
				return (T)obj;

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

		public static string GetDescription(this System.Enum en)
		{
			var memInfo = en.GetType().GetMember(en.ToString());
			if (memInfo.Length > 0)
			{
				var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
				if (attrs.Length > 0)
				{
					return ((DescriptionAttribute)attrs[0]).Description;
				}
			}
			return en.ToString();
		}

		public static dynamic ToExpandoObject(this object obj)
		{
			IDictionary<string, object> result = new ExpandoObject();
			foreach (var p in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(w => w.CanRead))
			{
				result[p.Name] = p.GetValue(obj, null);
			}
			return result;
		}
		public static void Each<T>(this IEnumerable<T> list, Action<T> action)
		{
			if (list == null) return;
			foreach (var item in list)
				action(item);
		}

		public static Task EachAsync<T>(this IEnumerable<T> list, Action<T> action)
		{
			return Task.Run(() =>
			{
				foreach (var item in list)
					action(item);
			});
		}

		public static ConcurrentDictionary<TKey, TValue> ToConcurrentDictionary<TKey, TValue>(this IEnumerable<TValue> source, Func<TValue, TKey> keySelector)
		{
			return new ConcurrentDictionary<TKey, TValue>(source.ToDictionary(keySelector));
		}

		public static string FormatString(this string value, params object[] args)
		{
			return string.Format(value, args);
		}

		public static string FormatString(this string value, object arg)
		{
			return string.Format(value, arg);
		}

		public static string FormatStringAuto(this string seperator, params object[] arg)
		{
			return string.Join(seperator, arg);
		}

		public static bool IsNull(this object value)
		{
			return value == null;
		}

		public static bool IsNullorWhiteSpace(this string value)
		{
			return string.IsNullOrWhiteSpace(value);
		}

		public static bool IsNotNullorWhiteSpace(this string value)
		{
			return !string.IsNullOrWhiteSpace(value);
		}

		public static string IsNullorWhiteSpace(this string value, string defaultValue)
		{
			return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
		}

		public static bool IsNullOrEmpty(this string value)
		{
			return string.IsNullOrEmpty(value);
		}

		public static string IsNullOrEmpty(this string value, string defaultValue)
		{
			return string.IsNullOrEmpty(value) ? defaultValue : value;
		}

		public static bool IsNumeric(this string value)
		{
			return Regex.IsMatch(value, @"[0-9]", RegexOptions.Compiled);
		}

		public static string Repeat(this char input, int count)
		{
			return new string(input, count);
		}

		public static string Truncate(this string value, int maxLength)
		{
			if (value.IsNullOrEmpty()) return value;
			return value.Length <= maxLength ? value : value.Substring(0, maxLength);
		}

		public static string ToBase64(this string value, Encoding encoding = null)
		{
			if (value.IsNullorWhiteSpace()) return value;
			return Convert.ToBase64String((encoding ?? Encoding.UTF8).GetBytes(value));
		}

		public static string FromBase64(this string value, Encoding encoding = null)
		{
			if (value.IsNullorWhiteSpace()) return value;
			return (encoding ?? Encoding.UTF8).GetString(Convert.FromBase64String(value));
		}
		public static string ToFileSize(this long filesize)
		{
			var index = 0;
			for (; filesize > 1024; index++)
				filesize /= 1024;
			return filesize.ToString("0 " + new[] { "B", "KB", "MB", "GB", "TB" }[index]);
		}

		public static string CapitaliseName(this string text)
		{
			if (string.IsNullOrEmpty(text))
				return string.Empty;

			const string targets = "- ";
			var culture = new CultureInfo("en-US");
			var capitalise = true;
			var result = new StringBuilder(text.Length);
			foreach (var c in text)
			{
				if (capitalise)
				{
					result.Append(char.ToUpper(c, culture));
					capitalise = false;
				}
				else
				{
					if (targets.Contains(c.ToString()))
						capitalise = true;

					result.Append(c);
				}
			}
			return result.ToString();
		}

		public static string ExtractBetween(this string text, string start, string end, StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase)
		{
			int startIndex = text.IndexOf(start, stringComparison);
			if (startIndex < 0) return string.Empty;
			startIndex += start.Length;

			int endIndex = text.IndexOf(end, startIndex, stringComparison);
			if (endIndex < 0) return string.Empty;

			return text.Substring(startIndex, endIndex - startIndex);
		}

		public static bool ContainsAnyOf(this string text, params string[] args)
		{
			return !text.IsNullOrEmpty() && args.Select(w => w.ToLower()).Any(text.ToLower().Contains);
		}

		public static bool ContainsAllOf(this string text, IEnumerable<string> args)
		{
			return !text.IsNullOrEmpty() && args.All(text.Contains);
		}

		public static string ToTitleCase(this string s, string culture = "en-US")
		{
			if (string.IsNullOrEmpty(s))
				return string.Empty;

			var ci = new CultureInfo(culture);
			s = s.ToLower(ci).Trim();

			var splittedString = s.Split(" -".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

			s = string.Empty;

			foreach (var str in splittedString)
				s += char.ToUpper(str[0], ci) + str.Substring(1) + " ";

			s = s.Trim();

			return s;
		}

		public static string ToUpperFirstLetter(this string source)
		{
			if (string.IsNullOrEmpty(source))
			{
				return string.Empty;
			}
			var letters = source.ToCharArray();
			letters[0] = char.ToUpper(letters[0]);
			return new string(letters);
		}

		public static string NullIfEmpty(this string source)
		{
			if (string.IsNullOrEmpty(source))
			{
				return null;
			}
			source = source.Trim();
			return 0 == source.Length ? null : source;
		}

		public static string EmptyIfNull(this string source)
		{
			return source == null ? string.Empty : source;
		}

		public static Guid ToGuid(this string source)
		{
			byte[] stringbytes = Encoding.UTF8.GetBytes(source);
			byte[] hashedBytes = new System.Security.Cryptography
				.SHA1CryptoServiceProvider()
				.ComputeHash(stringbytes);
			Array.Resize(ref hashedBytes, 16);
			return new Guid(hashedBytes);
		}

		public static string SurroundWith(this string value, string start, string end)
		{
			return string.Concat(start, value, end);
		}


		public static void AddOrUpdate(this IDictionary source, string key, object value)
		{
			if (!source.Contains(key))
				source.Add(key, value);
			else
				source[key] = value;
		}

		public static bool AddWhen<T>(this List<T> source, T value, Func<bool> condition)
		{
			if (condition.Invoke())
			{
				source.Add(value);
				return true;
			}
			return false;
		}

		public static bool AddWhen<T>(this List<T> source, T value, bool condition)
		{
			if (condition)
			{
				source.Add(value);
				return true;
			}
			return false;
		}
	}
}

namespace Skyline.Framework.Core
{
	using System;
	using System.Collections.Generic;
	using System.Collections.Specialized;
	using System.Linq;

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
			if (obj == null)
				return new List<T>();

			return (obj as string).Split(seperator.ToCharArray()).Select(x => (T)Convert.ChangeType(x, typeof(T))).ToList();
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
	}
}

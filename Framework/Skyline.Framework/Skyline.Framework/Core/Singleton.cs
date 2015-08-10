namespace Skyline.Framework.Core
{
	using System;
	public class Singleton<T> : Disposable where T : class
	{
		private static readonly Lazy<T> _instance = new Lazy<T>(CreateInstance);

		public static T Instance { get { return _instance.Value; } }

		private static T CreateInstance()
		{
			return Activator.CreateInstance(typeof(T), true) as T;
		}
	}
}

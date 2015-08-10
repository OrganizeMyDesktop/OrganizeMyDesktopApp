namespace Skyline.Framework.Core
{
	using System.Configuration;

	public static class AppSettingsManager
	{
		public static string GetValue(string key, string defaultValue = "")
		{
			return ConfigurationManager.AppSettings.GetValue(key, defaultValue);
		}
	}
}

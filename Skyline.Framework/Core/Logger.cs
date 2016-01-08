using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Concurrent;

namespace Skyline.Framework.Core
{
	public static class Logger
	{
		public static ILog Log<T>(this T type)
		{
			var loggerName = typeof(T).FullName;
			return Log(loggerName);
		}

		public static ILog LogWith(string loggerName)
		{
			return LogManager.GetLogger(loggerName);
		}

		static void Setup()
		{
			var hierarchy = (Hierarchy)LogManager.GetRepository();

			var patternLayout = new PatternLayout();
			patternLayout.ConversionPattern = "%date [%thread] %-5level %logger - %message%newline";
			patternLayout.ActivateOptions();

			var fileAppender = new RollingFileAppender();
			fileAppender.AppendToFile = false;
			fileAppender.File = @"Logs\EventLog.txt";
			fileAppender.Layout = patternLayout;
			fileAppender.MaxSizeRollBackups = 5;
			fileAppender.MaximumFileSize = "1GB";
			fileAppender.RollingStyle = RollingFileAppender.RollingMode.Size;
			fileAppender.StaticLogFileName = true;
			fileAppender.ActivateOptions();
			hierarchy.Root.AddAppender(fileAppender);

			//MemoryAppender memoryAppender = new MemoryAppender();
			//memoryAppender.ActivateOptions();
			//hierarchy.Root.AddAppender(memoryAppender);

			hierarchy.Root.Level = Level.All;
			hierarchy.Configured = true;
		}

		static Logger()
		{
			Setup();
		}
	}
}

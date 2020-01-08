using System;
using System.IO;

namespace CleanMyDesktop.Core.IO
{
    public class FileSystemWatch : IDisposable
	{
		Lazy<FileSystemWatcher> _watchFolder;
		string _path;
		NotifyFilters _filter;
		public FileSystemWatch(string path, NotifyFilters filter)
		{
			_watchFolder = new Lazy<FileSystemWatcher>();
			_path = path;
			_filter = filter;
		}

		public event FileSystemEventHandler Changed;
		public event FileSystemEventHandler Created;
		public event FileSystemEventHandler Deleted;
		public event RenamedEventHandler Renamed;
		public event ErrorEventHandler Error;

		public void Start()
		{
			_watchFolder.Value.Path = _path;
			_watchFolder.Value.NotifyFilter = _filter;
			_watchFolder.Value.Changed += Changed;
			_watchFolder.Value.Created += Created;
			_watchFolder.Value.Deleted += Deleted;
			_watchFolder.Value.Renamed += Renamed;
			_watchFolder.Value.Error += Error;
			_watchFolder.Value.EnableRaisingEvents = true;
		}

		public void Stop()
		{
			if (_watchFolder.IsValueCreated)
			{
				_watchFolder.Value.EnableRaisingEvents = false;
			}
		}

		public bool Enabled
		{
			get { return _watchFolder.IsValueCreated && _watchFolder.Value.EnableRaisingEvents; }
		}

		public void Dispose()
		{
			if (_watchFolder.IsValueCreated)
			{
				_watchFolder.Value.EnableRaisingEvents = false;
				_watchFolder.Value.Dispose();
			}
			GC.SuppressFinalize(this);
		}
	}
}

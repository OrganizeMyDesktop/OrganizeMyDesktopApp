using System;
using System.IO;

namespace CleanMyDesktop.Core.IO
{
    public class FileSystemWatch : IFileSystemWatch
    {
        private bool _initialized;
        private readonly bool _includeSubdirectories;
        private FileSystemWatcher _watchFolder;
        private bool disposedValue;

        public FileSystemWatch(string path, string filter, NotifyFilters notificationFilter, bool includeSubdirectories = true)
        {
            Path = path;
            NotificationFilter = notificationFilter;
            Filter = filter;
            _includeSubdirectories = includeSubdirectories;
        }

        public event FileSystemEventHandler Changed;
        public event FileSystemEventHandler Created;
        public event FileSystemEventHandler Deleted;
        public event RenamedEventHandler Renamed;
        public event ErrorEventHandler Error;

        public string Path { get; }
        public string Filter { get; }
        public NotifyFilters NotificationFilter { get; }

        public void Start()
        {
            if (!_initialized)
            {
                _watchFolder = new FileSystemWatcher(Path, Filter)
                {
                    IncludeSubdirectories = _includeSubdirectories,
                    NotifyFilter = NotificationFilter
                };

                AttachEvents();

                _initialized = true;
            }
            _watchFolder.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            if (_initialized)
            {
                _watchFolder.EnableRaisingEvents = false;
                DetachEvents();
                _watchFolder.Dispose();
            }
        }

        public bool Enabled
        {
            get { return _initialized && _watchFolder.EnableRaisingEvents; }
        }

        public bool IncludeSubdirectories
        {
            get { return _initialized && _watchFolder.IncludeSubdirectories; }
        }

        private void AttachEvents()
        {
            if (Changed != null)
                _watchFolder.Changed += Changed;
            if (Created != null)
                _watchFolder.Created += Created;
            if (Deleted != null)
                _watchFolder.Deleted += Deleted;
            if (Renamed != null)
                _watchFolder.Renamed += Renamed;
            if (Error != null)
                _watchFolder.Error += Error;
        }

        private void DetachEvents()
        {
            if (_watchFolder != null)
            {
                if (Changed != null)
                    _watchFolder.Changed -= Changed;
                if (Created != null)
                    _watchFolder.Created -= Created;
                if (Deleted != null)
                    _watchFolder.Deleted -= Deleted;
                if (Renamed != null)
                    _watchFolder.Renamed -= Renamed;
                if (Error != null)
                    _watchFolder.Error -= Error;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Stop();
                    if (Enabled)
                    {
                        DetachEvents();
                        _watchFolder?.Dispose();
                    }
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

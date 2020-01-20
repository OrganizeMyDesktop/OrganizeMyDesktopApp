using System;
using System.IO;

namespace CleanMyDesktop.Core.IO
{
    public class FileSystemWatch : IFileSystemWatch
    {
        private bool initialized = false;
        private FileSystemWatcher _watchFolder;
        private string _path { get; set; }
        private string _filter { get; set; }
        private NotifyFilters _notificationFilter { get; set; }
        private bool _includeSubdirectories { get; set; }

        public FileSystemWatch(string path, string filter, NotifyFilters notificationFilter, bool includeSubdirectories = true)
        {
            _path = path;
            _notificationFilter = notificationFilter;
            _filter = filter;
            _includeSubdirectories = includeSubdirectories;
        }

        public event FileSystemEventHandler Changed;
        public event FileSystemEventHandler Created;
        public event FileSystemEventHandler Deleted;
        public event RenamedEventHandler Renamed;
        public event ErrorEventHandler Error;

        public string Path { get { return _path; } }
        public string Filter { get { return _filter; } }
        public NotifyFilters NotificationFilter { get { return _notificationFilter; } }

        public void Start()
        {
            if (!initialized)
            {
                _watchFolder = new FileSystemWatcher(_path, _filter)
                {
                    IncludeSubdirectories = _includeSubdirectories,
                    NotifyFilter = _notificationFilter
                };

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

                initialized = true;
            }
            _watchFolder.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            if (initialized)
            {
                _watchFolder.EnableRaisingEvents = false;
            }
        }

        public bool Enabled
        {
            get { return initialized && _watchFolder.EnableRaisingEvents; }
        }

        public bool IncludeSubdirectories
        {
            get { return initialized && _watchFolder.IncludeSubdirectories; }
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

        public void Dispose()
        {
            Stop();
            if (_watchFolder != null)
            {
                DetachEvents();
                _watchFolder.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}

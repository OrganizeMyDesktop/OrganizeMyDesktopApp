using System;
using System.IO;

namespace CleanMyDesktop.Core.IO
{
    public class FileSystemWatch : IFileSystemWatch
    {
        private readonly Lazy<FileSystemWatcher> _watchFolder;
        private readonly string _path;
        private readonly NotifyFilters _filter;
        private readonly bool _includeSubdirectories;

        public FileSystemWatch(string path, NotifyFilters filter, bool includeSubdirectories = true)
        {
            _watchFolder = new Lazy<FileSystemWatcher>(() =>
            {
                var fileSystemWatcher = new FileSystemWatcher();
                if (Changed != null)
                    fileSystemWatcher.Changed += Changed;
                if (Created != null)
                    fileSystemWatcher.Created += Created;
                if (Deleted != null)
                    fileSystemWatcher.Deleted += Deleted;
                if (Renamed != null)
                    fileSystemWatcher.Renamed += Renamed;
                if (Error != null)
                    fileSystemWatcher.Error += Error;
                fileSystemWatcher.IncludeSubdirectories = _includeSubdirectories;
                return fileSystemWatcher;
            });
            _path = path;
            _filter = filter;
            _includeSubdirectories = includeSubdirectories;
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

        public bool IncludeSubdirectories
        {
            get { return _watchFolder.IsValueCreated && _watchFolder.Value.IncludeSubdirectories; }
        }

        public void Dispose()
        {
            if (_watchFolder.IsValueCreated)
            {
                _watchFolder.Value.EnableRaisingEvents = false;
                if (Changed != null)
                    _watchFolder.Value.Changed -= Changed;
                if (Created != null)
                    _watchFolder.Value.Created -= Created;
                if (Deleted != null)
                    _watchFolder.Value.Deleted -= Deleted;
                if (Renamed != null)
                    _watchFolder.Value.Renamed -= Renamed;
                if (Error != null)
                    _watchFolder.Value.Error -= Error;
                _watchFolder.Value.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}

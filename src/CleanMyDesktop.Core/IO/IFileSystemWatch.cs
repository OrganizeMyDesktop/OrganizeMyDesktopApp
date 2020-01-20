using System;
using System.IO;

namespace CleanMyDesktop.Core.IO
{
    public interface IFileSystemWatch : IDisposable
    {
        bool Enabled { get; }
        bool IncludeSubdirectories { get; }
        string Path { get; }
        string Filter { get; }
        NotifyFilters NotificationFilter { get; }

        event FileSystemEventHandler Changed;
        event FileSystemEventHandler Created;
        event FileSystemEventHandler Deleted;
        event ErrorEventHandler Error;
        event RenamedEventHandler Renamed;
        void Start();
        void Stop();
    }
}
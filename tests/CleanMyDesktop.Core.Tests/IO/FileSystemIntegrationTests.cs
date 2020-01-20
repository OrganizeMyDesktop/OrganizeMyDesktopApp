using FluentAssertions;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace CleanMyDesktop.Core.IO.Tests
{
    public class FileSystemIntegrationTests : IDisposable
    {
        private readonly string testFolder;
        public FileSystemIntegrationTests()
        {
            testFolder = Path.Combine(Path.GetDirectoryName(GetType().GetTypeInfo().Assembly.Location), "testfolder");
            if (!Directory.Exists(testFolder)) Directory.CreateDirectory(testFolder);
        }

        private string CombineFileName(string fileName)
        {
            return Path.Combine(testFolder, fileName);
        }

        [Fact]
        public void CreateNewFileTest()
        {
            IFileSystemWatch fileSystemWatch = new FileSystemWatch(testFolder, "*.txt", NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.FileName
                                 | NotifyFilters.DirectoryName, true);
            using (var monitoredSubject = fileSystemWatch.Monitor())
            {
                var fileName = Guid.NewGuid().ToString() + ".txt";
                fileSystemWatch.Start();
                Task.Delay(1000);
                File.WriteAllText(CombineFileName(fileName), Guid.NewGuid().ToString());
                Task.Delay(1000);
                monitoredSubject.Should().Raise("Created")
                .WithArgs<FileSystemEventArgs>(args => args.ChangeType == WatcherChangeTypes.Created);
            }

        }

        [Fact]
        public void FileDeleteTest()
        {
            const string fileName = "testfiled.txt";
            var fullDir = CombineFileName(fileName);
            File.WriteAllText(fullDir, Guid.NewGuid().ToString());
            IFileSystemWatch fileSystemWatch = new FileSystemWatch(testFolder, "*.txt", NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.FileName
                                 | NotifyFilters.DirectoryName, true);
            using (var monitoredSubject = fileSystemWatch.Monitor())
            {
                fileSystemWatch.Start();
                Task.Delay(1000);
                File.Delete(fullDir);
                Task.Delay(1000);
                monitoredSubject.Should().Raise("Deleted")
                .WithArgs<FileSystemEventArgs>(args => args.ChangeType == WatcherChangeTypes.Deleted);
            }
        }

        [Fact]
        public void FileChangeTest()
        {
            const string fileName = "testfilec.txt";
            var fullDir = CombineFileName(fileName);
            File.WriteAllText(fullDir, Guid.NewGuid().ToString());

            using (var fileSystemWatch = new FileSystemWatch(testFolder, "*.txt", NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.FileName
                                 | NotifyFilters.Size
                                 | NotifyFilters.DirectoryName, true))
            using (var monitoredSubject = fileSystemWatch.Monitor())
            {
                fileSystemWatch.Start();
                Task.Delay(1000);
                File.AppendAllText(fullDir, Guid.NewGuid().ToString());
                Task.Delay(100);
                monitoredSubject.Should().Raise("Changed")
                .WithArgs<FileSystemEventArgs>(args => args.Name == fileName && args.ChangeType == WatcherChangeTypes.Changed);
            }
        }

        public void Dispose()
        {
            if (Directory.Exists(testFolder))
                Directory.Delete(testFolder, true);
        }
    }
}
using System;
using System.IO;
using YamlParser;
using kmeans;
using Mapper;

namespace Watcher
{

    public static class FileWatcher
    {
        public static string FilePath;
        public static void Run()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = FilePath;

            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            
            // Only watch yaml files.
            watcher.Filter = "*.yaml";

           // watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
           // watcher.Deleted += new FileSystemEventHandler(OnChanged);
            //watcher.Renamed += new RenamedEventHandler(OnRenamed);

            // Begin watching.
            watcher.EnableRaisingEvents = true;

        }

        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
            var YamlParser = new Parser();
            var data = YamlParser.Parse(e.FullPath);
            
            var kmeans = new Kmeans();
            kmeans.DataSet = DataMapper.ToAttempts(data);
            kmeans.Clusters = 4;
            kmeans.Iterations = 100;
            kmeans.Run();
            kmeans.PrintClusters();
        }

    }

}
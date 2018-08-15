using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace Indeksator
{
    public abstract class Indexer
    {
        public FileSystemWatcher Watcher { get; set; }
        public string Filter { get; set; }
        public string StopIndex { get; set; }
        public string ThreadName { get; set; }

        public string RootFolder { get; set; } = "files";
        public string IndexFormat { get; set; } = ".~index";

        public void RunWatcher()
        {
            Watcher.Path = RootFolder;
            Watcher.IncludeSubdirectories = true;
            Watcher.Filter = Filter;

            Watcher.Changed += new FileSystemEventHandler(OnChanged);

            Watcher.EnableRaisingEvents = true;

            while (Console.ReadLine() != StopIndex) ;
        }

        public abstract void OnChanged(object sender, FileSystemEventArgs e);

        public void RunThread()
        {
            ThreadStart threadStart = new ThreadStart(RunWatcher);
            Thread thread = new Thread(threadStart);
            thread.Name = ThreadName;
            thread.Start();
        }
    }
}

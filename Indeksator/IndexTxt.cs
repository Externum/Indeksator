using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Threading;

namespace Indeksator
{
    public class IndexTxt : Indexer
    {

        public IndexTxt()
        {
            Watcher = new FileSystemWatcher();
            Filter = "*.txt";
            StopIndex = "stop txt";
            ThreadName = "IndexTxt";
        }

        public override void OnChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                Watcher.EnableRaisingEvents = false;
                Console.WriteLine("Файл изменен " + e.Name);
                string text = File.ReadAllText(RootFolder + "/" + e.Name, Encoding.Default);

                if(!File.Exists(RootFolder + "/" + e.Name + IndexFormat))
                {
                    File.Create(RootFolder + "/" + e.Name + IndexFormat).Close();
                    DataTxt dataTxt = new DataTxt(CountWords(text));
                    File.WriteAllText(RootFolder + "/" + e.Name + IndexFormat, JsonConvert.SerializeObject(dataTxt));
                }
                else
                {
                    DataTxt dataTxt = JsonConvert.DeserializeObject<DataTxt>(File.ReadAllText(RootFolder + "/" + e.Name + IndexFormat));
                    dataTxt.words = CountWords(text);
                    File.WriteAllText(RootFolder + "/" + e.Name + IndexFormat, JsonConvert.SerializeObject(dataTxt));
                }

            }
            finally
            {
                Watcher.EnableRaisingEvents = true;
            }
        }

        public int CountWords(string txt)
        {
            string text = txt;
            string[] words;
            while(text.Contains("  "))
            {
                text.Replace("  "," ");
            }
            words = text.Split(' ');

            return words.Length;
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Drawing;

namespace Indeksator
{
    class IndexImg : Indexer
    {

        public IndexImg()
        {
            Watcher = new FileSystemWatcher();
            Filter = "*.png";
            StopIndex = "stop img";
            ThreadName = "IndexImg";
        }

        public override void OnChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                Watcher.EnableRaisingEvents = false;
                Console.WriteLine("Файл изменен " + e.Name);
                using (Bitmap bmp = new Bitmap(e.FullPath))
                {
                    if (!File.Exists(RootFolder + "/" + e.Name + IndexFormat))
                    {
                        File.Create(RootFolder + "/" + e.Name + IndexFormat).Close();
                        DataImg dataImg = new DataImg(bmp.Height, bmp.Width);
                        File.WriteAllText(RootFolder + "/" + e.Name + IndexFormat, JsonConvert.SerializeObject(dataImg));
                    }
                    else
                    {
                        DataImg dataImg = JsonConvert.DeserializeObject<DataImg>(File.ReadAllText(RootFolder + "/" + e.Name + IndexFormat));
                        dataImg.height = bmp.Height;
                        dataImg.width = bmp.Width;
                        File.WriteAllText(RootFolder + "/" + e.Name + IndexFormat, JsonConvert.SerializeObject(dataImg));
                    }
                }
            }
            finally
            {
                Watcher.EnableRaisingEvents = true;
            }
        }
    }
}

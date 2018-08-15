using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Drawing;

namespace Indeksator
{
    class StartIndex
    {
        public string RootFolder { get; set; } = "files";
        public string IndexFormat { get; set; } = ".~index";

        public void RunIndexTxt()
        {
            foreach (string file in Directory.EnumerateFiles(RootFolder, "*.txt", SearchOption.AllDirectories))
            {
                string text = File.ReadAllText(file, Encoding.Default);

                if (!File.Exists(file + IndexFormat))
                {
                    File.Create(file + IndexFormat).Close();
                    DataTxt dataTxt = new DataTxt(CountWords(text));
                    File.WriteAllText(file + IndexFormat, JsonConvert.SerializeObject(dataTxt));
                }
                else
                {
                    DataTxt dataTxt = JsonConvert.DeserializeObject<DataTxt>(File.ReadAllText(file + IndexFormat));
                    dataTxt.words = CountWords(text);
                    File.WriteAllText(file + IndexFormat, JsonConvert.SerializeObject(dataTxt));
                }
            }
        }

        public void RunIndexImg()
        {
            foreach (string file in Directory.EnumerateFiles(RootFolder, "*.png", SearchOption.AllDirectories))
            {
                using (Bitmap bmp = new Bitmap(file))
                {
                    if (!File.Exists(file + IndexFormat))
                    {
                        File.Create(file + IndexFormat).Close();
                        DataImg dataImg = new DataImg(bmp.Height, bmp.Width);
                        File.WriteAllText(file + IndexFormat, JsonConvert.SerializeObject(dataImg));
                    }
                    else
                    {
                        DataImg dataImg = JsonConvert.DeserializeObject<DataImg>(File.ReadAllText(file + IndexFormat));
                        dataImg.height = bmp.Height;
                        dataImg.width = bmp.Width;
                        File.WriteAllText(file + IndexFormat, JsonConvert.SerializeObject(dataImg));
                    }
                }
            }
        }

        public int CountWords(string txt)
        {
            string text = txt;
            string[] words;
            while (text.Contains("  "))
            {
                text.Replace("  ", " ");
            }
            words = text.Split(' ');

            return words.Length;
        }
    }
}

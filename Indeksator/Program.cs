using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Timers;
using System.Drawing;
using System.Threading;

namespace Indeksator
{
    class Program
    {
        static void Main(string[] args)
        {
            StartIndex startIndex = new StartIndex();
            startIndex.RunIndexTxt();
            startIndex.RunIndexImg();
            IndexTxt indTxt = new IndexTxt();
            IndexImg indImg = new IndexImg();
            indImg.RunThread();
            indTxt.RunThread();
        }
    }
}

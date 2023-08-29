using System;
using System.IO.Compression;
using System.Net;

namespace Extract
{
    class Program
    {
        static void Main(string[] args)
        {
            //WebClient wc = new WebClient();

            //Console.WriteLine("Downloading file");

            //wc.DownloadFile("https://embed.widencdn.net/download/kavokerr/msivwrexdx/KaVo_IO-Sensor_Driver.1.0.6.96.zip", "\\titanium.zip");

            //Console.WriteLine("Finished Downloading File");

            Console.WriteLine("Starting Extraction");
            ZipFile.ExtractToDirectory("titanium.zip", @"..\setup.old");
            Console.WriteLine("Finished Extraction");
        }
    }
}

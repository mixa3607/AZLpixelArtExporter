using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;

namespace AZLpixelArtExporter
{
    class Program
    {
        static void Main(string[] args)
        {
            int cellHeight = 20;
            int cellWidth = 20;
            bool isRemovePrev = args.Length > 3 && args[2] == "-r";

            if (args.Length >= 2)
            {
                cellHeight = Convert.ToInt32(args[0]);
                cellWidth = Convert.ToInt32(args[1]);
            }
            else
            {
                Console.WriteLine($"Use: {System.Diagnostics.Process.GetCurrentProcess().ProcessName} <PixelHeight> <PixelWidth> [-r]");
                throw new Exception();
            }

            if (Directory.Exists(OutImgFolder) && isRemovePrev)
            {
                Directory.Delete(OutImgFolder, true);
                Console.WriteLine("Prev images deleted");
            }
            Console.Write("Rendering images...");
            var templates = PixelArtExporter.GetColoringTemplates();
            var bitmaps = PixelArtExporter.GetColoredBitmaps(templates, cellHeight, cellWidth);
            Console.WriteLine("OK");

            SaveBitmaps(bitmaps, templates, isRemovePrev);
        }

        private const string OutImgFolder = "./IMG/";
        static void SaveBitmaps(Bitmap[] bitmaps, ColoringTemplate[] templates, bool isRemovePrev)
        {
            if (!Directory.Exists(OutImgFolder))
            {
                Directory.CreateDirectory(OutImgFolder);
                Console.WriteLine("Folder for images created");
            }
                

            if (templates.Length != bitmaps.Length)
            {
                Console.WriteLine("Templates len != bitmaps len. Aborted!");
                throw new Exception();
            }

            for (int i = 0; i < bitmaps.Length; i++)
            {
                var currentBitmap = bitmaps[i];
                var currentTemplate = templates[i];
                Console.Write($"{i+1}/{bitmaps.Length} saving... ");
                currentBitmap.Save($"{OutImgFolder}{i+1} {currentTemplate.Name}.png", ImageFormat.Png);
                Console.WriteLine("OK");
            }
        }
    }

    
}

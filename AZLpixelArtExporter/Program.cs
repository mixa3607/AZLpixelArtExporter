using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace AZLpixelArtExporter
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isRemovePrev = args.Length > 0 && args[0] == "-r";
            Console.Write("Rendering images...");
            var templates = PixelArtExporter.GetColoringTemplates();
            var bitmaps = PixelArtExporter.GetColoredBitmaps(templates);
            Console.WriteLine("OK");

            SaveBitmaps(bitmaps, templates, isRemovePrev);
        }

        private const string OutImgFolder = "./IMG/";
        static void SaveBitmaps(Bitmap[] bitmaps, ColoringTemplate[] templates, bool isRemovePrev)
        {
            if (Directory.Exists(OutImgFolder) && isRemovePrev)
            {
                Directory.Delete(OutImgFolder, true);
                Console.WriteLine("Prev images deleted");
            }

            if (!Directory.Exists(OutImgFolder))
                Directory.CreateDirectory(OutImgFolder);

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

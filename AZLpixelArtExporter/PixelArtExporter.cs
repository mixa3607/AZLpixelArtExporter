using System;
using System.Drawing;
using System.Drawing.Imaging;
using Newtonsoft.Json;
using NLua;
// ReSharper disable PossibleNullReferenceException

namespace AZLpixelArtExporter
{
    public static class PixelArtExporter
    {
        
        private const string BaseLuaFolder = "./LuaScripts/";

        public static ColoringTemplate[] GetColoringTemplates()
        {
            var luaState = new Lua();
            luaState.DoString($"package.path = package.path .. ';{BaseLuaFolder}?.lua'");
            var jsonColoringTemplates = luaState.DoFile(BaseLuaFolder + "GetColoringTemplates.lua")[0] as string;
            ColoringTemplate[] coloringTemplates = JsonConvert.DeserializeObject<ColoringTemplate[]>(jsonColoringTemplates);

            return coloringTemplates;
        }

        public static Bitmap[] GetColoredBitmaps(ColoringTemplate[] coloringTemplates, int cellLength = 20, int cellWeight = 20)
        {
            Bitmap[] renderedBitmaps = new Bitmap[coloringTemplates.Length];
            for (int i = 0; i < coloringTemplates.Length; i++)
                renderedBitmaps[i] = MakePrint(coloringTemplates[i], Color.FromArgb(255, 212, 212, 212));

            return renderedBitmaps;
        }

        private static Bitmap MakePrint(ColoringTemplate template, Color bgColor)
        {
            int pxPerCell = 20;
            int height = template.Height * pxPerCell;
            int width = template.Length * pxPerCell;

            var bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            bitmap = ColorRectangle(bitmap, bgColor, 0, 0, bitmap.Width, bitmap.Height);
            foreach (var cell in template.Cells)
            {

                bitmap = ColorRectangle(bitmap, 
                    template.Colors[cell.ColorNum - 1], 
                    cell.X * pxPerCell,
                    (template.Height - cell.Y - 1) * pxPerCell, 
                    pxPerCell, 
                    pxPerCell);
            }

            return bitmap;
        }

        //private static Bitmap MakePrintDev(ColoringTemplate template, Color bgColor, int cellLength, int cellWeight)
        //{
        //
        //}
        //
        private static unsafe Bitmap ColorRectangle(Bitmap bitmap, Color color, int x, int y, int width, int height)
        {
            try
            {
                var lockArea = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                var bitmapData = bitmap.LockBits(lockArea, ImageLockMode.ReadWrite, bitmap.PixelFormat);
                var data = (byte*)bitmapData.Scan0;
                var bpp = bitmapData.Stride / bitmap.Width;

                for (int currentX = x; currentX < x + width; currentX++)
                {
                    for (int currentY = y; currentY < y + height; currentY++)
                    {
                        int startPixelIndex = ((bitmap.Height - currentY - 1) * bitmap.Width + currentX) * bpp;
                        data[startPixelIndex++] = color.B;
                        data[startPixelIndex++] = color.G;
                        data[startPixelIndex++] = color.R;
                        data[startPixelIndex] = color.A;
                    }
                }

                bitmap.UnlockBits(bitmapData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return bitmap;
        }
    }
}

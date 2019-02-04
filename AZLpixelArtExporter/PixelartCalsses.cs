using Newtonsoft.Json;
using System.Drawing;

namespace AZLpixelArtExporter
{
    public class ColoringTemplate
    {
        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("colorIds")]
        public int[] ColorIds { get; set; }

        [JsonProperty("blank")]
        public int Blank { get; set; }

        [JsonProperty("cells")]
        public Cell[] Cells { get; set; }

        [JsonProperty("colors")]
        public Color[] Colors { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("length")]
        public int Length { get; set; }
    }

    public class Cell
    {
        [JsonProperty("y")]
        public int Y { get; set; }

        [JsonProperty("x")]
        public int X { get; set; }

        [JsonProperty("colorNum")]
        public int ColorNum { get; set; }
    }
}

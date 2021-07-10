using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LtxMerger
{
    class TextureCollection
    {
        public SortedList<string, string> Associations { get; set; } = new SortedList<string, string>();
        public SortedList<string, string> Specifications { get; set; } = new SortedList<string, string>();
        public SortedList<string, string> Types { get; set; } = new SortedList<string, string>();

        public TextureCollection Deserialize(string text)
        {
            return this;
        }

        public string Serialize()
        {
            return "";
        }
    }
}

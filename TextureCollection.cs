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

        public TextureCollection(string text)
        {
            this.Deserialize(text);
        }

        public TextureCollection Deserialize(string text)
        {
            return this;
        }

        public string Serialize()
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine("[association]");
            foreach (KeyValuePair<string, string> association in this.Associations)
            {
                output.AppendLine($"        {association.Key}       = {association.Value}");
            }
            output.AppendLine("[specification]");
            foreach (KeyValuePair<string, string> specification in this.Specifications)
            {
                output.AppendLine($"        {specification.Key}       = {specification.Value}");
            }
            output.AppendLine("[types]");
            foreach (KeyValuePair<string, string> type in this.Types)
            {
                output.AppendLine($"        {type.Key}       = {type.Value}");
            }
            return output.ToString();
        }

        public static TextureCollection operator +(TextureCollection x, TextureCollection y)
        {
            TextureCollection textureCollection = x;
            foreach (KeyValuePair<string, string> association in y.Associations)
            {
                textureCollection.Associations.Add(association.Key, association.Value);
            }
            foreach (KeyValuePair<string, string> specification in y.Specifications)
            {
                textureCollection.Specifications.Add(specification.Key, specification.Value);
            }
            foreach (KeyValuePair<string, string> type in y.Types)
            {
                textureCollection.Types.Add(type.Key, type.Value);
            }
            return textureCollection;
        }
    }
}

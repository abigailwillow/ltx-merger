using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LtxMerger
{
    class TextureCollection
    {
        public SortedList<string, string> Associations { get; set; } = new SortedList<string, string>();
        public SortedList<string, string> Specifications { get; set; } = new SortedList<string, string>();
        public SortedList<string, int> Types { get; set; } = new SortedList<string, int>();
        private Regex _regex = new Regex(@"(?<key>[a-zA-Z0-9\\\.,:_ \[\]]+)\s+=\s+(?<value>[a-zA-Z0-9\\\.,:_ \[\]]+)");

        public TextureCollection(string text)
        {
            this.Deserialize(text);
        }

        public TextureCollection Deserialize(string text)
        {
            bool associationFound = false;
            bool specificationFound = false;
            bool typesFound = false;
            string[] lines = text.Split('\n');
            foreach (string line in lines)
            {
                associationFound = line.Contains("[association]") && (!specificationFound && !typesFound);
                specificationFound = line.Contains("[specification]") && (!associationFound && !typesFound);
                typesFound = line.Contains("[types]") && (!associationFound && !specificationFound);
                GroupCollection groups = _regex.Match(line).Groups;
                if (associationFound)
                {
                    this.Associations.Add(groups["key"].Value, groups["value"].Value);
                }
                if (specificationFound)
                {
                    this.Specifications.Add(groups["key"].Value, groups["value"].Value);
                }
                if (typesFound)
                {
                    this.Types.Add(groups["key"].Value, int.Parse(groups["value"].Value));
                }
            }
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
            foreach (KeyValuePair<string, int> type in this.Types)
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
            foreach (KeyValuePair<string, int> type in y.Types)
            {
                textureCollection.Types.Add(type.Key, type.Value);
            }
            return textureCollection;
        }
    }
}

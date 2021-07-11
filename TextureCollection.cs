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
        private Regex _regex = new Regex(@"(?<key>[a-z0-9\\\.,:_ \[\]]+)\s+=\s+(?<value>[a-z0-9\\\.,:_ \[\]]+)", RegexOptions.IgnoreCase);

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
                GroupCollection groups = _regex.Match(line).Groups;
                if (associationFound)
                {
                    Associations.Add(groups["key"].Value, groups["value"].Value);
                }
                if (specificationFound)
                {
                    Specifications.Add(groups["key"].Value, groups["value"].Value);
                }
                if (typesFound)
                {
                    Types.Add(groups["key"].Value, int.Parse(groups["value"].Value));
                }
                associationFound = line.Contains("[association]") && (!specificationFound && !typesFound);
                specificationFound = line.Contains("[specification]") && (!associationFound && !typesFound);
                typesFound = line.Contains("[types]") && (!associationFound && !specificationFound);
            }
            return this;
        }

        public string Serialize()
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine("[association]");
            foreach (KeyValuePair<string, string> association in Associations)
            {
                output.AppendLine($"\t\t{association.Key} = {association.Value}");
            }
            output.AppendLine("[specification]");
            foreach (KeyValuePair<string, string> specification in Specifications)
            {
                output.AppendLine($"\t\t{specification.Key} = {specification.Value}");
            }
            output.AppendLine("[types]");
            foreach (KeyValuePair<string, int> type in Types)
            {
                output.AppendLine($"\t\t{type.Key} = {type.Value}");
            }
            return output.ToString();
        }

        public static TextureCollection operator +(TextureCollection x, TextureCollection y)
        {
            TextureCollection textureCollection = x;
            foreach (KeyValuePair<string, string> association in y.Associations)
            {
                textureCollection.Associations[association.Key] = association.Value;
            }
            foreach (KeyValuePair<string, string> specification in y.Specifications)
            {
                textureCollection.Specifications[specification.Key] = specification.Value;
            }
            foreach (KeyValuePair<string, int> type in y.Types)
            {
                textureCollection.Types[type.Key] = type.Value;
            }
            return textureCollection;
        }
    }
}

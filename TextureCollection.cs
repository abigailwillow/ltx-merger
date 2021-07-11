using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LtxMerger {
    class TextureCollection {
        public SortedList<string, string> Associations { get; set; } = new SortedList<string, string>();
        public SortedList<string, string> Specifications { get; set; } = new SortedList<string, string>();
        public SortedList<string, int> Types { get; set; } = new SortedList<string, int>();
        private Regex _regex = new Regex(@"\s+(?<key>.+)\s+=\s+(?<value>.+)", RegexOptions.IgnoreCase);

        public TextureCollection(string text) {
            this.Deserialize(text);
        }

        public TextureCollection Deserialize(string text) {
            GroupType lastGroup = GroupType.Unknown;
            string[] lines = text.Split('\n');
            foreach (string line in lines) {
                GroupCollection groups = _regex.Match(line).Groups;
                lastGroup = line.Contains("[association]") ? GroupType.Assocation : lastGroup;
                lastGroup = line.Contains("[specification]") ? GroupType.Specification : lastGroup;
                lastGroup = line.Contains("[types]") ? GroupType.Types : lastGroup;
                if (groups.Count > 2) {
                    switch (lastGroup) {
                        case GroupType.Assocation:
                            Associations[groups["key"].Value] = groups["value"].Value;
                            break;
                        case GroupType.Specification:
                            Specifications[groups["key"].Value] = groups["value"].Value;
                            break;
                        case GroupType.Types:
                            Types[groups["key"].Value] = int.Parse(groups["value"].Value);
                            break;
                        case GroupType.Unknown:
                            break;
                        default:
                            break;
                    }
                }
            }
            return this;
        }

        public string Serialize() {
            StringBuilder output = new StringBuilder();
            output.AppendLine("[association]");
            foreach (KeyValuePair<string, string> association in Associations) {
                output.AppendLine($"\t\t{association.Key} = {association.Value}");
            }
            output.AppendLine("[specification]");
            foreach (KeyValuePair<string, string> specification in Specifications) {
                output.AppendLine($"\t\t{specification.Key} = {specification.Value}");
            }
            output.AppendLine("[types]");
            foreach (KeyValuePair<string, int> type in Types) {
                output.AppendLine($"\t\t{type.Key} = {type.Value}");
            }
            return output.ToString();
        }

        public static TextureCollection operator +(TextureCollection x, TextureCollection y) {
            TextureCollection textureCollection = x;
            foreach (KeyValuePair<string, string> association in y.Associations) {
                textureCollection.Associations[association.Key] = association.Value;
            }
            foreach (KeyValuePair<string, string> specification in y.Specifications) {
                textureCollection.Specifications[specification.Key] = specification.Value;
            }
            foreach (KeyValuePair<string, int> type in y.Types) {
                textureCollection.Types[type.Key] = type.Value;
            }
            return textureCollection;
        }

        private enum GroupType {
            Unknown,
            Assocation,
            Specification,
            Types
        }
    }
}

using System;
using System.IO;

namespace LtxMerger
{
    class Program
    {
        /// <param name="source">The original textures.ltx file</param>
        /// <param name="mods">The file from which the mods should be used</param>
        /// <param name="output">The file to output the merged collection to</param>
        static void Main(FileInfo source, FileInfo mods, FileInfo output)
        {
            output = output ?? source;
            if (!source.Exists) { Console.WriteLine($"Could not find file {source.FullName}"); return; }
            if (!mods.Exists) { Console.WriteLine($"Could not find file {mods.FullName}"); return; }
            TextureCollection sourceCollection = new TextureCollection(File.ReadAllText(source.FullName));
            TextureCollection modCollection = new TextureCollection(File.ReadAllText(mods.FullName));
            Console.WriteLine($"Merging {mods.Name} into {output.Name}, please wait...");
            sourceCollection += modCollection;
            File.WriteAllText(output.FullName, sourceCollection.Serialize());
            Console.WriteLine($"Finished merging to {output.Name}");
        }
    }
}

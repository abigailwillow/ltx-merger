using System;
using System.IO;

namespace LtxMerger
{
    class Program
    {
        /// <param name="source">The original textures.ltx file</param>
        /// <param name="mods">The file from which the mods should be used</param>
        static void Main(FileInfo source, FileInfo mods)
        {
            // TODO: Check if files actually exist
            TextureCollection sourceCollection = new TextureCollection(File.ReadAllText(source.FullName));
            TextureCollection modCollection = new TextureCollection(File.ReadAllText(mods.FullName));
            Console.WriteLine($"Merging {mods.Name} into {source.Name}, please wait...");
            sourceCollection += modCollection;
            Console.WriteLine($"Finished merging to {source.Name}");
        }
    }
}

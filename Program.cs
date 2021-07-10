using System;
using System.IO;

namespace LtxMerger
{
    class Program
    {
        /// <param name="sourceFile">The original textures.ltx file</param>
        /// <param name="modFile">The file from which the mods should be used</param>
        /// <param name="output">The file to write to</param>
        static void Main(FileInfo sourceFile, FileInfo modFile, FileInfo output)
        {
            // TODO: Check if files actually exist
            Console.WriteLine(File.ReadAllText(sourceFile.FullName));
            Console.WriteLine(File.ReadAllText(modFile.FullName));
            Console.WriteLine(output.FullName);
            Console.ReadKey();
        }
    }
}

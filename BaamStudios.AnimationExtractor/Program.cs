using System;
using System.IO;

namespace BaamStudios.AnimationExtractor
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide the path to an fbx file.");
                return -1;
            }

            var filename = args[0];
            if (!File.Exists(filename) || !Path.HasExtension(filename) || Path.GetExtension(filename).ToLower() != ".fbx")
            {
                Console.WriteLine("Please provide the path to an fbx file.");
                return -1;
            }

            Console.WriteLine("Extracting animation from \"" + args[0] + "\"");
            
            var contentBuilder = new ContentBuilder();
            contentBuilder.Clear();
            contentBuilder.Add(filename, "Model", null, "AnimationExtractorModelProcessor");

            string buildError = contentBuilder.Build();

            if (string.IsNullOrEmpty(buildError))
            {
                var animPath = Path.Combine(contentBuilder.OutputDirectory, "Model.anim");
                if (File.Exists(animPath))
                {
                    var destPath = Path.ChangeExtension(filename, ".anim");
                    if (File.Exists(destPath)) File.Delete(destPath);
                    File.Move(animPath, destPath);
                }
            }
            else
            {
                Console.WriteLine(buildError);
                return -1;
            }

            return 0;
        }
    }
}

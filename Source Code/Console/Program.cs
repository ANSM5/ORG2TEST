using JsonHelper;
using Report;

namespace Console
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                var fileHelper = new FileHelper.FileHelper();

                var sourcePath = args[0];
                var targetPath = args[1];

                if (fileHelper.FileExists(sourcePath))
                {
                    if (fileHelper.IsCorrectFileType(sourcePath, "json"))
                    {
                        var fileContent = fileHelper.ReadFileContent(sourcePath);

                        var jsonDeserialiser = new JsonDeserialise();

                        var deserialisedFile = jsonDeserialiser.Deserialise(fileContent);

                        var reportCreator = new Creator();

                        var report = reportCreator.Generate(deserialisedFile);

                        fileHelper.WriteTextFile(targetPath, report);

                        Console.WriteLine($"Report successfully created at: {targetPath}");
                    }
                    else
                    {
                        Console.WriteLine("A JSON file is required");
                    }
                }
                else
                {
                    Console.WriteLine("No file exists at the given path");
                }
            }
            else
            {
                Console.WriteLine("Incorrect number of arguments passed. A source file and destination target are required. Press any key to close the application");
            }
        }
    }
}

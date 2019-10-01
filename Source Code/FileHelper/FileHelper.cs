using System;
using System.IO;

namespace FileHelper
{
    public class FileHelper
    {
        public bool FileExists(string path)
        {
            try
            {
                return File.Exists(path);
            }
            catch (Exception e)
            {
                throw new Exception($"Failed when checking if the file exists at: {path}. Error: {e.Message}", e);
            }
        }

        public bool IsCorrectFileType(string path, string expectedFileType)
        {
            try
            {
                return Path.GetExtension(path) == $".{expectedFileType}";
            }
            catch (Exception e)
            {
                throw new Exception($"Failed when checking file type at path {path}. Error: {e.Message}", e);
            }
        }

        public string ReadFileContent(string path)
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to read the file from the path: {path}. Error: {e.Message}", e);
            }
        }

        public void WriteTextFile(string path, string content)
        {
            try
            {
                File.WriteAllText(path, content);
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to write the content to the target file at path: {path}", e);
            }
        }
    }
}

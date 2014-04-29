using System.Collections.Generic;
using System.IO;

namespace HisogramResearch.Utils
{
    public class DirectionIO
    {
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".JPE", ".BMP", ".GIF", ".PNG" };

        public static bool IsFileImage(string file)
        {
            var extension = Path.GetExtension(file);
            if (extension != null && ImageExtensions.Contains(extension.ToUpperInvariant()))
            {
                return true;
            }
            return false;
        }
        private static string _path = "";
        public static string GetPath()
        {
            if (_path.Length == 0)
            {
                var systemPArth = System.Reflection.Assembly.GetExecutingAssembly().Location;
                _path = systemPArth.Remove(systemPArth.LastIndexOf("\\", System.StringComparison.Ordinal));
            }
            return _path.Clone() as string;
        }

        public static bool IsExistNewFolder(string folderName)
        {
            if (Directory.Exists(folderName))
                return true;
            return false;
        }

        public static string CreateNewFolder(string folderName)
        {

            if (!IsExistNewFolder(folderName))
                Directory.CreateDirectory(folderName);

            return folderName;
        }

        public static bool IsExistFile(string filePath)
        {
            if (File.Exists(filePath))
                return true;
            return false;
        }
        
        public static void WriteAllText(string filePath, string value)
        {
            File.WriteAllText(filePath, value);
        }
        public static string ReadAllText(string filePath)
        {
           return File.ReadAllText(filePath);
        }

        public static void RemoveFile(string s)
        {
            if (File.Exists(s))
            {
                File.Delete(s);
            }
        }
    }
}

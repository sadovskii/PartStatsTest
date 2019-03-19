using PartStats.BLL.Interfacies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PartStats.BLL
{
    public class FileSystemManager : FileManagerBase, IFileManager
    {
        public FileSystemManager(IFileValidate validFile) : base(validFile) {  }

        public void Work(string path)
        {
            string[] allfiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            Dictionary<string, int> vs = new Dictionary<string, int>();

            foreach (var a in allfiles)
            {
                var file = new FileInfo(a);

                if (file.Exists && this.validFile.CheckFileEncoding(file, Encoding.UTF8))
                {
                    string[] details = this.GetContentFromFile(file.Extension, file.FullName, Encoding.UTF8);

                    this.SelectCorrectRecords(vs, details);
                }
            }

            this.SaveInfoToFile(vs);
        }
    }
}

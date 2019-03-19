using PartStats.BLL.Factories;
using PartStats.BLL.Interfacies;
using PartStats.BLL.Utils;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PartStats.BLL
{
    public  class FileManagerBase
    {
        protected readonly IFileValidate validFile;

        public FileManagerBase(IFileValidate validFile)
        {
            this.validFile = validFile;
        }

        protected void SaveInfoToFile(Dictionary<string, int> vs)
        {
            if (vs.Count != 0)
            {
                using (TextWriter tw = new StreamWriter(Constans.ResultFile, false, Encoding.UTF8))
                {
                    foreach (var s in vs)
                        tw.WriteLine(string.Format("{0}, {1}", s.Key, s.Value));
                }
            }
        }

        protected void SelectCorrectRecords(Dictionary<string, int> vs, string[] details)
        {
            foreach (var str in details)
                if (validFile.Check(str))
                {
                    var result = str.Split(',').Select(t => t.Trim().ToLower()).ToArray();
                    var value = int.Parse(result[1]);

                    if (vs.ContainsKey(result[0]))
                        vs[result[0]] += value;
                    else
                        vs.Add(result[0], value);
                }
        }

        protected string[] GetContentFromFile(string format, string fullName, Encoding encoding)
        {
            var readerFactory = new FileReaderFactory();
            var reader = readerFactory.Create(format);
            return reader.ReadFromFile(fullName, encoding);
        }
    }
}

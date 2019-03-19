using PartStats.BLL.Interfacies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PartStats.BLL.Readers
{
    public class TxtFileReader : IFileReader
    {
        public string[] ReadFromFile(string fullName, Encoding encoding) =>
            File.ReadAllLines(fullName, encoding);
    }
}

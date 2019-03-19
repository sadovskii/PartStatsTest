using System;
using System.Collections.Generic;
using System.Text;

namespace PartStats.BLL.Interfacies
{
    public interface IFileReader
    {
        string[] ReadFromFile(string fullName, Encoding encoding);
    }
}

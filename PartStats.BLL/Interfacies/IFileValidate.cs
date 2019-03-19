using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PartStats.BLL.Interfacies
{
    public interface IFileValidate
    {
        bool CheckFileEncoding(FileInfo fileInfo, Encoding encoding);
        bool Check(string str);
    }
}

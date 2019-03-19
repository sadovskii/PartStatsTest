using System;
using System.Collections.Generic;
using System.Text;

namespace PartStats.BLL.Interfacies
{
    public interface IFileReaderFactory
    {
        IFileReader Create(string context);
    }
}

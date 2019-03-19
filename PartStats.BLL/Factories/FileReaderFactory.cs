using PartStats.BLL.Interfacies;
using PartStats.BLL.Readers;
using PartStats.BLL.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace PartStats.BLL.Factories
{
    public class FileReaderFactory : IFileReaderFactory
    {
        private Dictionary<string, IFileReader> readers;

        public FileReaderFactory()
        {
            readers = new Dictionary<string, IFileReader>();
            readers.Add(Constans.DocxFormat, new DocxFileReader());
            readers.Add(Constans.PdfFormat, new PdfFileReader());
            readers.Add(Constans.TxtFormat, new TxtFileReader());
        }

        public IFileReader Create(string context)
        {
            if (readers.ContainsKey(context))
                return readers[context];

            throw new Exception("Reader not found");
        }
    }
}

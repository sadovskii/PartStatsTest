using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PartStats.BLL
{
    public class Worker
    {
        public void Router(string mode, string path)
        {
            if (mode == "filesystem")
            {
                WorkWithDirectory(path);
            }
            else if (mode == "http")
            { }
            else
                throw new Exception();
        }

        public void WorkWithDirectory(string path)
        {
            string[] allfiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            foreach (var a in allfiles)
            {
                var file = new FileInfo(a);
                if (CheckFileEncoding(file, Encoding.UTF8))
                {

                }
            }
        }

        public bool CheckFileEncoding(FileInfo fileInfo, Encoding encoding)
        {
            // Read the BOM
            var bom = new byte[4];
            using (var file = new FileStream(fileInfo.Name, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 4);
            }

            Encoding encoding1;
            // Analyze the BOM
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) encoding1 = Encoding.UTF7;
            else if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) encoding1 = Encoding.UTF8;
            else if (bom[0] == 0xff && bom[1] == 0xfe) encoding1 = Encoding.Unicode; //UTF-16LE
            else if (bom[0] == 0xfe && bom[1] == 0xff) encoding1 = Encoding.BigEndianUnicode; //UTF-16BE
            else if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) encoding1 = Encoding.UTF32;
            else encoding1 = Encoding.ASCII;

            return encoding1 == encoding;
        }

        public bool CheckFileFormat()
        {
            return true;
        }
    }
}

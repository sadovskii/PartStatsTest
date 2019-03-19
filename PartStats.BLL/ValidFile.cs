using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PartStats.BLL
{
    public class ValidFile
    {
        public bool CheckFileEncoding(FileInfo fileInfo, Encoding encoding)
        {
            using (var reader = new StreamReader(fileInfo.FullName, new UnicodeEncoding()))
            {
                reader.Peek(); // you need this!
                return reader.CurrentEncoding == encoding;
            }
        }

        public  bool Check(string str)
        {
            if (str.Count(x => x == ',') == 1)
            {
                var result = str.Split(',').Select(t => t.Trim()).ToArray();

                if (!string.IsNullOrEmpty(result[0]) && int.TryParse(result[1], out int numer))
                    return true;
            }
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace PartStats.BLL
{
    public class Worker
    {
        private const string  downloadedFilesfolder = @"Downloaded\{0}";
        public ValidFile validFile = new ValidFile();

        public void Router(string mode, string path)
        {
            if (mode == "filesystem")
                WorkWithDirectory(path);
            else if (mode == "http")
                WorkWithHttp(path);
            else
                throw new Exception();
        }

        public void WorkWithHttp(string path)
        {
            FileInfo fileInfo = new FileInfo(path);

            if(fileInfo.Exists)
            {
                string[] strUrls = File.ReadAllLines(fileInfo.FullName, Encoding.UTF8);
                Dictionary<string, int> vs = new Dictionary<string, int>();

                for (var i = 1; i <= strUrls.Length; i++)
                {
                    string filePathDownloaded = null;

                    if(Uri.TryCreate(strUrls[i], UriKind.Absolute, out Uri objUrl))
                    {
                        filePathDownloaded = string.Format(downloadedFilesfolder, string.Format("{0}.txt", strUrls[i]));
                        //objUrl.Segments[objUrl.Segments.Length - 1]
                        using (var client = new WebClient())
                        {
                            client.DownloadFile(strUrls[i], filePathDownloaded);
                        }
                    }

                    if (filePathDownloaded != null)
                    {
                        var file = new FileInfo(filePathDownloaded);

                        if (file.Exists && validFile.CheckFileEncoding(file, Encoding.UTF8))
                        {
                            string[] details = File.ReadAllLines(filePathDownloaded, Encoding.UTF8);
                            File.Delete(filePathDownloaded);
                            SelectCorrectRecords(vs, details);
                        }
                    }
                }

                SaveInfoToFile(vs);
            }
        }

        public void WorkWithDirectory(string path)
        {
            string[] allfiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            Dictionary<string, int> vs = new Dictionary<string, int>();

            foreach (var a in allfiles)
            {              
                var file = new FileInfo(a);

                if (file.Exists && validFile.CheckFileEncoding(file, Encoding.UTF8))
                {
                    string[] details = File.ReadAllLines(file.FullName, Encoding.UTF8);

                    SelectCorrectRecords(vs, details);
                }
            }

            SaveInfoToFile(vs);
        }

        private void SaveInfoToFile(Dictionary<string, int> vs)
        {
            if (vs.Count != 0)
            {
                using (TextWriter tw = new StreamWriter("output.txt", false, Encoding.UTF8))
                {
                    foreach (var s in vs)
                        tw.WriteLine(string.Format("{0}, {1}", s.Key, s.Value));
                }
            }
        }

        private void SelectCorrectRecords(Dictionary<string, int> vs, string[] details)
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
    }
}

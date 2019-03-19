using PartStats.BLL.Interfacies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace PartStats.BLL
{
    public class HttpFileManager : FileManagerBase, IFileManager
    {
        private const string downloadedFilesfolder = @"Downloaded\{0}";

        public HttpFileManager(IFileValidate validFile) : base(validFile) { }

        public void Work(string path)
        {
            FileInfo fileInfo = new FileInfo(path);

            if (fileInfo.Exists)
            {
                string[] strUrls = this.GetContentFromFile(fileInfo.Extension, fileInfo.FullName, Encoding.UTF8);
                Dictionary<string, int> vs = new Dictionary<string, int>();

                for (var i = 1; i <= strUrls.Length; i++)
                {
                    string filePathDownloaded = null;

                    if (Uri.TryCreate(strUrls[i], UriKind.Absolute, out Uri objUrl))
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
                            string[] details = this.GetContentFromFile(file.Extension, filePathDownloaded, Encoding.UTF8);
                            File.Delete(filePathDownloaded);
                            this.SelectCorrectRecords(vs, details);
                        }
                    }
                }   

                this.SaveInfoToFile(vs);
            }
        }
    }
}

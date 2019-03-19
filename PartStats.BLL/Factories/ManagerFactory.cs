using PartStats.BLL.Interfacies;
using PartStats.BLL.Utils;
using System;
using System.Collections.Generic;

namespace PartStats.BLL.Factories
{
    public class ManagerFactory : IManagerFactory
    {
        private Dictionary<string, IFileManager> managers;

        public ManagerFactory()
        {
            managers = new Dictionary<string, IFileManager>();
            managers.Add(Constans.FileSystem, new FileSystemManager(new FileValidate()));
            managers.Add(Constans.Http, new HttpFileManager(new FileValidate()));
        }

        public IFileManager Create(string context)
        {
            if (managers.ContainsKey(context))
                return managers[context];

            throw new Exception("File manager not found");
        }
    }
}

namespace PartStats.BLL.Interfacies
{
    public interface IManagerFactory
    {
        IFileManager Create(string context);
    }
}

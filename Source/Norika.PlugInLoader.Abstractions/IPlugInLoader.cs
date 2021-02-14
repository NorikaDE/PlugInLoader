namespace Norika.PlugInLoader.Abstractions
{
    public interface IPlugInLoader
    {
        IPlugInLoadRequest FromDirectory(string directory);
    }
}

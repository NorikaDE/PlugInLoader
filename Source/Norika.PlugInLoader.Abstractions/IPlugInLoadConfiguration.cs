namespace Norika.PlugInLoader.Abstractions
{
    public interface IPlugInLoadConfiguration
    {
        IPlugInLoadConfiguration MatchesName(string pattern);
        
        IPlugInLoadConfiguration IsSigned(string publicKey);
    }
}

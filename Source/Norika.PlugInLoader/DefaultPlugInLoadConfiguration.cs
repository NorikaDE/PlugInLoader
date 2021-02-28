using System.Diagnostics.CodeAnalysis;
using Norika.PlugInLoader.Abstractions;

namespace Norika.PlugInLoader
{
    [ExcludeFromCodeCoverage]
    internal class DefaultPlugInLoadConfiguration : IPlugInLoadConfiguration
    {
        internal string NamePattern { get; private set; } = "*";
        
        internal string PublicKey { get; private set; }
        
        public IPlugInLoadConfiguration MatchesName(string pattern)
        {
            NamePattern = pattern;
            return this;
        }

        public IPlugInLoadConfiguration IsSigned(string publicKey)
        {
            PublicKey = publicKey;
            return this;
        }
    }
}

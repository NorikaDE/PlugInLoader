using System.Text;
using Norika.PlugInLoader.Abstractions;

namespace Norika.PlugInLoader.Matcher
{
    internal class DefaultStrongNamePlugInMatcher : IPlugInMatcher
    {
        private readonly string _publicKey;

        public DefaultStrongNamePlugInMatcher(string publicKey)
        {
            _publicKey = publicKey;
        }

        public bool VerifyMatches(IAssemblyMetadata assembly)
        {
            byte[] assemblyPublicKey = assembly.GetPublicKey();
            if (assemblyPublicKey == null)
            {
                return false;
            }
            return assemblyPublicKey == Encoding.Unicode.GetBytes(_publicKey);
        }
    }
}

using System;
using System.Text.RegularExpressions;
using Norika.PlugInLoader.Abstractions;

namespace Norika.PlugInLoader.Matcher
{
    internal class DefaultAssemblyNamePlugInMatcher : IPlugInMatcher
    {
        private readonly Regex _regExMatch;

        internal DefaultAssemblyNamePlugInMatcher(string namePattern)
        {
            _regExMatch = new Regex("(\\\\|\\/){1}" + namePattern.Replace(".", "\\.").Replace("*", ".*"));
        }
        
        public bool VerifyMatches(IAssemblyMetadata assembly)
        {
            if (assembly?.Location == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            
            string assemblyName = assembly.Location;
            return assemblyName != null && _regExMatch.IsMatch(assemblyName);
        }
    }
}

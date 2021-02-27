using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Norika.PlugInLoader.Abstractions;
using Norika.PlugInLoader.Factories;
using Norika.PlugInLoader.Interfaces;
using Norika.PlugInLoader.Matcher;

namespace Norika.PlugInLoader
{
    public class DefaultPlugInLoadRequest : IPlugInLoadRequest
    {
        private readonly IFileSystem _fileSystem;
        private readonly ILoadContextFactory _loadContextFactory;
        private readonly IPlugInFactory _plugInFactory;
        private readonly IEnumerable<IFileInfo> _assemblyFileInfoCollection;
        private readonly ICollection<IPlugInMatcher> _matcher;
        private readonly DefaultPlugInLoadConfiguration _loadConfiguration;

        public DefaultPlugInLoadRequest(IFileSystem fileSystem, IEnumerable<IFileInfo> assemblies)
        {
            _fileSystem = fileSystem;
            _assemblyFileInfoCollection = assemblies;
            _matcher = new List<IPlugInMatcher>();
            _loadConfiguration = new DefaultPlugInLoadConfiguration();
            _loadContextFactory = new DefaultLoadContextFactory(fileSystem);
            _plugInFactory = new DefaultPlugInFactory();
        }

        public IPlugInLoadRequest Where(Action<IPlugInLoadConfiguration> configuration)
        {
            configuration.Invoke(_loadConfiguration);
            return this;
        }

        public IPlugInLoadRequest UseCustomAssemblyMatcher(IPlugInMatcher matcher)
        {
            _matcher.Add(matcher);
            return this;
        }

        public IPlugInCollection LookUp()
        {
            CreateDefaultMatchers();
            using var loadContext = CreateResolver(BuildUpPaths(_assemblyFileInfoCollection.ToList()));
            
            IList<IPlugIn> plugIns = (from assemblyFile 
                in _assemblyFileInfoCollection 
                select loadContext.LoadFromFileInfo(assemblyFile) 
                into assembly 
                where AllMatcherMatchesAssembly(assembly) 
                select _plugInFactory.CreatePlugIn(assembly)).ToList();
            
            return new DefaultPlugInCollection(plugIns);
        }

        private bool AllMatcherMatchesAssembly(IAssemblyMetadata assembly)
        {
            return _matcher.All(m => m.VerifyMatches(assembly));
        }

        private void CreateDefaultMatchers()
        {
            _matcher.Add(new DefaultAssemblyAttributePlugInMatcher<PlugInAssemblyAttribute>());
            if (!string.IsNullOrWhiteSpace(_loadConfiguration.PublicKey))
            {
                _matcher.Add(new DefaultStrongNamePlugInMatcher(_loadConfiguration.PublicKey));
            }
            if (!string.IsNullOrWhiteSpace(_loadConfiguration.NamePattern))
            {
                _matcher.Add(new DefaultAssemblyNamePlugInMatcher(_loadConfiguration.NamePattern));
            }
        }
        
        private  ILoadContext CreateResolver(IList<string> paths)
        {
            return _loadContextFactory.CreateLoadContext(paths);
        }

        private List<string> BuildUpPaths(ICollection<IFileInfo> matchingAssemblies)
        {
            var paths = matchingAssemblies.Select(fi => fi.FullName).ToList();
            paths.Add(typeof(PlugInAssemblyAttribute).Assembly.Location);
            return paths;
        }
    }
}

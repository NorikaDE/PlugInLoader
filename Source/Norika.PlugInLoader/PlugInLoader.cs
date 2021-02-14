using System.Collections.ObjectModel;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using Norika.PlugInLoader.Abstractions;

namespace Norika.PlugInLoader
{
    public class PlugInLoader : IPlugInLoader
    {
        private readonly IFileSystem _fileSystem;

        internal PlugInLoader(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
        
        public PlugInLoader() : this(new FileSystem()) { }
        
        public IPlugInLoadRequest FromDirectory(string directory)
        {
            if (!_fileSystem.Directory.Exists(directory))
            {
                throw new DirectoryNotFoundException(directory);
            }

            var assemblies = _fileSystem.Directory
                .GetFiles(directory, "*.dll")
                .Select(f => _fileSystem.FileInfo.FromFileName(f));
            return new DefaultPlugInLoadRequest(_fileSystem ,assemblies);
        }
    }
}

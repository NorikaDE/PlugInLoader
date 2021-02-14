using System;
using System.IO.Abstractions;
using Norika.PlugInLoader.Abstractions;

namespace Norika.PlugInLoader.Interfaces
{
    internal interface ILoadContext : IDisposable
    {
        IAssemblyMetadata LoadFromAssemblyPath(string path);

        IAssemblyMetadata LoadFromFileInfo(IFileInfo fileInfo);

    }
}

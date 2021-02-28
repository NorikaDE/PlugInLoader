using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Abstractions;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Norika.PlugInLoader.Factories;

namespace Norika.PlugInLoader.UnitTests
{
    [TestClass]
    [TestCategory("CheckIn")]
    public partial class DefaultLoadContextFactoryUnitTests
    {
        [TestMethod]
        public void CreateLoadContext_WithEmptyAssemblyList_ShouldCreateLoadContextWithRuntimeAssemblies()
        {
            string[] runtimeAssemblies = new[]
            {
                "RuntimeAssembly1.dll",
                "RuntimeAssembly2.dll"
            };
            WithRuntimeAssemblies(runtimeAssemblies);

            factory.CreateLoadContext(new List<string>());
            
            ShouldCreateLoadContextWithAssemblies(runtimeAssemblies);
        }
        
        [TestMethod]
        public void CreateLoadContext_WithGivenAssembly_ShouldCreateLoadContextWithRuntimeAssembliesAndGivenAssembly()
        {
            string[] runtimeAssemblies = new[]
            {
                "RuntimeAssembly1.dll",
                "RuntimeAssembly2.dll"
            };
            WithRuntimeAssemblies(runtimeAssemblies);

            factory.CreateLoadContext(new List<string> {  "MyAssembly.dll" } );
            
            ShouldCreateLoadContextWithAssemblies(runtimeAssemblies);
            ShouldCreateLoadContextWithAssemblies("MyAssembly.dll");
        }

        private void ShouldCreateLoadContextWithAssemblies(params string[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                metadataLoadContextFactory.Verify(l
                    => l.CreateMetadataLoadContext(It.Is<IList<string>>(sl
                        => sl.Contains(assembly))));
            }
        }
    }

    public partial class DefaultLoadContextFactoryUnitTests
    {
        private ILoadContextFactory factory;
        private Mock<IDirectory> directoryMock;
        private Mock<IFileSystem> fileSystemMock;
        private Mock<MetadataLoadContextFactory> metadataLoadContextFactory;

        [TestInitialize]
        public void InitializeTest()
        {
            directoryMock = new Mock<IDirectory>();
            fileSystemMock = new Mock<IFileSystem>();
            fileSystemMock.Setup(f => f.Directory).Returns(directoryMock.Object);
            metadataLoadContextFactory = new Mock<MetadataLoadContextFactory>();
            metadataLoadContextFactory.Setup(lc => lc.CreateMetadataLoadContext(It.IsAny<IList<string>>()))
                .Returns((MetadataLoadContext)null);
            
            factory = new DefaultLoadContextFactory(fileSystemMock.Object, metadataLoadContextFactory.Object);
        }

        private void WithRuntimeAssemblies(params string[] runtimeAssemblies)
        {
            directoryMock.Setup(d => d.GetFiles(It.IsAny<string>(), "*.dll"))
                .Returns(runtimeAssemblies);
        }
    }
}
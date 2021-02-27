using System.IO;
using System.IO.Abstractions;
using System.Reflection;
using System.Runtime.InteropServices;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Norika.PlugInLoader.UnitTests
{
    [TestClass]
    public class DefaultLoadContextUnitTests
    {
        [TestMethod]
        public void LoadFromAssemblyPath_WithExistentAssembly_ShouldReturnTypeOfAssemblyAbstraction()
        {
            _loadContext.LoadFromAssemblyPath("./Norika.PlugInLoader.Samples.dll")
                .Should().BeOfType<AssemblyAbstraction>();
        }
        
        [TestMethod]
        public void LoadFromAssemblyPath_WithExistentAssembly_ShouldCorrectRepresentationOfRequestedAssembly()
        {
            _loadContext.LoadFromAssemblyPath("./Norika.PlugInLoader.Samples.dll")
                .Location.Should().EndWith("PlugInLoader.Samples.dll");
        }
        
        [TestMethod]

        public void LoadFromFileInfo_WithExistentAssembly_ReturnTypeOfAssemblyAbstraction()
        {
            IFileInfo fileInfo = Mock.Of<IFileInfo>(file => file.FullName == "./Norika.PlugInLoader.Samples.dll");
            _loadContext.LoadFromFileInfo(fileInfo)
                .Should().BeOfType<AssemblyAbstraction>();
        }
        
        [TestMethod]
        public void LoadFromAssemblyPath_WithExistentAssembly_ShouldNotReturnNullValue()
        {
            _loadContext.LoadFromAssemblyPath("./Norika.PlugInLoader.Samples.dll")
                .Should().NotBeNull();
        }
        
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void LoadFromAssemblyPath_WithNotExistentAssembly_ShouldThrowFileNotFoundException()
        {
            _loadContext.LoadFromAssemblyPath("./NotExistent.dll");
        }
        
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void LoadFromFileInfo_WithNotExistentAssembly_ShouldThrowFileNotFoundException()
        {
            IFileInfo fileInfo = Mock.Of<IFileInfo>(file => file.FullName == "./NotExistent.dll");
            _loadContext.LoadFromFileInfo(fileInfo);
        }

        [TestInitialize]
        public void InitializeTest()
        {
            _loadContext = new DefaultLoadContext(
                new MetadataLoadContext(
                    new PathAssemblyResolver(
                        Directory.GetFiles(RuntimeEnvironment.GetRuntimeDirectory(), "*.dll")
                        )));
        }

        private DefaultLoadContext _loadContext;
    }
}
using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Norika.PlugInLoader.Abstractions;
using Norika.PlugInLoader.Internals;
using Norika.PlugInLoader.UnitTests.Matcher;

namespace Norika.PlugInLoader.UnitTests
{
    [TestClass]
    [TestCategory("CheckIn")]
    public class DefaultPlugInFactoryUnitTests
    {
        [TestMethod]
        public void dev()
        {
            Mock<IActivator> activatorMock = new Mock<IActivator>();
            Mock<IAssemblyMetadata> assemblyMetadataMock = new Mock<IAssemblyMetadata>();

            activatorMock.Setup(a => a.CreateInstance<PlugInAssemblyAttribute>(
                    It.IsAny<object[]>()))
                .Returns(new PlugInAssemblyAttribute("PlugInName"));
            
            assemblyMetadataMock.Setup(a => a.GetCustomAttributeData())
                .Returns(new List<CustomAttributeData>
                {
                    new CustomAttributeDataStub(typeof(PlugInAssemblyAttribute))
                        .WithConstructorArgumentType(typeof(string), "PlugInName")
                });
            
            DefaultPlugInFactory factory = new DefaultPlugInFactory(activatorMock.Object);

            factory.CreatePlugIn(assemblyMetadataMock.Object);
        }
    }
}
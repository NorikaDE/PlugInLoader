using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.AutoMock;
using Norika.PlugInLoader.Abstractions;

namespace Norika.PlugInLoader.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            IPlugInLoader loader = new PlugInLoader();

            var plugIns = loader.FromDirectory(".")
                .Where(c 
                    => c.MatchesName("Norika.*Samples.dll"))
                .LookUp();

            foreach (var demoPlugIn in plugIns.LoadAll<IDemoPlugIn>())
            {
                demoPlugIn.Bla();
            }
        }

        [TestMethod]
        public void dev()
        {
            AutoMocker autoMocker = new AutoMocker();
            
            var sut = autoMocker.CreateInstance<DefaultPlugInFactory>();

            Mock<IAssemblyMetadata> assemblyMetadata = new Mock<IAssemblyMetadata>();
            
            IPlugIn plugIn = sut.CreatePlugIn(assemblyMetadata.Object);
            
            Assert.IsNotNull(plugIn);
        }
    }
}

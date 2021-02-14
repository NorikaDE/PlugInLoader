using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    }
}

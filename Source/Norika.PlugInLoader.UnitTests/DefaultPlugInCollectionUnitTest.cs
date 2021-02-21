using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Norika.PlugInLoader.Abstractions;

namespace Norika.PlugInLoader.UnitTests
{
    [TestClass]
    [TestCategory("CheckIn")]
    public class DefaultPlugInCollectionUnitTest
    {
        [TestMethod]
        public void LoadAll_OnCollectionWithFivePlugIns_ShouldCallLoadToAllPlugIns()
        {
            var collection = WithPlugIns(5);
            collection.LoadAll();
            collection.ForEach(p => Mock.Get(p).Verify(pm => pm.Load()));
        }
        
        [TestMethod]
        public void LoadAllGeneric_OnCollectionWithFivePlugIns_ShouldCallLoadMultipleGenericToAllPlugIns()
        {
            var collection = WithPlugIns(5);
            collection.LoadAll<IPlugIn>();
            collection.ForEach(p => Mock.Get(p).Verify(pm => pm.LoadMultiple<IPlugIn>()));
        }

        private static IPlugInCollection WithPlugIns(int count)
        {
            IList<IPlugIn> plugIns = new List<IPlugIn>();
            for (int i = 0; i < count; i++)
            {
                Mock<IPlugIn> plugInMock = new Mock<IPlugIn>();
                plugInMock.Setup(p => p.LoadMultiple<IPlugIn>()).Returns(new List<IPlugIn>());
                plugIns.Add(plugInMock.Object);
            }
            IPlugInCollection collection = new DefaultPlugInCollection(plugIns);
            return collection;
        }
    }
}
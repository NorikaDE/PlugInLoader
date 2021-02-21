using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Norika.PlugInLoader.UnitTests
{
    [TestClass]
    [TestCategory("CheckIn")]
    public class AssemblyAbstractionUnitTests
    {
        [TestMethod]
        public void Location_WithCurrentAssembly_ShouldBeSame()
        {
            _abstraction.Location.Should().BeEquivalentTo(Assembly.GetExecutingAssembly().Location);
        }
        
        [TestMethod]
        public void FullName_WithCurrentAssembly_ShouldBeSame()
        {
            _abstraction.FullName.Should().BeEquivalentTo(Assembly.GetExecutingAssembly().FullName);
        }

        [TestMethod]
        public void GetPublicKey_FromCurrentAssemblyWithoutPublicKey_ShouldBeEmpty()
        {
            _abstraction.GetPublicKey().Should().BeEmpty();
        }
        
        private AssemblyAbstraction _abstraction;
        
        [TestInitialize]
        public void SetUpTest()
        {
            _abstraction = new AssemblyAbstraction(Assembly.GetExecutingAssembly());
        }
    }
}
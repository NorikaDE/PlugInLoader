using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Norika.PlugInLoader.Abstractions;
using Norika.PlugInLoader.Matcher;

namespace Norika.PlugInLoader.UnitTests.Matcher
{
    [TestClass]
    [TestCategory("CheckIn")]
    public class DefaultAssemblyNamePlugInMatcherUnitTests
    {

        [TestMethod]
        public void VerifyMatches_WithAssemblyLocationMatchesFullFileNamePattern_ShouldReturnTrue()
        {
            WithMatcherMatchingPattern("Namespace.PlugIn.*.dll");
            
            matcher.VerifyMatches(
                ForAssemblyWithLocation("D:/Folder/Namespace.PlugIn.PlugInA.dll"))
                .Should().BeTrue();
        }
        
        [TestMethod]
        public void VerifyMatches_WithAssemblyLocationMatchesFileNamePatternPartially_ShouldReturnFalse()
        {
            WithMatcherMatchingPattern("Namespace.PlugIn.*.dll");
            
            matcher.VerifyMatches(
                    ForAssemblyWithLocation("D:/Folder/OtherNamespace.PlugIn.PlugInA.dll"))
                .Should().BeFalse();
        }
        
        [TestMethod]
        public void VerifyMatches_WithAssemblyLocationDoesNotMatchFileNamePattern_ShouldReturnFalse()
        {
            WithMatcherMatchingPattern("Namespace.PlugIn.*.dll");
            
            matcher.VerifyMatches(
                    ForAssemblyWithLocation("D:/Folder/Class.PlugIn.PlugInA.dll"))
                .Should().BeFalse();
        }
        
        [TestMethod]
        public void VerifyMatches_WithNullInputValue_ShouldThrowArgumentNullException()
        {
            WithMatcherMatchingPattern("Namespace.PlugIn.*.dll");

            matcher.Invoking(m => m.VerifyMatches(null))
                .Should()
                .Throw<ArgumentNullException>();
        }

        private IAssemblyMetadata ForAssemblyWithLocation(string location)
        {
            Mock<IAssemblyMetadata> assemblyMetadata = new Mock<IAssemblyMetadata>();
            assemblyMetadata.Setup(a => a.Location).Returns(location);
            return assemblyMetadata.Object;
        }

        private void WithMatcherMatchingPattern(string pattern)
        {
            matcher = new DefaultAssemblyNamePlugInMatcher(pattern);
        }
        
        private DefaultAssemblyNamePlugInMatcher matcher;
    }
}
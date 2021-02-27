using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Norika.PlugInLoader.Abstractions;
using Norika.PlugInLoader.Matcher;

namespace Norika.PlugInLoader.UnitTests.Matcher
{
    [TestClass]
    [TestCategory("CheckIn")]
    public partial class DefaultStrongNamePlugInMatcherUnitTest
    {

        [TestMethod]
        public void VerifyMatches_WithGivenKeyMatchesPublicKeyOfAssembly_ShouldReturnTrue()
        {
            const string publicKey = "abcdefg";
            
            WithMatcherMatchingStrongName(publicKey);
            IAssemblyMetadata assembly = ForAssemblyWithStrongName(Encoding.Unicode.GetBytes(publicKey));

            matcher.VerifyMatches(assembly).Should().BeTrue();
        }
        
        [TestMethod]
        public void VerifyMatches_WithAssemblyWithoutPublicKey_ShouldReturnFalse()
        {
            const string publicKey = "abcdefg";
            
            WithMatcherMatchingStrongName(null);
            IAssemblyMetadata assembly = ForAssemblyWithStrongName(Encoding.Unicode.GetBytes(publicKey));

            matcher.VerifyMatches(assembly).Should().BeFalse();
        }
        
        [TestMethod]
        public void VerifyMatches_WithGivenKeyDoesNotMatchAssemblyPublicKey_ShouldReturnFalse()
        {
            WithMatcherMatchingStrongName("gfedcba");
            IAssemblyMetadata assembly = ForAssemblyWithStrongName(
                Encoding.Unicode.GetBytes("abcdefg"));
            
            matcher.VerifyMatches(assembly).Should().BeFalse();
        }

    }

    public partial class DefaultStrongNamePlugInMatcherUnitTest
    {
        private IAssemblyMetadata ForAssemblyWithStrongName(byte[] publicKey)
        {
            Mock<IAssemblyMetadata> assemblyMetadata = new Mock<IAssemblyMetadata>();
            assemblyMetadata.Setup(a => a.GetPublicKey()).Returns(publicKey);
            return assemblyMetadata.Object;
        }
        
        private void WithMatcherMatchingStrongName(string strongName)
        {
            matcher = new DefaultStrongNamePlugInMatcher(strongName);
        }
        
        private DefaultStrongNamePlugInMatcher matcher;
        
    }
}
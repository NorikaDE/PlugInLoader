using System;
using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Norika.PlugInLoader.Abstractions;
using Norika.PlugInLoader.Matcher;

namespace Norika.PlugInLoader.UnitTests.Matcher
{
    [TestClass]
    [TestCategory("CheckIn")]
    public partial class DefaultAssemblyAttributePlugInMatcherUnitTest
    {

        [TestMethod]
        public void VerifyMatches_WithGivenAssemblyContainsOnlyTargetAttributeData_ShouldReturnTrue()
        {
            IAssemblyMetadata assembly = ForAssemblyWithAttributes(
                new CustomAttributeDataStub(typeof(MyTypeAttribute)));

            matcher.VerifyMatches(assembly).Should().BeTrue();
        }
        
        [TestMethod]
        public void VerifyMatches_WithGivenAssemblyContainsTargetAttributeData_ShouldReturnTrue()
        {
            IAssemblyMetadata assembly = ForAssemblyWithAttributes(
                new CustomAttributeDataStub(typeof(MyTypeAttribute)),
                new CustomAttributeDataStub(typeof(TestCategoryAttribute)));

            matcher.VerifyMatches(assembly).Should().BeTrue();
        }
        
        [TestMethod]
        public void VerifyMatches_WithGivenAssemblyDoesNotContainTargetAttributeData_ShouldReturnFalse()
        {
            IAssemblyMetadata assembly = ForAssemblyWithAttributes(
                new CustomAttributeDataStub(typeof(TestCategoryAttribute)),
                new CustomAttributeDataStub(typeof(TestCategoryAttribute)));

            matcher.VerifyMatches(assembly).Should().BeFalse();
        }
        
        [TestMethod]
        public void VerifyMatches_WithGivenAssemblyReturnsNoAttributeData_ShouldReturnFalse()
        {
            IAssemblyMetadata assembly = ForAssemblyWithAttributes();
            matcher.VerifyMatches(assembly).Should().BeFalse();
        }
    }

    public partial class DefaultAssemblyAttributePlugInMatcherUnitTest
    {
        private IAssemblyMetadata ForAssemblyWithAttributes(params CustomAttributeData[] attributeData)
        {
            Mock<IAssemblyMetadata> assemblyMetadata = new Mock<IAssemblyMetadata>();
            assemblyMetadata.Setup(a => a.GetCustomAttributeData()).Returns(attributeData);
            return assemblyMetadata.Object;
        }

        [TestInitialize]
        public void InitializeTest()
        {
            matcher = new DefaultAssemblyAttributePlugInMatcher<MyTypeAttribute>();
        }
        
        private DefaultAssemblyAttributePlugInMatcher<MyTypeAttribute> matcher;
    }
    
    internal class MyTypeAttribute : Attribute { }
    internal class CustomAttributeDataStub : CustomAttributeData
    {
        internal CustomAttributeDataStub(Type attributeType)
        {
            AttributeType = attributeType;
            ConstructorArguments = new List<CustomAttributeTypedArgument>();
        }

        internal CustomAttributeData WithConstructorArgumentType(Type argumentType, object value)
        {
            ConstructorArguments.Add(new CustomAttributeTypedArgument(argumentType, value));
            return this;
        }
            
        public override Type AttributeType { get; }

        public override IList<CustomAttributeTypedArgument> ConstructorArguments { get; }
    }
}
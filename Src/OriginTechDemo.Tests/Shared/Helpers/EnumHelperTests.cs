using Microsoft.VisualStudio.TestTools.UnitTesting;
using OriginTechDemo.Application.ViewModels;
using OriginTechDemo.Domain.Validators;
using OriginTechDemo.Shared.Helpers;
using System;

namespace OriginTechDemo.Tests.Application.Validators
{
    public enum ETest
    {
        None,
        Option1,
        Option2
    }

    [TestClass]
    public class EnumHelperTests
    {
        [TestMethod]
        public void ShouldReturnEnumNames()
        {
            var result = EnumHelper.GetNames(typeof(ETest), toLower: false, ignoreNone: false);

            Assert.IsTrue(result.Count == 3);
            Assert.IsTrue(result.Contains("None"));
            Assert.IsTrue(result.Contains("Option1"));
            Assert.IsTrue(result.Contains("Option2"));
        }

        [TestMethod]
        public void ShouldReturnEnumNamesToLower()
        {
            var result = EnumHelper.GetNames(typeof(ETest), toLower: true, ignoreNone: false);

            Assert.IsTrue(result.Count == 3);
            Assert.IsTrue(result.Contains("none"));
            Assert.IsTrue(result.Contains("option1"));
            Assert.IsTrue(result.Contains("option2"));
        }

        [TestMethod]
        public void ShouldReturnEnumIgnoringNone()
        {
            var result = EnumHelper.GetNames(typeof(ETest), toLower: false, ignoreNone: true);

            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.Contains("Option1"));
            Assert.IsTrue(result.Contains("Option2"));
        }

        [TestMethod]
        public void ShouldReturnEnumToLowerAndIgnoringNone()
        {
            var result = EnumHelper.GetNames(typeof(ETest), toLower: true, ignoreNone: true);

            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.Contains("option1"));
            Assert.IsTrue(result.Contains("option2"));
        }

        [TestMethod]
        public void ShouldReturnStringfiedEnumNames()
        {
            var result = EnumHelper.StringfyNames(typeof(ETest), toLower: false, ignoreNone: false);

            Assert.IsTrue(result.Contains("None"));
            Assert.IsTrue(result.Contains("Option1"));
            Assert.IsTrue(result.Contains("Option2"));
        }

        [TestMethod]
        public void ShouldReturnStringfiedEnumNamesToLower()
        {
            var result = EnumHelper.StringfyNames(typeof(ETest), toLower: true, ignoreNone: false);

            Assert.IsTrue(result.Contains("none"));
            Assert.IsTrue(result.Contains("option1"));
            Assert.IsTrue(result.Contains("option2"));
        }

        [TestMethod]
        public void ShouldReturnStringfiedEnumNamesIgnoringNone()
        {
            var result = EnumHelper.StringfyNames(typeof(ETest), toLower: false, ignoreNone: true);

            Assert.IsTrue(result.Contains("Option1"));
            Assert.IsTrue(result.Contains("Option2"));
        }

        [TestMethod]
        public void ShouldReturnStringfiedEnumNamesToLowerAndIgnoringNone()
        {
            var result = EnumHelper.StringfyNames(typeof(ETest), toLower: true, ignoreNone: true);

            Assert.IsTrue(result.Contains("option1"));
            Assert.IsTrue(result.Contains("option2"));
        }
    }
}

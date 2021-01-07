using System;
using System.Linq;
using FluentAssertions;
using WorkWearGroup.API.Models;
using WorkWearGroup.API.Validation;
using Xunit;

namespace WorkWearGroup.API.UnitTests
{
    public class ItemValidatorTests
    {
        private readonly ItemValidator _systemUnderTest;

        public ItemValidatorTests()
        {
            _systemUnderTest = new ItemValidator();
        }

        [Theory]
        [InlineData("$ABC123")]
        [InlineData("ABC123!")]
        [InlineData("ABC123@")]
        [InlineData("AB@C123_")]
        [InlineData("ABC123#")]
        [InlineData("%ABC123~")]
        public void Validate_WhenKeyIsInvalid_ShouldError(string key)
        {
            var validationResult = _systemUnderTest.Validate(new Item {Key = key, Value = "valid value"});
            validationResult.IsValid.Should().BeFalse();
        }

        [Theory]
        [InlineData("ABC123")]
        [InlineData("ABC123.")]
        [InlineData("ABC123-")]
        [InlineData("ABC123_")]
        [InlineData("ABC123~")]
        [InlineData("~.ABC123~")]
        [InlineData("..ABC123~")]
        [InlineData("-ABC123~")]
        [InlineData("_ABC123~")]
        [InlineData("_ABC123~")]
        public void Validate_WhenKeyIsValid_ShouldSucceed(string key)
        {
            var validationResult = _systemUnderTest.Validate(new Item { Key = key, Value = "valid value" });
            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_WhenKeyIsMoreThan32Characters_ShouldError()
        {
            var key = string.Concat(Enumerable.Repeat("a", 33));
            var validationResult = _systemUnderTest.Validate(new Item { Key = key, Value = "valid value" });

            validationResult.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_WhenValueIsMoreThan32Characters_ShouldError()
        {
            var value = string.Concat(Enumerable.Repeat("a", 1025));
            var validationResult = _systemUnderTest.Validate(new Item { Key = "abc", Value = value });

            validationResult.IsValid.Should().BeFalse();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Validate_WhenValueIsInvalid_ShouldError(string value)
        {
            var validationResult = _systemUnderTest.Validate(new Item { Key = "valid key", Value = value });
            validationResult.IsValid.Should().BeFalse();
        }
    }
}

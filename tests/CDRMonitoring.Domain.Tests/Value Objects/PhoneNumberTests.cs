using System;
using CDRMonitorig.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CDRMonitoring.Domain.Tests.Value_Objects
{
    public class PhoneNumberTests
    {
        [Theory]
        [InlineData("+447475838301")]
        [InlineData("+79610768792")]
        [InlineData("+123123123")]
        public void From_ValidString_PhoneNumberIsCreated(string stringValue)
        {
            // Act
            PhoneNumber number = PhoneNumber.From(stringValue);

            // Assert
            number.Should().NotBeNull();
            number.Number.Should().Be(stringValue);
        }

        [Theory]
        [InlineData("447475838301")]
        [InlineData("test")]
        [InlineData("")]
        public void From_InvalidString_ExceptionIsThrown(string stringValue)
        {
            // Act
            var act = new Action(() => PhoneNumber.From(stringValue));

            // Assert
            act.Should().Throw<InvalidOperationException>();
        }
    }
}

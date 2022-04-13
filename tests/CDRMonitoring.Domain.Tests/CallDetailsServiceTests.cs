using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CDRMonitorig.Domain;
using CDRMonitorig.Domain.Entities;
using CDRMonitorig.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using Xunit;

namespace CDRMonitoring.Domain.Tests
{
    public class CallDetailsServiceTests
    {
        private readonly IEnumerable<Call> _calls = new[]
        {
            new Call
            {
                Id = Guid.NewGuid().ToString(),
                Dialed = PhoneNumber.From("+447475838301"),
                Caller = PhoneNumber.From("+449475838302"),
                Duration = TimeSpan.FromMinutes(1),
                SalesPrice = 100
            },
            new Call
            {
                Id = Guid.NewGuid().ToString(),
                Dialed = PhoneNumber.From("+447475838301"),
                Caller = PhoneNumber.From("+447475838308"),
                Duration = TimeSpan.FromMinutes(1),
                SalesPrice = 50
            },
            new Call
            {
                Id = Guid.NewGuid().ToString(),
                Dialed = PhoneNumber.From("+467475838302"),
                Caller = PhoneNumber.From("+447475838302"),
                Duration = TimeSpan.FromMinutes(1),
                SalesPrice = 50
            },
            new Call
            {
                Id = Guid.NewGuid().ToString(),
                Dialed = PhoneNumber.From("+445475838302"),
                Caller = PhoneNumber.From("+447475838302"),
                Duration = TimeSpan.FromMinutes(1),
                SalesPrice = 100
            }
        };

        private readonly Mock<ICallDetailsRepository> _repository = new Mock<ICallDetailsRepository>();

        public CallDetailsServiceTests()
        {
            _repository.Setup(r => r.GetAll())
                .ReturnsAsync(_calls);
        }

        [Fact]
        public async Task GetTotalInformation_ReturnsCorrectValue()
        {
            // Arrange
            CallDetailsService service = new CallDetailsService(_repository.Object);

            // Act
            var result = await service.GetTotalInformation();

            // Assert
            result.Should().NotBeNull();
            result.Cost.Should().Be(300);
            result.Count.Should().Be(4);
            result.Duration.Should().Be(TimeSpan.FromMinutes(4));
        }

        // TODO: tbd tests for rules
    }
}

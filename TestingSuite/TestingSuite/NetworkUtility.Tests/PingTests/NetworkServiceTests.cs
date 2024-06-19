using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Extensions;
using NetworkUtility.DNS;
using NetworkUtility.Ping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace NetworkUtility.Tests.PingTests
{
    public class NetworkServiceTests
    {
        private readonly NetworkService _pingService;
        private readonly IDNS _dNS;
        public NetworkServiceTests()
        {
            //Dependencies
            _dNS = A.Fake<IDNS>();
            
            //SUT
            _pingService = new NetworkService(_dNS);
        }
        [Fact]
        public void NetworkService_SendPing_ReturnString()
        {
            //arrange
            A.CallTo(() => _dNS.SendDNS()).Returns(true);

            //act
            var result = _pingService.SendPing();

            //assert
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Be("Success: Ping Sent!");
            result.Should().Contain("Succ", Exactly.Once());
        }

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(2, 2, 4)]
        public void NetworkService_PingTimeout_ReturnInt(int a, int b, int expected)
        {
            //arrange

            //act
            var result = _pingService.PingTimeout(a, b);

            //assert
            result.Should().Be(expected);
            result.Should().BeGreaterThanOrEqualTo(2);
            result.Should().NotBeInRange(-11, 0);
        }

        [Fact]
        public void NetworkService_LastPingDate_ReturnDate()
        {
            //arrange

            //act
            var result = _pingService.LastPingDate();

            //assert
            result.Should().BeAfter(1.January(2010));
            result.Should().BeBefore(1.January(2030));

        }

        [Fact]
        public void NetworkService_GetPingOptions_ReturnsObject()
        {
            //arrange
            var expected = new PingOptions()
            {
                DontFragment = true,    
                Ttl = 1
            };
            //act
            var result = _pingService.GetPingOptions();

            //assert
            result.Should().BeOfType<PingOptions>();
            result.Should().BeEquivalentTo(expected);
            result.Ttl.Should().Be(1);
        }

        [Fact]
        public void NetworkService_GetPingOptions_ReturnsList()
        {
            //arrange
            var expected = new PingOptions()
            {
                DontFragment = true,
                Ttl = 1
            };
            //act
            var result = _pingService.MostRecentPings();

            //assert            
            result.Should().ContainEquivalentOf(expected);
            result.Should().Contain(x => x.DontFragment == true);
        }

    }
}

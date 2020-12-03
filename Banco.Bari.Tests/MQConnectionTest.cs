using Banco.Bari.ConsoleApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Banco.Bari.Tests
{
    public class MQConnectionTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Constructor_ShouldThrowArgumentNull_WhenHostIsNotInformed(string host)
        {
            Assert.Throws<ArgumentNullException>(() => { var connection = new MQConnection(host); });
        }

        [Fact]
        public void Constructor_ShouldSetProperty_WhenHostIsInformed()
        {
            var connection = new MQConnection("host=localhost"); 
        }
    }
}

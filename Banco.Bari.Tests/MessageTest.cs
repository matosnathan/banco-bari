using Banco.Bari.ConsoleApp;
using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Banco.Bari.Tests
{
    public class MessageTest
    {
        private Faker _faker;
        public MessageTest()
        {
            _faker = new Faker();
        }        

        [Fact]
        public void ShouldSetAllProperties_WhenOriginAndTextIsInformed()
        {
            var origin = _faker.Name.JobArea().ToString();
            var text = _faker.Lorem.Word();

            var message = new Message(origin, text);

            Assert.NotNull(message);
            Assert.False(string.IsNullOrEmpty(text));
            Assert.False(string.IsNullOrEmpty(origin));
            Assert.NotEqual(Guid.Empty, message.Id);
            Assert.True(message.Time != TimeSpan.Zero);
        }

        [Fact]
        public void ShouldSetAnActualTime_WhenIsConstructed()
        {
            var startTime = DateTime.Now.TimeOfDay;
            var message = new Message("", "");

            Assert.True(message.Time >= startTime);
        }

        [Fact]
        public void ShouldSetOriginAndText_WhenItsInformed()
        {
            var origin = _faker.Name.JobArea().ToString();
            var text = _faker.Lorem.Word();

            var message = new Message(origin, text);

            Assert.Equal(origin, message.Origin);
            Assert.Equal(text, message.Text);
        }


    }
}

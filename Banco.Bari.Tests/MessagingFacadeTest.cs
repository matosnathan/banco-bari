using Banco.Bari.ConsoleApp;
using Banco.Bari.ConsoleApp.Handlers;
using Banco.Bari.ConsoleApp.Models;
using Banco.Bari.ConsoleApp.Providers;
using Banco.Bari.ConsoleApp.Services;
using Moq;
using System;
using Xunit;

namespace Banco.Bari.Tests
{
    public class MessagingFacadeTest
    {
        [Theory]
        [InlineData("mensagem","")]
        [InlineData("mensagem",null)]
        [InlineData(null,"topico")]
        [InlineData("","topico")]
        public void Publish_ShouldThrowsArgumentNull_WhenMessageOrTopicIsNotInformed(string message, string topic)
        {
            var rabbitProviderMoq = new Mock<IRabbitMQProvider>();
            var handlerMoq = new Mock<MessageHandler>();
            rabbitProviderMoq.Setup(x => x.Publish(It.IsAny<Message>(), It.IsAny<string>()));
            var app = new App("my_app");
            var service = new MessagingFacade(rabbitProviderMoq.Object, app, null);

            Assert.Throws<ArgumentNullException>(() => { service.Publish(message, topic); });
        }

        [Fact]
        public void Publish_ShouldPublishAMessage_WhenAllRequiredPropertiesIsInformed()
        {
            var rabbitProviderMoq = new Mock<IRabbitMQProvider>();
            var handlerMoq = new Mock<MessageHandler>();
            rabbitProviderMoq.Setup(x => x.Publish(It.IsAny<Message>(), It.IsAny<string>()));
            var app = new App("my_app");
            var service = new MessagingFacade(rabbitProviderMoq.Object, app, null);

            service.Publish("my_message", "my_topic");

            rabbitProviderMoq.Verify(x => x.Publish(It.IsAny<Message>(), It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Subscribe_ShouldThrowsArgumentNull_WhenTopicIsNotInformed(string topic)
        {
            var rabbitProviderMoq = new Mock<IRabbitMQProvider>();
            var writterMoq = new Mock<IWritterService>();
            rabbitProviderMoq.Setup(x => x.Subscribe(It.IsAny<string>(), It.IsAny<Action<Message>>()));
            var handler = new MessageHandler(writterMoq.Object);

            var app = new App("my_app");
            var service = new MessagingFacade(rabbitProviderMoq.Object, app, handler);

            Assert.Throws<ArgumentNullException>(() => { service.Subscribe(topic); });
        }

        [Fact]
        public void Subscribe_ShouldSubscribeInProvider_WhenAllPropertiesIsInformed()
        {
            var rabbitProviderMoq = new Mock<IRabbitMQProvider>();
            var writterMoq = new Mock<IWritterService>();
            rabbitProviderMoq.Setup(x => x.Subscribe(It.IsAny<string>(), It.IsAny<Action<Message>>()));
            var handler = new MessageHandler(writterMoq.Object);

            var app = new App("my_app");
            var service = new MessagingFacade(rabbitProviderMoq.Object, app, handler);

            service.Subscribe("my_topic");

            rabbitProviderMoq.Verify(x => x.Subscribe(It.IsAny<string>(), It.IsAny<Action<Message>>()), Times.Once);
        }
    }
}

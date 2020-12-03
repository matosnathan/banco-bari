using System;

namespace Banco.Bari.ConsoleApp
{
    public class Message
    {
        public Message(string origin, string text)
        {
            Id = Guid.NewGuid();
            Origin = origin;
            Time = DateTime.Now.TimeOfDay;
            Text = text;
        }
        protected Message() { }

        public Guid Id { get; private set; }
        public string Origin { get; private set; }
        public TimeSpan Time { get; private set; }
        public string Text { get; private set; }
    }
}

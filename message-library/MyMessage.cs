using EasyNetQ;

namespace message_library
{
    //[Queue("MyMessageQueue", ExchangeName = "MyMessageExchange")]
    public class MyMessage : IQueueMessage
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
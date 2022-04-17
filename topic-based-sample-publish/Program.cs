using EasyNetQ;
using message_library;

public class Program
{
    static void Main(string[] args)
    {      

        using (var bus = RabbitHutch.CreateBus("host=localhost;username=admin;password=admin"))
        {
            bus.PubSub.Publish<IQueueMessage>(new MyMessage() { Id = 1, Text = "My Message 1" }, "message.basic");
            bus.PubSub.Publish<IQueueMessage>(new MyMessage() { Id = 2, Text = "My Message 2" }, "message.basic");

            bus.PubSub.Publish<IQueueMessage>(new MyAnotherMessage() { Id = 3, Message = "My Message 3" }, "message.another");
            bus.PubSub.Publish<IQueueMessage>(new MyAnotherMessage() { Id = 4, Message = "My Message 4" }, "message.another");
            Console.ReadLine();
        }


    }
}


using EasyNetQ;
using message_library;

public class Program
{
    static async Task Main(string[] args)
    {
        using (var bus = RabbitHutch.CreateBus("host=localhost;username=admin;password=admin"))
        {
            bus.PubSub.Subscribe<IQueueMessage>("basic", HandleMessage, msg => msg.WithTopic("message.basic"));

            Console.ReadLine();
        }

        //using (var bus = RabbitHutch.CreateBus("host=localhost;username=admin;password=admin"))
        //{
        //    bus.PubSub.Subscribe<IQueueMessage>("another", HandleAnotherMessage, msg => msg.WithTopic("message.another"));

        //    Console.ReadLine();
        //}

        //using (var bus = RabbitHutch.CreateBus("host=localhost;username=admin;password=admin"))
        //{
        //    bus.PubSub.Subscribe<IQueueMessage>("wildcard", HandleBothMessage, msg => msg.WithTopic("message.*"));

        //    Console.ReadLine();
        //}
    }

    private static void HandleMessage(IQueueMessage msg)
    {
        var message = msg as MyMessage;
        if (message != null)
        {
            Console.WriteLine(message.Id + " " + message.Text);
        }
    }

    private static void HandleAnotherMessage(IQueueMessage msg)
    {
        var message = msg as MyAnotherMessage;
        if (message != null)
        {
            Console.WriteLine(message.Id + " " + message.Message);
        }
    }

    private static void HandleBothMessage(IQueueMessage msg)
    {        
        if (msg is MyMessage message)
        {
            Console.WriteLine(message.Id + " " + message.Text);
        }

        if (msg is MyAnotherMessage anotherMessage)
        {
            Console.WriteLine(anotherMessage.Id + " " + anotherMessage.Message);
        }
    }
}




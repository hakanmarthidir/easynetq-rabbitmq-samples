using EasyNetQ;
using message_library;

public class Program
{
    static async Task Main(string[] args)
    {

        //1- Basic Sample - async

        //using (var bus = RabbitHutch.CreateBus("host=localhost;username=admin;password=admin"))
        //{
        //    await bus.PubSub.SubscribeAsync<MyMessage>("",
        //        message => Task.Factory.StartNew(() =>
        //        {

        //            Console.WriteLine(message.Text);

        //        })
        //        ).ConfigureAwait(false);

        //    Console.WriteLine("Listening for messages...");
        //    Console.ReadLine();
        //}

        //2 - Polymorphic Sample 

        using (var bus = RabbitHutch.CreateBus("host=localhost;username=admin;password=admin"))
        {
            await bus.PubSub.SubscribeAsync<IQueueMessage>("",
                message => Task.Factory.StartNew(() =>
                {
                    //Console.WriteLine(message.Id);

                    //or 
                    var messageNormal = message as MyMessage;
                    var messageAnother = message as MyAnotherMessage;

                    if (messageNormal != null) { Console.WriteLine(messageNormal.Text); }                  
                    else if (messageAnother != null) { Console.WriteLine(messageAnother.Message); }
                    else
                    {
                        Console.WriteLine("Invalid Message Type...");
                    }

                })
                ).ConfigureAwait(false);

            Console.WriteLine("Listening for messages...");
            Console.ReadLine();
        }

    }   


    //static void HandleMyMessage(MyMessage incomingMessage)
    //{ 
    //    Console.WriteLine(incomingMessage.Text);
    //}
}

using EasyNetQ;
using message_library;

public class Program
{
    static async Task Main(string[] args)
    {
        var message = new MyMessage()
        {
            Id = 1,
            Text = "My Message 1"
        };

        //1- BASIC SAMPLE 

        //using (var bus = RabbitHutch.CreateBus("host=localhost;username=admin;password=admin"))
        //{
        //    await bus.PubSub.PublishAsync<MyMessage>(message).ConfigureAwait(false);
        //    Console.WriteLine("published...");
        //}



        //2- with PublisherConfirm and Task Situation Control 
        //using (var bus = RabbitHutch.CreateBus("host=localhost;publisherConfirms=true;username=admin;password=admin"))
        //{
        //    await bus.PubSub.PublishAsync<MyMessage>(message).ContinueWith(task=> {

        //        if (task.IsCompleted && !task.IsFaulted)
        //        {
        //            Console.WriteLine("published...");
        //        }
        //        if (task.IsFaulted)
        //        {
        //            Console.WriteLine("an error occured while publishing a message...");
        //            // you can look into the task.Exception object
        //        }

        //    }).ConfigureAwait(false);            
        //}

        //3- Polymorphic 

        var anotherMessage = new MyAnotherMessage()
        {
            Id = 2,
            Message = "My Another Message 1"
        };

        using (var bus = RabbitHutch.CreateBus("host=localhost;publisherConfirms=true;username=admin;password=admin"))
        {
            await bus.PubSub.PublishAsync<IQueueMessage>(message).ContinueWith(task => {

                if (task.IsCompleted && !task.IsFaulted)
                {
                    Console.WriteLine("published...");
                }
                if (task.IsFaulted)
                {
                    Console.WriteLine("an error occured while publishing a message...");
                    // you can look into the task.Exception object
                }

            }).ConfigureAwait(false);

            await bus.PubSub.PublishAsync<IQueueMessage>(anotherMessage).ContinueWith(task => {

                if (task.IsCompleted && !task.IsFaulted)
                {
                    Console.WriteLine("another published...");
                }
                if (task.IsFaulted)
                {
                    Console.WriteLine("an error occured while publishing a message...");
                    // you can look into the task.Exception object
                }

            }).ConfigureAwait(false);
        }


    }
}
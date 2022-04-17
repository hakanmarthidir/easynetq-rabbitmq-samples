using EasyNetQ;
using message_library;
using System.Collections.Concurrent;

public class Program
{
    static async Task Main(string[] args)
    {

        var workers = new BlockingCollection<MyMessageHandler>();
        for (int i = 0; i < 10; i++)
        {
            var worker = new MyMessageHandler();
            workers.Add(worker);
        }

        using (var bus = RabbitHutch.CreateBus("host=localhost;username=admin;password=admin"))
        {
            await bus.Rpc.RespondAsync<MyMessage, MyMessageResponse>(request => 
            
            Task.Factory.StartNew(()=> {

                var worker = workers.Take();
                try
                {
                    return worker.HandleMyMessage(request);
                }
                finally 
                {
                    workers.Add(worker);
                }
            
            }));
           
            Console.ReadLine();
        }
    }   
}

public class MyMessageHandler
{
    public MyMessageResponse HandleMyMessage(MyMessage incomingMessage)
    {
        //handle incommingMessage
        return new MyMessageResponse() { Status = Guid.NewGuid().ToString() };
    }
}

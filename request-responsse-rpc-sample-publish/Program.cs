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

        using (var bus = RabbitHutch.CreateBus("host=localhost;username=admin;password=admin;timeout=10"))
        {
            var task = bus.Rpc.RequestAsync<MyMessage, MyMessageResponse>(message);

            task.ContinueWith(x => 
            {
                Console.WriteLine(x.Result.Status);
            });

            Console.ReadLine();
        }


    }
}


using EasyNetQ;
using message_library;

public class Program
{
    static async Task Main(string[] args)
    {       
        using (var bus = RabbitHutch.CreateBus("host=localhost;username=admin;password=admin"))
        {
            bus.SendReceive.Receive<MyMessage>("myTestQueue", HandleMessage);

            Console.ReadLine();
        }
    }

    private static void HandleMessage(MyMessage msg)
    {
        Console.WriteLine(msg.Id + " " + msg.Text);
    }
}



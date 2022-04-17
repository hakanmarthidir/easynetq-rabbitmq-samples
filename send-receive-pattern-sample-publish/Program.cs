using EasyNetQ;
using message_library;

public class Program
{
    static void Main(string[] args)
    {       
        using (var bus = RabbitHutch.CreateBus("host=localhost;username=admin;password=admin;timeout=10"))
        {
            bus.SendReceive.Send<MyMessage>("myTestQueue", new MyMessage() { Id = 1, Text = "My Message 1"});
            bus.SendReceive.Send<MyMessage>("myTestQueue", new MyMessage() { Id = 2, Text = "My Message 2"});
            bus.SendReceive.Send<MyMessage>("myTestQueue", new MyMessage() { Id = 3, Text = "My Message 3"});
            Console.ReadLine();
        }
    }
}


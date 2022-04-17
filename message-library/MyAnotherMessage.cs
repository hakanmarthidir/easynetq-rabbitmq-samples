namespace message_library
{
    public class MyAnotherMessage : IQueueMessage
    {
        public int Id { get; set; }
        public string Message { get; set; }
    }
}
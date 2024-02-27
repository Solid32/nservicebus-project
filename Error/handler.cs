using NServiceBus;
public class MyMessageHandler : IHandleMessages<MyMessage>
{
    static string file = "error_log.csv";
    static string separator = ",";

    public Task Handle(MyMessage message, IMessageHandlerContext context)
    {
        // Obtenez l'ID du message
        var messageId = context.MessageId;

        string[] newLine = {messageId , message.Data};
        string lineToWrite = string.Join(separator, newLine) + Environment.NewLine; // Add newline at the end

        try
        {
            File.AppendAllText(file, lineToWrite);
            Console.WriteLine($"Data has been successfully appended to the CSV file, Context: {context.MessageHeaders}, {context.MessageId}, {context}");
        }
        catch (Exception)
        {
            Console.WriteLine("Data could not be appended to the CSV file.");
        }

        return Task.CompletedTask;
    }
}

public class MyMessage : IMessage
{
    public string Data { get; set; } = string.Empty;
}

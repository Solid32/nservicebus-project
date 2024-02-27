using NServiceBus;

class Program
{
    static async Task Main(string[] args)
    {
        var endpointConfiguration = new EndpointConfiguration("NSMQProcess");
        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        transport.ConnectionString("amqp://guest:guest@localhost:5672/");
        endpointConfiguration.EnableInstallers();
        transport.UseConventionalRoutingTopology();
        endpointConfiguration.UseSerialization<NewtonsoftJsonSerializer>();

        var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

        Console.WriteLine("Appuyez sur 'Entrée' pour envoyer un message. Pour quitter, Ctrl + C");
        Console.ReadLine();

        int i = 0;
        string csvFilePath = "../Data/151_Pokemon.csv";

        // Lire le fichier CSV ligne par ligne et envoyer chaque ligne en tant que message
        using (var reader = new StreamReader(csvFilePath))
        {
            string? line;
            while ((line = reader.ReadLine()) != null ) // limiter le nombre de lignes envoyées pour l'exemple
            {
                line = JoinAbilities(line);
                var message = new MyMessage { Data = line };
                await endpointInstance.Send("PokéEx", message).ConfigureAwait(false);

                Console.WriteLine($"Message envoyé avec le contenu : {line}");
                i++;
            }
        }

        Console.WriteLine(" Toutes les lignes du fichier CSV ont été envoyées avec succès.");
        Console.WriteLine(" Appuyez sur [entrée] pour quitter.");
        Console.ReadLine();
    }
        static string JoinAbilities(string line) // fonction pour les abilités
    {
        int startIndex = line.IndexOf('[');
        if (startIndex != -1)
        {
            int endIndex = line.IndexOf(']', startIndex);
            if (endIndex != -1)
            {
                string abilities = line.Substring(startIndex + 1, endIndex - startIndex - 1);
                string joinedAbilities = abilities.Replace(", ", ";");
                line = line.Remove(startIndex + 1, endIndex - startIndex - 1).Insert(startIndex + 1, joinedAbilities);
            }
        }
        return line;
    }
}

public class MyMessage : IMessage
{
    public string Data { get; set; } = string.Empty;
}

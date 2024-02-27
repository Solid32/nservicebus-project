using NServiceBus;
class Program
{
    static async Task Main(string[] args)
    {
        var endpointConfiguration = new EndpointConfiguration("PokéQ");
        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        transport.ConnectionString("amqp://guest:guest@localhost:5672/");


        transport.UseConventionalRoutingTopology();

        endpointConfiguration.UseSerialization<NewtonsoftJsonSerializer>();

        // Configuration de la récupération en cas d'échec

        var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(true);

        Console.WriteLine("Appuyez sur 'Entrée' pour quitter");

        Console.ReadLine();

        await endpointInstance.Stop().ConfigureAwait(true);
    }
}

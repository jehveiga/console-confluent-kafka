using Confluent.Kafka;

var config = new ConsumerConfig
{
    GroupId = "consumer-teste01",
    BootstrapServers = "localhost:9092"
};

using var consumer = new ConsumerBuilder<string, string>(config).Build();

// Se increvendo na fila
consumer.Subscribe("topico-teste");

while (true)
{
    // Será efetuado a leitura das mensagens, ela será lida uma a uma dentro do looping
    var result = consumer.Consume();
    Console.WriteLine($"Mensagem: {result.Message.Key} - {result.Message.Value}");
}

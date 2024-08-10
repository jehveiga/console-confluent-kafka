using Confluent.Kafka;

var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

using var producer = new ProducerBuilder<string, string>(config).Build();

var message = new Message<string, string>
{
    Key = Guid.NewGuid().ToString(),
    Value = $"Teste de mensagem {DateTime.Now.Second}"
};


var result = await producer.ProduceAsync(topic: "topico-teste", message: message);


Console.WriteLine($"{result.Offset}");

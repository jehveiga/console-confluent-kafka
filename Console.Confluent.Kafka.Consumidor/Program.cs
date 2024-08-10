using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using modelo.schema;

// Configurando a configurando que será passada para gerar o Schema usando o Avro
var schemaConfig = new SchemaRegistryConfig
{
    Url = "http://localhost:8081"
};

var schemaRegistry = new CachedSchemaRegistryClient(schemaConfig);

var config = new ConsumerConfig
{
    GroupId = "consumer-teste01",
    BootstrapServers = "localhost:9092"
};

using var consumer = new ConsumerBuilder<string, Modelo>(config)
    .SetValueDeserializer(new AvroDeserializer<Modelo>(schemaRegistry).AsSyncOverAsync())
    .Build();

// Se increvendo na fila
consumer.Subscribe("modelos");

while (true)
{
    // Será efetuado a leitura das mensagens, ela será lida uma a uma dentro do looping
    var result = consumer.Consume();
    Console.WriteLine($"Mensagem: {result.Message.Value.Descricao}");
}

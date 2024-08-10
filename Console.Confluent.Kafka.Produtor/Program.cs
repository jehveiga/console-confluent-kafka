using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using modelo.schema;

// Configurando a configurando que será passada para gerar o Schema usando o Avro
var schemaConfig = new SchemaRegistryConfig
{
    Url = "http://localhost:8081"
};

var schemaRegistry = new CachedSchemaRegistryClient(schemaConfig);

var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

using var producer = new ProducerBuilder<string, Modelo>(config)
    .SetValueSerializer(new AvroSerializer<Modelo>(schemaRegistry))
    .Build();

var message = new Message<string, Modelo>
{
    Key = Guid.NewGuid().ToString(),
    Value = new Modelo
    {
        Id = Guid.NewGuid().ToString(),
        Descricao = "Modelo do Apache Kafka"
    }
};


var result = await producer.ProduceAsync(topic: "modelos", message: message);


Console.WriteLine($"{result.Offset}");

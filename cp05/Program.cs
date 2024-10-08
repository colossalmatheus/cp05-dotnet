using cp05.Service;
using cp05.Repository;
using cp05.Model; // Certifique-se de incluir os namespaces necessários
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner.
builder.Services.AddControllers();

// Configuração do MongoDB
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetValue<string>("MongoDB:ConnectionString");
    
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new ArgumentException("Connection string cannot be null or empty", nameof(connectionString));
    }

    return new MongoClient(connectionString);
});

// Registro do BancoRepository e da interface IBancoRepository
builder.Services.AddSingleton<IBancoRepository>(sp =>
{
    var mongoClient = sp.GetRequiredService<IMongoClient>();
    var configuration = sp.GetRequiredService<IConfiguration>();
    var databaseName = configuration.GetValue<string>("MongoDB:DatabaseName");
    var collectionName = configuration.GetValue<string>("MongoDB:CollectionName");
    
    if (string.IsNullOrEmpty(databaseName))
    {
        throw new ArgumentException("Database name cannot be null or empty", nameof(databaseName));
    }
    
    if (string.IsNullOrEmpty(collectionName))
    {
        throw new ArgumentException("Collection name cannot be null or empty", nameof(collectionName));
    }

    return new BancoRepository(mongoClient, databaseName, collectionName);
});

// Adiciona suporte ao Swagger para documentação da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurações de pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
using cp05.Model;
using MongoDB.Driver;

namespace cp05.Service;

public class BancoService
{
    private readonly IMongoCollection<BancoModel> _bancoCollection;

    public BancoService(IMongoClient mongoClient, string databaseName, string collectionName)
    {
        var database = mongoClient.GetDatabase(databaseName);
        _bancoCollection = database.GetCollection<BancoModel>(collectionName);
    }

    // Método para adicionar dados
    public async Task AddDataAsync(BancoModel data)
    {
        await _bancoCollection.InsertOneAsync(data);
    }

    // Método para alterar dados
    public async Task UpdateDataAsync(string id, BancoModel updatedData)
    {
        await _bancoCollection.ReplaceOneAsync(d => d.Id == id, updatedData);
    }

    // Método para excluir dados
    public async Task DeleteDataAsync(string id)
    {
        await _bancoCollection.DeleteOneAsync(d => d.Id == id);
    }

    // Método para consultar todos os dados
    public async Task<List<BancoModel>> GetAllDataAsync()
    {
        return await _bancoCollection.Find(_ => true).ToListAsync();
    }
}
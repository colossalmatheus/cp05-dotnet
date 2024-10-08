using cp05.Model;
using MongoDB.Driver;

namespace cp05.Repository;

public class BancoRepository : IBancoRepository
{
    private readonly IMongoCollection<BancoModel> _bancos;

    public BancoRepository(IMongoClient mongoClient, string databaseName, string collectionName)
    {
        var database = mongoClient.GetDatabase(databaseName);
        _bancos = database.GetCollection<BancoModel>(collectionName);
    }

    public async Task SaveAsync(BancoModel banco)
    {
        await _bancos.InsertOneAsync(banco);
    }

    public async Task<BancoModel> GetByIdAsync(string id)
    {
        var filter = Builders<BancoModel>.Filter.Eq(b => b.Id, id);
        return await _bancos.Find(filter).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(string id, BancoModel updateBanco)
    {
        var filter = Builders<BancoModel>.Filter.Eq(b => b.Id, id);
        await _bancos.ReplaceOneAsync(filter, updateBanco);
    }

    public async Task DeleteAsync(string id)
    {
        var filter = Builders<BancoModel>.Filter.Eq(b => b.Id, id);
        await _bancos.DeleteOneAsync(filter);
    }

    public async Task<List<BancoModel>> GetAllAsync()
    {
        return await _bancos.Find(_ => true).ToListAsync();
    }
}

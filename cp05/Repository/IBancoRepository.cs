using cp05.Model;

namespace cp05.Repository;

public interface IBancoRepository
{
    Task SaveAsync(BancoModel banco);
    Task<BancoModel> GetByIdAsync(string id);
    Task UpdateAsync(string id, BancoModel updateBanco);
    Task DeleteAsync(string id);
    Task<List<BancoModel>> GetAllAsync();
}
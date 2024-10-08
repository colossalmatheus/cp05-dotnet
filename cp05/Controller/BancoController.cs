using cp05.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using cp05.Repository;

[ApiController]
[Route("api/[controller]")]
public class BancoController : ControllerBase
{
    private readonly IBancoRepository _bancoRepository;

    public BancoController(IBancoRepository bancoRepository)
    {
        _bancoRepository = bancoRepository;
    }

    [HttpPost]
    public async Task<IActionResult> AddBanco([FromBody] BancoModel banco)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _bancoRepository.SaveAsync(banco);
            return Ok(new ResponseModel { Message = "Banco Teste adicionado com sucesso!" });
        }
        catch (System.Exception ex)
        {
            return StatusCode(500, new ResponseModel { Message = $"Erro interno: {ex.Message}" });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBanco(string id, [FromBody] BancoModel updateBanco)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var existingBanco = await _bancoRepository.GetByIdAsync(id);
            if (existingBanco == null)
            {
                return NotFound(new ResponseModel { Message = "Banco Teste não encontrado." });
            }

            await _bancoRepository.UpdateAsync(id, updateBanco);
            return Ok(new ResponseModel { Message = "Banco Teste atualizado com sucesso!" });
        }
        catch (System.Exception ex)
        {
            return StatusCode(500, new ResponseModel { Message = $"Erro interno: {ex.Message}" });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBanco(string id)
    {
        try
        {
            var existingBanco = await _bancoRepository.GetByIdAsync(id);
            if (existingBanco == null)
            {
                return NotFound(new ResponseModel { Message = "Banco Teste não encontrado." });
            }

            await _bancoRepository.DeleteAsync(id);
            return Ok(new ResponseModel { Message = "Banco Teste deletado com sucesso!" });
        }
        catch (System.Exception ex)
        {
            return StatusCode(500, new ResponseModel { Message = $"Erro interno: {ex.Message}" });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBanco()
    {
        try
        {
            var data = await _bancoRepository.GetAllAsync();
            return Ok(data);
        }
        catch (System.Exception ex)
        {
            return StatusCode(500, new ResponseModel { Message = $"Erro interno: {ex.Message}" });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBancoById(string id)
    {
        try
        {
            var banco = await _bancoRepository.GetByIdAsync(id);
            if (banco == null)
            {
                return NotFound(new ResponseModel { Message = "Banco Teste não encontrado." });
            }

            return Ok(banco);
        }
        catch (System.Exception ex)
        {
            return StatusCode(500, new ResponseModel { Message = $"Erro interno: {ex.Message}" });
        }
    }
}

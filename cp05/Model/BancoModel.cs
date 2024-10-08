using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace cp05.Model;

public class BancoModel
{
    public string Id { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo Agência é obrigatório.")]
    public string Agencia { get; set; }

    [Required(ErrorMessage = "O campo Conta é obrigatório.")]
    public string Conta { get; set; }
}

using System.ComponentModel.DataAnnotations;
using Dima.Core.Enums;

namespace Dima.Core.Requests.Transactions;

public class UpdateTransactionRequest : Request
{
    public long Id { get; set; }
    

    [Required(ErrorMessage = "Título inválido")]
    [MaxLength(80, ErrorMessage = "O título deve conter até 80 caracteres")]
    public string Title { get; set; } = string.Empty;


    [Required(ErrorMessage = "Tipo inválido")]
    public ETransactionType Type { get; set; }


    [Required(ErrorMessage = "Valor inválido")]
    public decimal Amount { get; set; }


    [Required(ErrorMessage = "Categoria inválido")]
    public long CategoryId { get; set; }


    [Required(ErrorMessage = "Data inválido")]
    public DateTime? PaidOrReceivedAt { get; set; }
}

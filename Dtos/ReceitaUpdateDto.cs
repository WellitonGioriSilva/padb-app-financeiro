using System.ComponentModel.DataAnnotations;

namespace ApiFinanceiro.Dtos
{
    public class ReceitaUpdateDto : ReceitaDto
    {
        [Required]
        public required string Situacao { get; set; }

        [Required]
        public required DateOnly DataRecebimento { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiFinanceiro.Models
{
    [Table("receitas"), PrimaryKey(nameof(Id))]
    public class Receita
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("descricao")]
        public required string Descricao { get; set; }

        [Column("valor")]
        public required decimal Valor { get; set; }

        [Column("data_previsao")]
        public DateOnly? DataPrevisao { get; set; }

        [Column("data_recebimento")]
        public DateOnly? DataRecebimento { get; set; }

        [Column("categoria")]
        public required string Categoria { get; set; }

        [Column("observacao")]
        public required string Observacao { get; set; }

        [Column("situacao")]
        public string Situacao { get; set; } = "pendente";

    }
}

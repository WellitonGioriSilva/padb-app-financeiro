using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiFinanceiro.Models
{
    [Table("despesas"), PrimaryKey(nameof(Id))]
    public class Despesa
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("categoria")]
        public string Categoria { get; set; }

        [Column("valor")]
        public decimal Valor { get; set; }

        [Column("data_vencimento")]
        public DateOnly DataVencimento { get; set; }

        [Column("situacao")]
        public string Situacao { get; set; } = "pendente";

        [Column("dt_pagamento")]
        public DateTime? DataPagamento { get; set; }
    }
}

using ApiFinanceiro.Dtos;
using ApiFinanceiro.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinanceiro.Controllers
{
    [Route("/receitas")]
    [ApiController]
    public class ReceitaController : ControllerBase
    {
        private static List<Receita> listaReceitas = new()
        {
            new Receita
            {
                Descricao = "Salário",
                Valor = 3500.00m,
                DataPrevisao = new DateOnly(2026, 3, 5),
                DataRecebimento = new DateOnly(2026, 3, 5),
                Categoria = "Salário",
                Observacao = "Pagamento mensal da empresa",
                Situacao = "Recebido"
            },
            new Receita
            {
                Descricao = "Freelance Website",
                Valor = 1200.00m,
                DataPrevisao = new DateOnly(2026, 3, 10),
                DataRecebimento = new DateOnly(2026, 3, 12),
                Categoria = "Freelance",
                Observacao = "Desenvolvimento de site para cliente",
                Situacao = "Recebido"
            },
            new Receita
            {
                Descricao = "Dividendos",
                Valor = 320.50m,
                DataPrevisao = new DateOnly(2026, 3, 20),
                DataRecebimento = new DateOnly(2026, 3, 20),
                Categoria = "Investimentos",
                Observacao = "Dividendos de ações",
                Situacao = "Recebido"
            },
            new Receita
            {
                Descricao = "Aluguel",
                Valor = 900.00m,
                DataPrevisao = new DateOnly(2026, 3, 25),
                DataRecebimento = new DateOnly(2026, 3, 25),
                Categoria = "Imóveis",
                Observacao = "Aluguel da casa",
                Situacao = "Recebido"
            }
        };

        [HttpGet()]
        public ActionResult FindAll()
        {
            return Ok(listaReceitas);
        }

        [HttpPost()]
        public ActionResult Create([FromBody]ReceitaDto novaReceita)
        {
            var receita = new Receita
            {
                Descricao = novaReceita.Descricao,
                Valor = novaReceita.Valor,
                DataPrevisao = novaReceita.DataPrevisao,
                Categoria = novaReceita.Categoria,
                Observacao = novaReceita.Observacao,
                Situacao = "Pendente"
            };

            listaReceitas.Add(receita);

            return Created("", receita);
        }

        [HttpGet("{id}")]
        public ActionResult FindById(Guid id)
        {
            var receita = listaReceitas.FirstOrDefault(r => r.Id == id);

            if (receita is null)
            {
                return NotFound(new { mensagem = $"Receita #{id} não encontrada" });
            }

            return Ok(receita);
        }

        [HttpPut("{id}")]
        public ActionResult Update(Guid id, [FromBody] ReceitaUpdateDto receitaDto)
        {
            var receita = listaReceitas.FirstOrDefault(r => r.Id == id);

            if (receita is null)
            {
                return NotFound(new { mensagem = $"Receita #{id} não encontrada" });
            }

            receita.Descricao = receitaDto.Descricao;
            receita.Valor = receitaDto.Valor;
            receita.Categoria = receitaDto.Categoria;
            receita.Observacao = receitaDto.Observacao;
            receita.Situacao = receitaDto.Situacao;
            receita.DataRecebimento = receitaDto.DataRecebimento;
            receita.DataPrevisao = receitaDto.DataPrevisao;

            return Ok(receita);
        }

        [HttpDelete("{id}")]
        public ActionResult Remove(Guid id)
        {
            var receita = listaReceitas.FirstOrDefault(r => r.Id == id);

            if (receita is null)
            {
                return NotFound(new { mensagem = $"Receita #{id} não encontrada" });
            }

            listaReceitas.Remove(receita);

            return NoContent();
        }
    }
}

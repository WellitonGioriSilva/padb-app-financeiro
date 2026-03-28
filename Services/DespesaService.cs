using ApiFinanceiro.DataContext;
using ApiFinanceiro.Dtos;
using ApiFinanceiro.Exceptions;
using ApiFinanceiro.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiFinanceiro.Services
{
    public class DespesaService
    {
        private readonly AppDbContext _context;

        public DespesaService(AppDbContext context)
        {
            _context = context; 
        }

        public async Task<ICollection<Despesa>> FindAll()
        {
            try
            {
                return _context.Despesas.ToList();
            }catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<Despesa> Create(DespesaDto novaDespesa)
        {
            try
            {
                var despesa = new Despesa
                {
                    Descricao = novaDespesa.Descricao,
                    Valor = novaDespesa.Valor,
                    Categoria = novaDespesa.Categoria,
                    DataVencimento = novaDespesa.DataVencimento,
                    Situacao = "Pendente"
                };


                await _context.Despesas.AddAsync(despesa);
                await _context.SaveChangesAsync();

                return despesa;
            }
            catch (Exception ex) {
                throw;            
            }
        }

        public async Task<Despesa> FindById(int id)
        {
            var despesa = _context.Despesas.FirstOrDefault(x => x.Id == id);

            if (despesa is null)
            {
                throw new ErrorServiceException("", c => c.NotFound(new {message = $"Despesa #{id} não encontrada" }));
            }

            return despesa;
        }

        public async Task<Despesa> Update(int id, DespesaUpdateDto despesaDto)
        {
            try
            {
                var despesa = await FindById(id);

                var dataPagamento = new DateTime(despesaDto.DataPagamento.Year, despesaDto.DataPagamento.Month, despesaDto.DataPagamento.Day);
                var dataVencimento = new DateTime(despesaDto.DataVencimento.Year, despesaDto.DataVencimento.Month, despesaDto.DataVencimento.Day);

                despesa.Descricao = despesaDto.Descricao;
                despesa.Valor = despesaDto.Valor;
                despesa.DataVencimento = despesaDto.DataVencimento;
                despesa.Categoria = despesaDto.Categoria;
                despesa.Situacao = despesaDto.Situacao;
                despesa.DataPagamento = dataPagamento;

                _context.Despesas.Update(despesa);
                await _context.SaveChangesAsync();

                return despesa;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task Remove(int id)
        {
            try
            {
                var despesa = await FindById(id);

                _context.Despesas.Remove(despesa);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //[HttpPost("{id}/pagamento")]
        //public ActionResult Pay([FromBody] DespesaDto novaDespesa)
        //{

        //    var despesa = new Despesa
        //    {
        //        Descricao = novaDespesa.Descricao,
        //        Valor = novaDespesa.Valor,
        //        Categoria = novaDespesa.Categoria,
        //        DataVencimento = novaDespesa.DataVencimento,
        //        Situacao = "Pendente"
        //    };

        //    listaDespesas.Add(despesa);

        //    return Created("", despesa);
        //}
    }
}

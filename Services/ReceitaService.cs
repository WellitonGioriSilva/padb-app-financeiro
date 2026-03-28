using ApiFinanceiro.DataContext;
using ApiFinanceiro.Dtos;
using ApiFinanceiro.Exceptions;
using ApiFinanceiro.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiFinanceiro.Services
{
    public class ReceitaService
    {
        private readonly AppDbContext _context;

        public ReceitaService(AppDbContext context)
        {
            _context = context; 
        }

        public async Task<ICollection<Receita>> FindAll()
        {
            try
            {
                return _context.Receitas.ToList();
            }catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<Receita> Create(ReceitaDto novaReceita)
        {
            try
            {
                var receita = new Receita
                {
                    Descricao = novaReceita.Descricao,
                    Valor = novaReceita.Valor,
                    Categoria = novaReceita.Categoria,
                    DataPrevisao = novaReceita.DataPrevisao,
                    Observacao = novaReceita.Observacao,
                    Situacao = "Pendente"
                };


                await _context.Receitas.AddAsync(receita);
                await _context.SaveChangesAsync();

                return receita;
            }
            catch (Exception ex) {
                throw;            
            }
        }

        public async Task<Receita> FindById(int id)
        {
            var receita = _context.Receitas.FirstOrDefault(x => x.Id == id);

            if (receita is null)
            {
                throw new ErrorServiceException("", c => c.NotFound(new {message = $"Receita #{id} não encontrada" }));
            }

            return receita;
        }

        public async Task<Receita> Update(int id, ReceitaUpdateDto receitaDto)
        {
            try
            {
                var receita = await FindById(id);

                receita.Descricao = receitaDto.Descricao;
                receita.Valor = receitaDto.Valor;
                receita.DataRecebimento = receitaDto.DataRecebimento;
                receita.Categoria = receitaDto.Categoria;
                receita.Situacao = receitaDto.Situacao;
                receita.DataPrevisao = receitaDto.DataPrevisao;
                receita.Observacao = receitaDto.Observacao;

                _context.Receitas.Update(receita);
                await _context.SaveChangesAsync();

                return receita;
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
                var receita = await FindById(id);

                _context.Receitas.Remove(receita);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

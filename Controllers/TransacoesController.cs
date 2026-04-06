using Microsoft.AspNetCore.Mvc;
using GlobalBankApi.Data;
using GlobalBankApi.Models;
namespace GlobalBankApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{

    private readonly AppDbContext context;   
    public TransacoesController(AppDbContext ctx){
        context = ctx;
    }
    [HttpGet("extrato/{id}")]
    public ActionResult Get(string id){
       var transacoes = context.Transacoes.Where(c => c.ContaId.Equals(id)).ToList();
        return Ok(transacoes);
    }
    [HttpPost]
    public ActionResult Post(Transacao transacao){
        var contaBancaria = context.ContaBancarias.FirstOrDefault(t => t.NumeroConta == transacao.ContaId);
        if (contaBancaria == null)
        {
            return NotFound();
        }

        if (transacao.Tipo.ToLower().Equals("saque"))
        {
            if (transacao.Valor > contaBancaria.Saldo){
                return Conflict("Saldo insuficiente.");}
            else if (transacao.Valor > 10000)
            {
                Console.WriteLine("🚩 ALERTA DE SEGURANÇA: Transação de alto valor detectada para a conta [Número da Conta]!");
            }
            contaBancaria.Saldo = contaBancaria.Saldo - transacao.Valor;
            context.ContaBancarias.Update(contaBancaria);
            context.Transacoes.Add(transacao);
            context.SaveChanges();
            return Ok();
        }else if (transacao.Tipo.ToLower().Equals("deposito"))
        {
            if (transacao.Valor <= 0)
            {
                return BadRequest("Não é possível depositar esse valor");
            }
            contaBancaria.Saldo = contaBancaria.Saldo + transacao.Valor;
            context.ContaBancarias.Update(contaBancaria);
            context.Transacoes.Add(transacao);
            context.SaveChanges();
            return Ok();
        }
        {
            return BadRequest("O saldo inicial não pode ser negativo para contas internacionais");
        }
    }

}

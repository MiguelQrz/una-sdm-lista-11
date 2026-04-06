using Microsoft.AspNetCore.Mvc;
using GlobalBankApi.Data;
using GlobalBankApi.Models;
using System.Text.Json.Serialization;
namespace GlobalBankApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContasController : ControllerBase
{

    private readonly AppDbContext context;   
    public ContasController(AppDbContext ctx){
        context = ctx;
    }
    [HttpGet("banco/dashboard")]
    public ActionResult GetAll(){
        var saldoTotal = context.Transacoes.Sum(c => c.Valor);
        var transacoesCount = context.Transacoes.Count();
        string response = string.Format("Patrimônio total: {0}\nQuantidade Total: {1}", saldoTotal, transacoesCount);
        return Content(response);
    }

    [HttpGet]
    public ActionResult Get(){
        var contas = context.ContaBancarias.ToList();
        return Ok(contas);
    }
    [HttpPost]
    public ActionResult Post(ContaBancaria cb){
        if (cb.Saldo < 0)
        {
            return BadRequest("O saldo inicial não pode ser negativo para contas internacionais");
        }
        context.Add(cb);
        context.SaveChanges();
        return CreatedAtAction(nameof(Get), cb);
    }

}
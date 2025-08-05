using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProvaSuficiencia.Context;
using ProvaSuficiencia.Domain.Comanda;
using ProvaSuficiencia.Dto.Comanda;

namespace ProvaSuficiencia.Controller.RestAPIFurb;


[ApiController]
[Authorize]
[Route("RestAPIFurb/comandas")]
public class ComandaController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext context = context;

    [HttpGet]
    public ActionResult<GetComandasDto> GetComandas()
    {
        var comandas = context.Comandas.Select(c => new GetComandasDto
        {
            IdUsuario = c.IdUsuario,
            NomeUsuario = c.NomeUsuario,
            TelefoneUsuario = c.TelefoneUsuario,
        });
        return Ok(comandas);
    }

    [HttpGet("{id}")]
    public ActionResult<GetComandaDto> GetComanda(int id)
    {
        var comandas = context.Comandas.Where(c => c.Id == id)
                                       .Select(c => new GetComandaDto
                                       {
                                           IdUsuario = c.IdUsuario,
                                           NomeUsuario = c.NomeUsuario,
                                           TelefoneUsuario = c.TelefoneUsuario,
                                           Produtos = c.Produtos.Select(p => new GetComandaDto.Produto
                                           {
                                               Id = p.Id,
                                               Nome = p.Nome,
                                               Preco = p.Preco,
                                           }),
                                       });
        if (!comandas.Any())
        {
            return NotFound();
        }
        return Ok(comandas.First());
    }

    [HttpPost]
    public async Task<ActionResult<PostComandaResponseDto>> PostComanda([FromBody] PostComandaDto postComandaDto)
    {
        var comanda = new Comanda
        {
            IdUsuario = postComandaDto.IdUsuario,
            NomeUsuario = postComandaDto.NomeUsuario,
            TelefoneUsuario = postComandaDto.TelefoneUsuario,
            Produtos = [.. postComandaDto.Produtos.Select(p => new Produto
            {
                Id = p.Id,
                Nome = p.Nome,
                Preco = p.Preco,
            })],
        };

        var erro = ValidadorComanda.Validar(comanda);
        if (erro is not null)
        {
            return BadRequest(erro);
        }

        await context.Comandas.AddAsync(comanda);
        await context.SaveChangesAsync();
        return StatusCode((int)HttpStatusCode.Created, new PostComandaResponseDto
        {
            Id = comanda.Id,
            IdUsuario = comanda.IdUsuario,
            NomeUsuario = comanda.NomeUsuario,
            TelefoneUsuario = comanda.TelefoneUsuario,
            Produtos = comanda.Produtos.Select(p => new PostComandaResponseDto.Produto
            {
                Id = p.Id,
                Nome = p.Nome,
                Preco = p.Preco,
            })
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutComanda(int id, [FromBody] PutComandaDto putComandaDto)
    {
        var comanda = context.Comandas.FirstOrDefault(c => c.Id == id);
        if (comanda is null)
        {
            return NotFound();
        }

        comanda.IdUsuario = putComandaDto.IdUsuario ?? comanda.IdUsuario;
        comanda.NomeUsuario = putComandaDto.NomeUsuario ?? comanda.NomeUsuario;
        comanda.TelefoneUsuario = putComandaDto.TelefoneUsuario ?? comanda.TelefoneUsuario;
        if (putComandaDto.Produtos is not null)
        {

            comanda.Produtos = [.. putComandaDto.Produtos.Select(p => new Produto
            {
                Id = p.Id,
                Nome = p.Nome,
                Preco = p.Preco,
                Comanda = comanda,
            })];

            var erro = ValidadorComanda.Validar(comanda);
            if (erro is not null)
            {
                return BadRequest(erro);
            }

            await context.Database.ExecuteSqlRawAsync("DELETE FROM Produtos WHERE ComandaId = {0}", comanda.Id);
            await context.Produtos.AddRangeAsync(comanda.Produtos);
        }
        else
        {
            var erro = ValidadorComanda.Validar(comanda);
            if (erro is not null)
            {
                return BadRequest(erro);
            }
        }

        await context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComanda(int id)
    {
        var comanda = context.Comandas.FirstOrDefault(c => c.Id == id);

        if (comanda is null)
        {
            return NotFound();
        }

        await context.Database.ExecuteSqlRawAsync("DELETE FROM Produtos WHERE ComandaId = {0}", id);
        await context.Database.ExecuteSqlRawAsync("DELETE FROM Comandas WHERE Id = {0}", id);
        await context.SaveChangesAsync();

        return Ok(new { success = new { text = "comanda removida" } });
    }

}


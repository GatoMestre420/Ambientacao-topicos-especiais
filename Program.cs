using System.ComponentModel.DataAnnotations;
using API.Models;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);
//Registrar o serviço de banco da dados
builder.Services.AddDbContext<AppDataContext>();

var app = builder.Build();



// Produto produto = new Produto();
// produto.Nome = "Bolacha";
// Console.WriteLine(produto.Nome);
//Funcionalidades da aplicação = EndPoints
// GET: http://localhost:5240/
app.MapGet("/", () => "API de Produtos");

// GET http://localhost:5240/produto/listar
app.MapGet("/produto/listar", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.TabelaProdutos.Any())
    {
        return Results.Ok(ctx.TabelaProdutos.ToList());
    }
    return Results.NotFound("Não existem produtos na tabela");

});

//http://localhost:5240/produto/buscar/nomedoproduto
app.MapGet("/produto/buscar/{id}", ([FromRoute] string id,
    [FromServices] AppDataContext ctx) =>
{
    Produto? produto = ctx.TabelaProdutos.Find(id);
    if (produto == null)
    {
        return Results.NotFound("Produto não encontrado!");
    }
    return Results.Ok(produto);

}
);

// Cadastrar Produtos dentro da lista
app.MapPost("/produto/cadastrar", ([FromBody] Produto produto, [FromServices] AppDataContext ctx) =>
 {
     List<ValidationResult> erros = new List<ValidationResult>();
     if (!Validator.TryValidateObject(produto, new ValidationContext(produto), erros, true
     ))
     {
         return Results.BadRequest(erros);
     }

     //REGRA DE NEGÓCIO - não é permitido o cadastro de um produto com um nome já cadastrado. 
     Produto? produtoEncontrado = ctx.TabelaProdutos.FirstOrDefault
         (x => x.Nome == produto.Nome);

     if (produtoEncontrado is null)
     {
         // Adicionar o objeto dentro da tabela de banco de dados
         ctx.TabelaProdutos.Add(produto);
         ctx.SaveChanges();
         return Results.Created("", produto);
     }
     return Results.BadRequest("Já Existe um produto com o mesmo nome");


 });

//deletar Produto
app.MapDelete("/produtos/deletar/{id}", ([FromRoute] string id,
   [FromServices] AppDataContext ctx) =>
{

    Produto? produto = ctx.TabelaProdutos.Find(id);
    if (produto is null)
    {
        return Results.NotFound("produto não encontrado!");
    }
    ctx.TabelaProdutos.Remove(produto);
    ctx.SaveChanges();
    return Results.Ok("Produto Deletado");
}
);

//Alterar produto --------------------------
app.MapPut("/produtos/alterar/{id}", ([FromRoute] string id, [FromBody] Produto produtoAlterado,
   [FromServices] AppDataContext ctx) =>
{
    Produto? produto = ctx.TabelaProdutos.Find(id);
    if (produto is null)
    {
        return Results.NotFound("Produto não encontrado!");
    }
    produto.Nome = produtoAlterado.Nome;
    produto.Descricao = produtoAlterado.Descricao;
    produto.Valor = produtoAlterado.Valor;
    ctx.TabelaProdutos.Update(produto);
    ctx.SaveChanges();
    return Results.Ok("Produto Alterado!");

});


app.Run();



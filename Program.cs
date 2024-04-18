using API.Models;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);
//Registrar o serviço de banco da dados
builder.Services.AddDbContext<AppDataContext>();

var app = builder.Build();



// Produto produto = new Produto();
// produto.Nome = "Bolacha";
// Console.WriteLine(produto.Nome);



List<Produto> produtos = new List<Produto>();
produtos.Add(new Produto("Celular", "IOS", 1000.00));
produtos.Add(new Produto("Celular", "ANDROID", 900.00));
produtos.Add(new Produto("Televisão", "LG", 5000.00));
produtos.Add(new Produto("Placa de video", "NVIDEO", 4500.00));

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
app.MapGet("/produto/buscar/{nome}", ([FromRoute] string nome) =>
{

    for (int i = 0; i < produtos.Count; i++)
    {
        if (produtos[i].Nome == nome)
        {
            //retornar o produto encontrado
            return Results.Ok(produtos[i]);
        }
    }
    return Results.NotFound("Produto não encontrado!");
}
);

// EXERCÍCIO - Cadastrar Produtos dentro da lista
app.MapPost("/produto/cadastrar", ([FromBody] Produto produto, [FromServices] AppDataContext ctx) =>
 {
     // Adicionar o objeto dentro da tabela de banco de dados
     ctx.TabelaProdutos.Add(produto);
     ctx.SaveChanges();
     return Results.Created("", produto);

 });

//deletar Produto
app.MapDelete("/produtos/deletar/{nome}", ([FromRoute] string nome) =>
{

    for (int i = 0; i < produtos.Count; i++)
    {
        if (produtos[i].Nome == nome)
        {
            produtos.Remove(produtos[i]);
            return Results.Ok(produtos);

        }
    }
    return Results.NotFound("Produto não encontrado!");
}
);

//Alterar produto --------------------------
app.MapPut("/produtos/alterar/{nome}", ([FromRoute] string nome, [FromBody] Produto produtoAlterado) =>
{
    Produto? produto = produtos.FirstOrDefault(x => x.Nome == nome);
    if (produto is null)
    {
        return Results.NotFound("Produto não encontrado!");
    }
    produto.Nome = produtoAlterado.Nome;
    produto.Descricao = produtoAlterado.Descricao;
    produto.Valor = produtoAlterado.Valor;
    return Results.Ok("Produto Alterado!");

});


app.Run();



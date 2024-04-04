using API.Models;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);
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
// GET: 
app.MapGet("/", () => "API de Produtos");

app.MapGet("/produto/listar", () => produtos);

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
app.MapPost("/produto/cadastrar", ([FromBody] Produto produto) =>
 {

     //  //Preencher o objeto pelo construtor
     //  Produto produto = new Produto(nome, descricao, valor);

     //  //Preencher o objeto pelos atributos 
     //  produto.Nome = nome;
     //  produto.Descricao = descricao;
     //  produto.Valor = valor;

     //Adicionar o objeto dentro da lista 
     produtos.Add(produto);
     return Results.Created("", produto);

 });


//Exercicios 
//1) Cadastrar um produto : ok
//a) pela URL : ok
//b) pelo corpo : ok
//2) remoção do Produto
//3) Alteração do produto

app.Run();



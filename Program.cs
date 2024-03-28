var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Produto produto = new Produto();
produto.setNome("bolacha");
Console.WriteLine(produto.getNome());

List<Produto> produtos = new List<Produto>();
produtos.Add(new Produto("Celular", "IOS"));
produtos.Add(new Produto("Celular", "ANDROID"));
produtos.Add(new Produto("Televisão", "LG"));
produtos.Add(new Produto("Placa de video", "NVIDEO"));

//Funcionalidades da aplicação = EndPoints
// GET: 
app.MapGet("/", () => "API de Produtos");

app.MapGet("/produto/listar", () => produtos);

// EXERCÍCIO - Cadastrar Produtos dentro da lista
app.MapPost("/produto/cadastrar", () => "cadastro de produtos!");


app.Run();

//registro = record
record Produto(string nome, string descricao);
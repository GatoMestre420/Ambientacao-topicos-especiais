namespace API.Models;
public class Produto
{

    //CONSTRUTOR
    public Produto() { }
    public Produto(string nome, string descricao, double valor)
    {
        Nome = nome;
        Descricao = descricao;
        Valor = valor;
    }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public double Valor { get; set; }


    //     private string nome;
    //     private string descricao;

    //     public void setNome(string nome)
    //     {

    //         this.nome = nome
    //     }
    //     public string getNome()
    //     {
    //         return this.nome;
    //     }
}


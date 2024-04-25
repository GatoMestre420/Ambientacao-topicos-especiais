using System.ComponentModel.DataAnnotations;

namespace API.Models;
public class Produto
{

    //CONSTRUTOR VAZIO
    public Produto()
    {

        Id = Guid.NewGuid().ToString();
        CriadoEm = DateTime.Now;

    }

    //CONSTRUTOR CHEIO
    public Produto(string nome, string descricao, double valor)
    {
        Id = Guid.NewGuid().ToString();
        Nome = nome;
        Descricao = descricao;
        Valor = valor;
        CriadoEm = DateTime.Now;
    }

    //Atributos ou propriedades = caracteristicas de um objeto

    public string Id { get; set; }

    //Anotações - Data Annotations
    [Required(ErrorMessage = "Este campo é obrigatório!")]
    public string? Nome { get; set; }

    [MinLength(3, ErrorMessage = "Este campo tem o tamanho minimo de 3 caracteres")]
    [MaxLength(12, ErrorMessage = "Este campo tem o tamanho máximo de 12 caracteres")]
    public string? Descricao { get; set; }

    [Range(1, 1000, ErrorMessage = "Esse campo deve ter valor entre 1 e 1000")]
    public double Valor { get; set; }

    public DateTime CriadoEm { get; set; }




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


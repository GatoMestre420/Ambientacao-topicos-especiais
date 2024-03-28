namespace API.Models;
public class Produto
{
    private string nome;
    private string descricao;

    public void setNome(string nome)
    {

        this.nome = nome
    }
    public string getNome()
    {
        return this.nome;
    }
}


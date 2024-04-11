using Microsoft.EntityFrameworkCore;

namespace API.Models;


// Classe que representa o Entity framework Core : Code First 
public class AppDataContext : DbContext
{

    //Representação das classes que vão virar tabelas no bando de dados 
    public DbSet<Produto> TabelaProdutos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Configuração da conexão com o banco de Dados
        optionsBuilder.UseSqlite("Data Source=app.db");
    }


}
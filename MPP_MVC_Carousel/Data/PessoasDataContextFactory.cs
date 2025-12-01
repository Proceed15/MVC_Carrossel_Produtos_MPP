using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration; 
using MPP_MVC_Carousel.Data;

// A fábrica deve ser pública e estar em um local acessível.
public class PessoasDataContextFactory : IDesignTimeDbContextFactory<PessoasDataContext>
{
    public PessoasDataContext CreateDbContext(string[] args)
    {
        // 1. Configura a leitura do arquivo appsettings.json para o ambiente 'Design Time'
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        // 2. Obtém a string de conexão (Confirme que 'DefaultConnection' é o nome correto)
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // 3. Configura o DbContext com a string
        var builder = new DbContextOptionsBuilder<PessoasDataContext>();
        builder.UseSqlServer(connectionString);

        // 4. Retorna a nova instância que o 'dotnet ef' precisa
        return new PessoasDataContext(builder.Options);
    }
}

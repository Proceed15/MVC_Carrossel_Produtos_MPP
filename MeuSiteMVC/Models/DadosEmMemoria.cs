using System.Collections.Generic;

namespace MeuSiteMVC.Models
{
    public static class DadosEmMemoria
    {
        public static List<Categoria> Categorias { get; set; } = new()
        {
            new Categoria { Id = 1, Nome = "Eletrônicos" },
            new Categoria { Id = 2, Nome = "Livros" },
            new Categoria { Id = 3, Nome = "Roupas" }
        };

        public static List<Produto> Produtos { get; set; } = new()
        {
            new Produto { Id = 1, Nome = "Notebook Dell", Descricao = "Notebook com i7, 16GB RAM", Preco = 4500.00m, CategoriaId = 1, ImagemUrl = "/images/notebook.jpg" },
            new Produto { Id = 2, Nome = "Livro C#", Descricao = "Guia da linguagem de programação de C#", Preco = 75.90m, CategoriaId = 2, ImagemUrl = "/images/livro.jpg" },
            new Produto { Id = 3, Nome = "Camiseta Crefisa", Descricao = "Camiseta verde com logo do Palmeiras", Preco = 49.90m, CategoriaId = 3, ImagemUrl = "/images/camiseta.jpg" }
        };

        public static List<Pedido> Pedidos { get; set; } = new()
        {
            new Pedido { Id = 1, Cliente = "João Silva", Data = DateTime.Now.AddDays(-2), Status = "Pendente" },
            new Pedido { Id = 2, Cliente = "Maria Oliveira", Data = DateTime.Now.AddDays(-1), Status = "Pago" }
        };
    }
}
// Models/DadosEmMemoria.cs
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
            new Produto { Id = 1, Nome = "Notebook Dell", Descricao = "Notebook com i7, 16GB RAM", Preco = 4500.00m, CategoriaId = 1 },
            new Produto { Id = 2, Nome = "Livro C# 12", Descricao = "Guia completo de C#", Preco = 75.90m, CategoriaId = 2 },
            new Produto { Id = 3, Nome = "Camiseta MVC", Descricao = "Camiseta preta com logo MVC", Preco = 49.90m, CategoriaId = 3 }
        };
    }
}
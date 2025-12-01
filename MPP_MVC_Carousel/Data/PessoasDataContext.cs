using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MPP_MVC_Carousel.Models;

namespace MPP_MVC_Carousel.Data
{
    public class PessoasDataContext : DbContext
    {
        public PessoasDataContext(DbContextOptions<PessoasDataContext> options) : base(options)
        {
        }
        
        public DbSet<PessoaModel> PessoaModel { get; set; }
        public DbSet<CategoriaModel> Categorias { get; set; }
        public DbSet<ProdutoModel> Produtos { get; set; }
    }
}
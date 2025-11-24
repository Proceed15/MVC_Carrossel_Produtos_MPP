using System.Collections.Generic;

namespace MeuSiteMVC.Models
{
    public class CategoriaComProdutosViewModel
    {
        public Categoria Categoria { get; set; }
        public List<Produto> Produtos { get; set; }
    }
}
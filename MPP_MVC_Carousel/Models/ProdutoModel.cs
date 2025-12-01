using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPP_MVC_Carousel.Models
{
    public class ProdutoModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Range(0.01, 99999.99, ErrorMessage = "Preço deve ser maior que zero")]
        public decimal Preco { get; set; }

        public string Imagem { get; set; } // Caminho da foto

        // Chave Estrangeira para Categoria
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public virtual CategoriaModel Categoria { get; set; }
    }
}

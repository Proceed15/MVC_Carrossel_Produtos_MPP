using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MPP_MVC_Carousel.Models
{
    public class CategoriaModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da categoria é obrigatório")]
        [MaxLength(50)]
        public string Nome { get; set; }
        
        [MaxLength(300)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        // Relacionamento: Uma categoria tem vários produtos
        public virtual ICollection<ProdutoModel> Produtos { get; set; }
    }
}
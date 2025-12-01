using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MPP_MVC_Carousel.Models
{
    public class PessoaModel
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Nome { get; set; }
        public string Foto { get; set; }
    }
}

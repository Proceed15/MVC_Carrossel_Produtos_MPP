namespace MeuSiteMVC.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int CategoriaId { get; set; }
        public string ImagemUrl { get; set; } = string.Empty; // Nova propriedade
        public HttpPostedFileBase? UploadImage { get; set; } // Propriedade para upload de imagem
    }
}
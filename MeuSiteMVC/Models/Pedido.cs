namespace MeuSiteMVC.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public DateTime Data { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pendente";
        public List<Produto> Itens { get; set; } = new();
    }
}
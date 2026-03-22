using SQLite;

namespace minhasCompras.Models
{
    public class Produto
    {
        [PrimaryKey, AutoIncrement]

        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Quantidade { get; set; }
        public double Preco { get; set; }

        [Ignore]
        public double Total { get => Preco * Quantidade  ; }
    }

}

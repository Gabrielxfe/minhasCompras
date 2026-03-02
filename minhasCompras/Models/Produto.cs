using SQLite;

namespace minhasCompras.Models
{
    public class Produto
    {
        [PrimaryKey, AutoIncrement]

        public int Id { get; set; }
        public string Descricao { get; set; }

        public double quantidade { get; set; }

        public double preco { get; set; }
    }
}

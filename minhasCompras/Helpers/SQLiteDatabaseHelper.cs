using minhasCompras.Models;
using SQLite;

namespace minhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();
        }

        public Task<int> Insert(Produto p) => _conn.InsertAsync(p);
        public Task<int> Update(Produto p) => _conn.UpdateAsync(p);
        public Task<List<Produto>> GetAll() => _conn.Table<Produto>().ToListAsync();
        public Task<int> Delete(int id) => _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        public Task<List<Produto>> Search(string q) =>
            _conn.QueryAsync<Produto>("SELECT * FROM Produto WHERE Descricao LIKE ?", $"%{q}%");
    }
}
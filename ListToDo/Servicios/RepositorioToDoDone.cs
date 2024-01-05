using ListToDo.Controllers;
using System.Data.SqlClient;
using Dapper;
using ListToDo.Models;

namespace ListToDo.Servicios
{
    public interface IRepositorioToDoDone
    {
        Task<List<ToDoDone>> ObtenerToDoDone(int id_usuario);
    }
    public class RepositorioToDoDone: IRepositorioToDoDone
    {
        private readonly string connectionString;

        public RepositorioToDoDone(IConfiguration configuration
            )
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            
        }

        public async Task<List<ToDoDone>> ObtenerToDoDone(int id_usuario)
        {
            using var connection = new SqlConnection(connectionString);

            var todoDone = await connection.QueryAsync<ToDoDone>(@"SELECT * FROM todo_Done
                                                   WHERE id_usuario = @id_usuario", new { id_usuario });

            return todoDone.ToList();
        }

    }
}

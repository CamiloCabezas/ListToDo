using ListToDo.Models;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace ListToDo.Servicios
{
    public interface IRepositorioToDo
    {
        Task Actualizar(ToDoModel toDo);
        Task ActualizarDone(ToDoModel toDo);
        Task crearTodo(ToDoModel toDo);
        Task<ToDoModel> ObtenerById(int id, int usuarioId);
        Task<List<ToDoModel>> ObtenerToDo(int id_usuario);
        Task<List<ToDoDone>> ObtenerToDoDone(int id_usuario);
    }
    public class RepositorioToDo : IRepositorioToDo
    {
        private readonly string connectionString;

        public RepositorioToDo(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task crearTodo(ToDoModel toDo)
        {
            using var connection = new SqlConnection(connectionString);

            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO table_ToDo(Name, Description, id_usuario)
                                                              VALUES(@Name, @Description, @id_usuario);

                                                              SELECT SCOPE_IDENTITY();", toDo);

            toDo.Id = id;
        }

        public async Task<List<ToDoModel>> ObtenerToDo(int id_usuario)
        {
            using var connection = new SqlConnection(connectionString);

            var listToDo = await connection.QueryAsync<ToDoModel>(@"SELECT * FROM table_ToDo
                                                             WHERE id_usuario = @id_usuario", new { id_usuario });

            return listToDo.ToList();
        }



        public async Task<ToDoModel> ObtenerById(int id, int usuarioId)
        {

            using var connection = new SqlConnection(connectionString);

            return await connection.QueryFirstOrDefaultAsync<ToDoModel>(@"SELECT table_ToDo.Id, table_ToDo.Name, table_ToDo.Description, table_ToDo.id_usuario
                                                                        FROM table_ToDo
                                                                        INNER JOIN usuarios ON usuarios.Id = table_ToDo.id_usuario
                                                                        WHERE usuarios.Id = @usuarioId AND table_ToDo.Id = @id
                                                                        ", new { id, usuarioId });
        }


        public async Task Actualizar(ToDoModel toDo)
        {
            
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE table_ToDo
                                            SET Name = @Name, Description = @Description
                                            WHERE Id = @Id", toDo);
        }

        public async Task ActualizarDone(ToDoModel toDo)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE table_ToDo
                                SET Done = 1
                                WHERE Id = @Id", new { toDo.Id });

            var datecreate = DateTime.Now;
            Console.WriteLine(toDo.id_usuario);
            await connection.QueryFirstOrDefaultAsync<ToDoDone>(@"INSERT INTO todo_Done(Name, Description, id_usuario, Done, DateCreate)
                                             VALUES(@Name, @Description, @id_usuario, @Done, @DateCreate)",
                                              new { toDo.Name, toDo.Description, toDo.id_usuario, toDo.Done, datecreate });


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
